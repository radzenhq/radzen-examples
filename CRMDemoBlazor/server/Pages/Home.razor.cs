using System;
using System.Linq;
using System.Collections.Generic;
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

  public class RevenueByCompany
  {
      public string Company { get; set; }
      public decimal Revenue { get; set; }
  }

  public class RevenueByEmployee
  {
      public string Employee { get; set; }
      public decimal Revenue { get; set; }
  }

  public class RevenueByMonth
  {
      public DateTime Month { get; set; }
      public decimal Revenue { get; set; }
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

    public IEnumerable<RevenueByCompany> RevenueByCompany()
    {
        return Context.Opportunities
                            .Include(opportunity => opportunity.Contact)
                            .ToList()
                            .GroupBy(opportunity => opportunity.Contact.Company)
                            .Select(group => new RevenueByCompany() {
                                  Company = group.Key,
                                  Revenue = group.Sum(opportunity => opportunity.Amount)
                              });
    }

    public IEnumerable<RevenueByEmployee> RevenueByEmployee()
    {
        return Context.Opportunities
                            .Include(opportunity => opportunity.User)
                            .ToList()
                            .GroupBy(opportunity => $"{opportunity.User.FirstName} {opportunity.User.LastName}")
                            .Select(group => new RevenueByEmployee() {
                                  Employee = group.Key,
                                  Revenue = group.Sum(opportunity => opportunity.Amount)
                              });
    }

    public IEnumerable<RevenueByMonth> RevenueByMonth()
    {
        return Context.Opportunities
                            .Include(opportunity => opportunity.OpportunityStatus)
                            .Where(opportunity => opportunity.OpportunityStatus.Name == "Won")
                            .ToList()
                            .GroupBy(opportunity => new DateTime(opportunity.CloseDate.Year, opportunity.CloseDate.Month, 1))
                            .Select(group => new RevenueByMonth() {
                                Revenue = group.Sum(opportunity => opportunity.Amount),
                                Month = group.Key
                            })
                            .OrderBy(deals => deals.Month);
    }
  }
}
