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
    }
}
