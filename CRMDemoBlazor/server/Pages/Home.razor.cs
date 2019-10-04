using System;
using System.Linq;
using RadzenCrm.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace RadzenCrm.Pages
{
  public class Stats
  {
    public DateTime Month { get; set; }
    public decimal Revenue { get; set; }

    public int Opportunities { get; set; }
    public decimal AverageDealSize { get; set; }
    public double Ratio { get; set; }
  }
  public partial class HomeComponent
  {
    [Inject]
    public CrmContext Context { get; set; }

    public Stats MonthlyStats()
    {
      double wonOpportunities = Context.Opportunities
                          .Include(opportunity => opportunity.OpportunityStatus)
                          .Where(opportunity => opportunity.OpportunityStatus.Name == "Won")
                          .Count();

      var totalOpportunities = Context.Opportunities.Count();

      var ratio = wonOpportunities / totalOpportunities;

      return Context.Opportunities
                         .Include(opportunity => opportunity.OpportunityStatus)
                         .Where(opportunity => opportunity.OpportunityStatus.Name == "Won")
                         .ToList()
                         .GroupBy(opportunity => new DateTime(opportunity.CloseDate.Year, opportunity.CloseDate.Month, 1))
                         .Select(group => new Stats()
                         {
                           Month = group.Key,
                           Revenue = group.Sum(opportunity => opportunity.Amount),
                           Opportunities = group.Count(),
                           AverageDealSize = group.Average(opportunity => opportunity.Amount),
                           Ratio = ratio
                         })
                         .OrderBy(deals => deals.Month)
                         .Last();
    }
  }
}
