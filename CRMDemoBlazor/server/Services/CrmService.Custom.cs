using System.Linq;
using System.Linq.Dynamic.Core;
using RadzenCrm.Data;
using RadzenCrm.Models.Crm;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;

namespace RadzenCrm
{
  public partial class CrmService
  {
    private readonly SecurityService security;

    public CrmService(CrmContext context, NavigationManager navigationManager, SecurityService security) : this(context, navigationManager)
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
      if (security.IsInRole("Sales Manager"))
      {
        var userId = security.User.Id;

        // Filter the opportunities by the current user's id
        items = items.Where(item => item.UserId == userId);

        items = items.Include(item => item.User);
      }
    }

    partial void OnTasksRead(ref IQueryable<Task> items)
    {
      items = items.Include(item => item.Opportunity.User).Include(item => item.Opportunity.Contact);
    }
  }
}
