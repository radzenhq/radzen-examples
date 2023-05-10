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

namespace CRMBlazorWasmRBS.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ServerMethodsController : Controller
    {
        private CRMBlazorWasmRBS.Server.Data.RadzenCRMContext context;

        public ServerMethodsController(CRMBlazorWasmRBS.Server.Data.RadzenCRMContext context)
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
                                .LastOrDefault();

            return Ok(System.Text.Json.JsonSerializer.Serialize(stats, new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = null }));
        }

        public IActionResult RevenueByCompany()
        {
            var result = context.Opportunities
                                .Include(opportunity => opportunity.Contact)
                                .ToList()
                                .GroupBy(opportunity => opportunity.Contact.Company)
                                .Select(group => new {
                                        Company = group.Key,
                                        Revenue = group.Sum(opportunity => opportunity.Amount)
                                });

            return Ok(System.Text.Json.JsonSerializer.Serialize(result, new System.Text.Json.JsonSerializerOptions
            {
                    PropertyNamingPolicy = null
            }));
        }

        public IActionResult RevenueByEmployee()
        {
            var result = context.Opportunities
                                .Include(opportunity => opportunity.User)
                                .ToList()
                                .GroupBy(opportunity => $"{opportunity.User.FirstName} {opportunity.User.LastName}")
                                .Select(group => new {
                                        Employee = group.Key,
                                        Revenue = group.Sum(opportunity => opportunity.Amount)
                                });


            return Ok(System.Text.Json.JsonSerializer.Serialize(result, new System.Text.Json.JsonSerializerOptions
            {
                    PropertyNamingPolicy = null
            }));
        }

        public IActionResult RevenueByMonth()
        {
            var result = context.Opportunities
                                .Include(opportunity => opportunity.OpportunityStatus)
                                .Where(opportunity => opportunity.OpportunityStatus.Name == "Won")
                                .ToList()
                                .GroupBy(opportunity => new DateTime(opportunity.CloseDate.Year, opportunity.CloseDate.Month, 1))
                                .Select(group => new {
                                    Revenue = group.Sum(opportunity => opportunity.Amount),
                                    Month = group.Key
                                })
                                .OrderBy(deals => deals.Month);
            
            return Ok(System.Text.Json.JsonSerializer.Serialize(result, new System.Text.Json.JsonSerializerOptions
            {
                    PropertyNamingPolicy = null
            }));
        }
    }
}