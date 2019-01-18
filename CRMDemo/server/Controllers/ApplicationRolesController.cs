using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;

namespace Crm.Controllers
{
    [Authorize(AuthenticationSchemes="Bearer")]
    [ODataRoutePrefix("auth/ApplicationRoles")]
    public partial class ApplicationRolesController : ODataController
    {
       private RoleManager<IdentityRole> roleManager;

       public ApplicationRolesController(RoleManager<IdentityRole> roleManager)
       {
           this.roleManager = roleManager;
       }

       partial void OnRolesRead(ref IQueryable<IdentityRole> roles);

       [EnableQuery]
       [HttpGet]
       public IEnumerable<IdentityRole> Get()
       {
           var roles = roleManager.Roles;

           OnRolesRead(ref roles);

           return roles;
       }

       partial void OnRoleCreated(IdentityRole role);

       [HttpPost]
       public async Task<IActionResult> Post([FromBody] IdentityRole role)
       {
           if (role == null)
           {
               return BadRequest();
           }

           OnRoleCreated(role);

           var result = await roleManager.CreateAsync(role);

           if (!result.Succeeded)
           {
               var message = string.Join(", ", result.Errors.Select(error => error.Description));

               return BadRequest(new { error = new { message }});
           }

           return Created($"auth/Roles('{role.Id}')", role);
       }

       partial void OnRoleDeleted(IdentityRole role);

       [HttpDelete("{Id}")]
       public async Task<IActionResult> Delete(string key)
       {
           var role = await roleManager.FindByIdAsync(key);

           if (role == null)
           {
               return NotFound();
           }

           OnRoleDeleted(role);

           var result = await roleManager.DeleteAsync(role);

           if (!result.Succeeded)
           {
               var message = string.Join(", ", result.Errors.Select(error => error.Description));

               return BadRequest(new { error = new { message }});
           }

           return new NoContentResult();
       }
    }
}
