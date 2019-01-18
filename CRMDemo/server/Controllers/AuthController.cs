using System;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

using Crm.Authentication;
using Crm.Data;
using Crm.Models;

namespace Crm.Controllers
{
    [Route("/auth/[action]")]
    public partial class AuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHostingEnvironment env;
        public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IHostingEnvironment env)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.env = env;
        }

        private IActionResult Error(string message)
        {
            return BadRequest(new { error = new { message } });
        }

        private IActionResult Jwt(IEnumerable<Claim> claims)
        {
            var handler = new JwtSecurityTokenHandler();

            var token = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenProviderOptions.Issuer,
                Audience = TokenProviderOptions.Audience,
                SigningCredentials = TokenProviderOptions.SigningCredentials,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenProviderOptions.Expiration)
            });

            return Json(new { access_token = handler.WriteToken(token), expires_in = TokenProviderOptions.Expiration.TotalSeconds });
        }

        partial void OnChangePassword(ApplicationUser user, string newPassword);

        [Authorize(AuthenticationSchemes="Bearer")]
        [HttpPost("/auth/change-password")]
        public async Task<IActionResult> ChangePassword([FromBody]JObject data)
        {
            var oldPassword = data.GetValue("OldPassword", StringComparison.OrdinalIgnoreCase);
            var newPassword = data.GetValue("NewPassword", StringComparison.OrdinalIgnoreCase);

            if (oldPassword == null || newPassword == null)
            {
                return Error("Invalid old or new or password.");
            }

            var id = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var user = await userManager.FindByIdAsync(id);

            OnChangePassword(user, newPassword.ToObject<string>());

            var result = await userManager.ChangePasswordAsync(user, oldPassword.ToObject<string>(), newPassword.ToObject<string>());

            if (!result.Succeeded)
            {
                var message = string.Join(", ", result.Errors.Select(error => error.Description));

                return Error(message);
            }

            return new NoContentResult();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]JObject data)
        {
            var username = data.GetValue("UserName", StringComparison.OrdinalIgnoreCase);
            var password = data.GetValue("Password", StringComparison.OrdinalIgnoreCase);

            if (username == null || password == null)
            {
                return Error("Invalid user name or password.");
            }

            if (env.EnvironmentName == "Development" && username.ToObject<string>() == "admin" && password.ToObject<string>() == "admin")
            {
                return Jwt(new List<Claim>() {
                  new Claim(ClaimTypes.Name, "admin"),
                  new Claim(ClaimTypes.Email, "admin")
                });
            }

            var user = await userManager.FindByNameAsync(username.ToObject<string>());

            if (user == null)
            {
                return Error("Invalid user name or password.");
            }

            var validPassword = await userManager.CheckPasswordAsync(user, password.ToObject<string>());

            if (!validPassword)
            {
                return Error("Invalid user name or password.");
            }
            var principal = await signInManager.CreateUserPrincipalAsync(user);

            return Jwt(principal.Claims);
        }

        partial void OnUserCreated(ApplicationUser user);

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]JObject data)
        {
            var email = data.GetValue("Email", StringComparison.OrdinalIgnoreCase);
            var password = data.GetValue("Password", StringComparison.OrdinalIgnoreCase);

            if (email == null || password == null)
            {
                return Error("Invalid email or password.");
            }

            var user = new ApplicationUser { UserName = email.ToObject<string>(), Email = email.ToObject<string>() };

            EntityPatch.Apply(user, data);

            OnUserCreated(user);

            var result = await userManager.CreateAsync(user, password.ToObject<string>());

            if (result.Succeeded)
            {
                return Created($"auth/Users('{user.Id}')", user);
            }
            else
            {
                return IdentityError(result);
            }
        }

        private IActionResult IdentityError(IdentityResult result)
        {
            var message = string.Join(", ", result.Errors.Select(error => error.Description));

            return BadRequest(new { error = new { message } });
        }

    }
}
