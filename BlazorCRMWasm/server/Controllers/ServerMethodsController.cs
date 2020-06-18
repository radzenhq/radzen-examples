using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using BlazorCrmWasm.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrmWasm.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ServerMethodsController : Controller
    {
        private readonly CrmContext context;

        public ServerMethodsController(CrmContext context)
        {
            this.context = context;
        }

        public IActionResult MonthlyStats()
        {
            double wonOpportunities = context.Opportunities
                                .Include(opportunity => opportunity.OpportunityStatus)
                                .Where(opportunity => opportunity.OpportunityStatus.Name == "Won")
                                .Count();

            var totalOpportunities = context.Opportunities.Count();

            var ratio = wonOpportunities / totalOpportunities;

            var stats = context.Opportunities
                               .Include(opportunity => opportunity.OpportunityStatus)
                               .Where(opportunity => opportunity.OpportunityStatus.Name == "Won")
                               .ToList()
                               .GroupBy(opportunity => new DateTime(opportunity.CloseDate.Year, opportunity.CloseDate.Month, 1))
                               .Select(group => new
                               {
                                   Month = group.Key,
                                   Revenue = group.Sum(opportunity => opportunity.Amount),
                                   Opportunities = group.Count(),
                                   AverageDealSize = group.Average(opportunity => opportunity.Amount),
                                   Ratio = ratio
                               })
                               .OrderBy(deals => deals.Month)
                               .Last();

            return Ok(JsonSerializer.Serialize(stats, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            }));
        }

        public IActionResult RevenueByCompany()
        {
            var result = context.Opportunities
                                .Include(opportunity => opportunity.Contact)
                                .GroupBy(opportunity => opportunity.Contact.Company)
                                .Select(group => new {
                                     Company = group.Key,
                                     Revenue = group.Sum(opportunity => opportunity.Amount)
                                 });

            return Ok(JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            }));
        }

        public IActionResult RevenueByEmployee()
        {
            var result = context.Opportunities
                                .Include(opportunity => opportunity.User)
                                .GroupBy(opportunity => $"{opportunity.User.FirstName} {opportunity.User.LastName}")
                                .Select(group => new {
                                     Employee = group.Key,
                                     Revenue = group.Sum(opportunity => opportunity.Amount)
                                 });


            return Ok(JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            }));
        }

        public IActionResult RevenueByMonth()
        {
            var result = context.Opportunities
                                .Include(opportunity => opportunity.OpportunityStatus)
                                .Where(opportunity => opportunity.OpportunityStatus.Name == "Won")
                                .GroupBy(opportunity => new DateTime(opportunity.CloseDate.Year, opportunity.CloseDate.Month, 1))
                                .Select(group => new {
                                    Revenue = group.Sum(opportunity => opportunity.Amount),
                                    Month = group.Key
                                })
                                .OrderBy(deals => deals.Month);
            
            return Ok(JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = null
            }));
        }
    }
}
