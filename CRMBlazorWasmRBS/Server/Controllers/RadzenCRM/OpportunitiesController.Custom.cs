using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CRMBlazorWasmRBS.Server.Models;
using CRMBlazorWasmRBS.Server.Models.RadzenCRM;

namespace CRMBlazorWasmRBS.Server.Controllers.RadzenCRM
{
    [Authorize]
    public partial class OpportunitiesController
    {
        partial void OnOpportunitiesRead(ref IQueryable<Opportunity> items)
        {
            var userManager = (UserManager<ApplicationUser>)HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>));

            var user = userManager.GetUserAsync(HttpContext.User).Result;
            if (user != null)
            {
                var roles = userManager.GetRolesAsync(user).Result;

                if (!roles.Contains("Sales Manager"))
                {
                    // Filter the opportunities by the current user's id
                    items = items.Where(item => item.UserId == user.Id);
                }
            }

            items = items.Include(item => item.User);
        }
    }
}