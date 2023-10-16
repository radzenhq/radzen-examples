using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Radzen;

using CRMBlazorServerRBS.Data;
using CRMBlazorServerRBS.Models.RadzenCRM;

namespace CRMBlazorServerRBS
{
    public partial class RadzenCRMService
    {
        IHttpContextAccessor httpContextAccessor;
        ApplicationIdentityDbContext identityDbContext;

        public RadzenCRMService(RadzenCRMContext context, NavigationManager navigationManager, IHttpContextAccessor httpContextAccessor, ApplicationIdentityDbContext identityDbContext)
          : this(context, navigationManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.identityDbContext = identityDbContext;
        }

        partial void OnOpportunityCreated(Opportunity item)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Set the UserId property of the opportunity to the current user's id
            item.UserId = userId;
        }

        partial void OnOpportunitiesRead(ref IQueryable<Opportunity> items)
        {
            if (!httpContextAccessor.HttpContext.User.IsInRole("Sales Manager"))
            {
                var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Filter the opportunities by the current user's id
                items = items.Where(item => item.UserId == userId);
            }

            // Include the User
            items = items.Include(item => item.User);
        }

        partial void OnTasksRead(ref IQueryable<CRMBlazorServerRBS.Models.RadzenCRM.Task> items)
        {
            items = items.Include(item => item.Opportunity.User).Include(item => item.Opportunity.Contact);
        }
    }
}