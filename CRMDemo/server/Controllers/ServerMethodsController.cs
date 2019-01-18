using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Crm.Data;
using Crm.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Crm.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ServerMethodsController : Controller
    {
        private readonly CrmContext context;

        private readonly UserManager<ApplicationUser> userManager;
        public ServerMethodsController(CrmContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
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


            return Json(new { value = result }, new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
            });
        }

        public IActionResult RevenueByEmployee()
        {
            var result = context.Opportunities
                                .Include(opportunity => opportunity.User)
                                .GroupBy(opportunity => opportunity.User.UserName)
                                .Select(group => new {
                                     Employee = group.Key,
                                     Revenue = group.Sum(opportunity => opportunity.Amount)
                                 });


            return Json(new { value = result }, new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
            });
        }

        public IActionResult RevenueByMonth()
        {
            var result = context.Opportunities
                                 .GroupBy(opportunity => new DateTime(opportunity.CloseDate.Year, opportunity.CloseDate.Month, 1))
                                 .Select(group => new {
                                     Revenue = group.Sum(opportunity => opportunity.Amount),
                                     Month = group.Key
                                 });
            
            return Json(new { value = result }, new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
            });
        }

        public IActionResult AverageDealSizeByMonth()
        {
            var result = context.Opportunities
                                .GroupBy(opportunity => new DateTime(opportunity.CloseDate.Year, opportunity.CloseDate.Month, 1))
                                .Select(group => new {
                                    DealSize = group.Average(opportunity => opportunity.Amount),
                                    Month = group.Key
                                });
            
            return Json(new { value = result }, new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
            });
        }

        public IActionResult DealsWonRatio()
        {
            double wonOpportunities = context.Opportunities
                                .Include(opportunity => opportunity.OpportunityStatus)
                                .Where(opportunity => opportunity.OpportunityStatus.Name == "Won")
                                .Count();

            var totalOpportunities = context.Opportunities.Count();

            var result = wonOpportunities / totalOpportunities;

            return Json(new { value = result});
        }

        public IActionResult UserPersonalData()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = userManager.FindByIdAsync(userId).GetAwaiter().GetResult();

            return Json(new { 
                FirstName = result.FirstName, 
                LastName = result.LastName, 
                Picture = result.Picture },  new JsonSerializerSettings() {
                ContractResolver = new DefaultContractResolver()
            });
        }

        [HttpPost]
        public IActionResult UpdatePersonalData(string firstName, string lastName, string picture)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = userManager.FindByIdAsync(userId).GetAwaiter().GetResult();

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Picture = picture;

            var result = userManager.UpdateAsync(user).GetAwaiter().GetResult();

            if (result == IdentityResult.Success)
            {
                return Ok();
            }
            else
            {
                var message =  string.Join(", ", result.Errors.Select(error => error.Description));

                return BadRequest(new { error = new { message }});
            }
        }
    }
}