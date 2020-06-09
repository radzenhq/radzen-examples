using System;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using BlazorCrmWasm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrmWasm.Controllers.Crm
{
    [Authorize]
    public partial class OpportunitiesController
    {
        partial void OnOpportunitiesRead(ref IQueryable<Models.Crm.Opportunity> items)
        {
            var userManager = (UserManager<ApplicationUser>)HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));

            var user = userManager.GetUserAsync(HttpContext.User).Result;
            if (user != null)
            {
                var roles = userManager.GetRolesAsync(user).Result;

                if (roles.Contains("Sales Manager"))
                {
                    // Filter the opportunities by the current user's id
                    items = items.Where(item => item.UserId == user.Id);
                }
            }
        }

        partial void OnOpportunityCreated(Models.Crm.Opportunity item)
        {
            var userManager = (UserManager<ApplicationUser>)HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));

            var userId = userManager.GetUserId(HttpContext.User);

            // Set the UserId property of the opportunity to the current user's id
            item.UserId = userId;
        }
    }
}
