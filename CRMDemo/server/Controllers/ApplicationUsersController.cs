using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Newtonsoft.Json.Linq;

using Crm.Data;
using Crm.Models;

namespace Crm.Controllers
{
    [Authorize(AuthenticationSchemes="Bearer")]
    [ODataRoutePrefix("auth/ApplicationUsers")]
    public partial class ApplicationUsersController : ODataController
    {
        private UserManager<ApplicationUser> userManager;

        public ApplicationUsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        partial void OnUsersRead(ref IQueryable<ApplicationUser> users);

        [EnableQuery]
        [HttpGet]
        public IEnumerable<ApplicationUser> Get()
        {
            var users = userManager.Users;

            OnUsersRead(ref users);

            return users;
        }

        [EnableQuery]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetApplicationUser(string key)
        {
            var user = await userManager.FindByIdAsync(key);

            if (user == null)
            {
                return NotFound();
            }

            user.RoleNames = await userManager.GetRolesAsync(user);

            return new ObjectResult(user);
        }

        partial void OnUserDeleted(ApplicationUser user);

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string key)
        {
            var user = await userManager.FindByIdAsync(key);

            if (user == null)
            {
                return NotFound();
            }

            OnUserDeleted(user);

            var result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return IdentityError(result);
            }

            return new NoContentResult();
        }

        partial void OnUserUpdated(ApplicationUser user);

        [HttpPatch("{Id}")]
        public async Task<IActionResult> Patch(string key, [FromBody]JObject data)
        {
            var user = await userManager.FindByIdAsync(key);

            if (user == null)
            {
                return NotFound();
            }

            EntityPatch.Apply(user, data);

            var roles = data.GetValue("RoleNames");

            OnUserUpdated(user);

            IdentityResult result = null;

            if (roles != null)
            {
                var rolesData = roles.ToObject<IEnumerable<string>>();

                result = await userManager.RemoveFromRolesAsync(user, await userManager.GetRolesAsync(user));

                if (!result.Succeeded)
                {
                    return IdentityError(result);
                }

                if(rolesData.Any()) 
                {
                    result = await userManager.AddToRolesAsync(user, rolesData);
                }

                if (!result.Succeeded)
                {
                    return IdentityError(result);
                }
            }

            var password = data.GetValue("Password", StringComparison.OrdinalIgnoreCase);

            if (password != null)
            {
                result = await userManager.RemovePasswordAsync(user);

                if (!result.Succeeded)
                {
                    return IdentityError(result);
                }

                result = await userManager.AddPasswordAsync(user, password.ToObject<string>());

                if (!result.Succeeded)
                {
                    return IdentityError(result);
                }
            }

            result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return IdentityError(result);
            }

            return new NoContentResult();
        }

        partial void OnUserCreated(ApplicationUser user);

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]JObject data)
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

            IdentityResult result = null;

            result = await userManager.CreateAsync(user, password.ToObject<string>());

            if (result.Succeeded)
            {
                var roles = data.GetValue("RoleNames");

                if (roles != null)
                {
                    var rolesData = roles.ToObject<IEnumerable<string>>();
                    if (rolesData.Any())
                    {
                        result = await userManager.AddToRolesAsync(user, rolesData);

                        if (!result.Succeeded)
                        {
                            return IdentityError(result);
                        }
                    }
                    
                    user.RoleNames = rolesData;
                }
                
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

        private IActionResult Error(string message)
        {
            return BadRequest(new { error = new { message } });
        }
    }
}
