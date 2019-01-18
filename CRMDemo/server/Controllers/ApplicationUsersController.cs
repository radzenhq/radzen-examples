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

            var roles = data.GetValue("RoleNames").ToObject<IEnumerable<string>>();

            OnUserUpdated(user);

            IdentityResult result = null;

            if (roles != null)
            {
                result = await userManager.RemoveFromRolesAsync(user, await userManager.GetRolesAsync(user));

                if (!result.Succeeded)
                {
                    return IdentityError(result);
                }

                if(roles.Any()) 
                {
                    result = await userManager.AddToRolesAsync(user, roles);
                }

                if (!result.Succeeded)
                {
                    return IdentityError(result);
                }
            }

            var password = data.GetValue("Password", StringComparison.OrdinalIgnoreCase).ToObject<string>();

            if (password != null)
            {
                result = await userManager.RemovePasswordAsync(user);

                if (!result.Succeeded)
                {
                    return IdentityError(result);
                }

                result = await userManager.AddPasswordAsync(user, password);

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
            var email = data.GetValue("Email", StringComparison.OrdinalIgnoreCase).ToObject<string>();
            var password = data.GetValue("Password", StringComparison.OrdinalIgnoreCase).ToObject<string>();

            var user = new ApplicationUser { UserName = email, Email = email };

            EntityPatch.Apply(user, data);

            OnUserCreated(user);

            IdentityResult result = null;

            result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var roles = data.GetValue("RoleNames").ToObject<IEnumerable<string>>();

                if (roles != null && roles.Any())
                {
                    result = await userManager.AddToRolesAsync(user, roles);

                    if (!result.Succeeded)
                    {
                        return IdentityError(result);
                    }
                }

                user.RoleNames = roles;
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
