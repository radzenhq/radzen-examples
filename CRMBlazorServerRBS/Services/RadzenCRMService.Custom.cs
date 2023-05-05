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
using Radzen;

using CRMBlazorServerRBS.Data;
using CRMBlazorServerRBS.Models.RadzenCRM;

namespace CRMBlazorServerRBS
{
    public partial class RadzenCRMService
    {
        private readonly SecurityService security;

        public RadzenCRMService(RadzenCRMContext context, NavigationManager navigationManager, SecurityService security)
          : this(context, navigationManager)
        {
            this.security = security;
        }

        partial void OnOpportunityCreated(Opportunity item)
        {
            var userId = security.User.Id;

            // Set the UserId property of the opportunity to the current user's id
            item.UserId = userId;
        }

        partial void OnOpportunitiesRead(ref IQueryable<Opportunity> items)
        {
            if (!security.IsInRole("Sales Manager"))
            {
                var userId = security.User.Id;

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