using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using Microsoft.EntityFrameworkCore;

namespace CRMBlazorServerRBS.Pages
{
    public partial class Index
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        [Inject]
        public Data.RadzenCRMContext Context { get; set; }

        [Inject]
        public RadzenCRMService RadzenCRMService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            monthlyStats = MonthlyStats();
            revenueByCompany = RevenueByCompany();
            revenueByMonth = RevenueByMonth();
            revenueByEmployee = RevenueByEmployee();
            getOpportunitiesResult = await RadzenCRMService.GetOpportunities(new Radzen.Query { OrderBy = "CloseDate desc", Expand = "Contact,OpportunityStatus" });
            getTasksResult = await RadzenCRMService.GetTasks(new Radzen.Query { OrderBy = "DueDate desc" });
        }

        public Stats monthlyStats { get; set; }
        IEnumerable<Pages.RevenueByCompany> revenueByCompany { get; set; }
        IEnumerable<Pages.RevenueByMonth> revenueByMonth { get; set; }
        IEnumerable<Pages.RevenueByEmployee> revenueByEmployee { get; set; }
        IQueryable<Models.RadzenCRM.Opportunity> getOpportunitiesResult { get; set; }
        IQueryable<Models.RadzenCRM.Task> getTasksResult { get; set; }

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
                            .LastOrDefault();
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
}