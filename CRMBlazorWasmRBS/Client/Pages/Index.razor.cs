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

namespace CRMBlazorWasmRBS.Client.Pages
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
        HttpClient Http { get; set; }

        [Inject]
        RadzenCRMService RadzenCRMService { get; set; }

        public async Task<Stats> MonthlyStats()
        {
            var response = await Http.SendAsync(new HttpRequestMessage(HttpMethod.Get, new Uri($"{NavigationManager.BaseUri}api/servermethods/monthlystats")));

            return await response.ReadAsync<Stats>();
        }

        public async Task<IEnumerable<RevenueByCompany>> RevenueByCompany()
        {
            var response = await Http.SendAsync(new HttpRequestMessage(HttpMethod.Get, new Uri($"{NavigationManager.BaseUri}api/servermethods/RevenueByCompany")));

            return await response.ReadAsync<IEnumerable<RevenueByCompany>>();
        }

        public async Task<IEnumerable<RevenueByEmployee>> RevenueByEmployee()
        {
            var response = await Http.SendAsync(new HttpRequestMessage(HttpMethod.Get, new Uri($"{NavigationManager.BaseUri}api/servermethods/RevenueByEmployee")));

            return await response.ReadAsync<IEnumerable<RevenueByEmployee>>();
        }

        public async Task<IEnumerable<RevenueByMonth>> RevenueByMonth()
        {
            var response = await Http.SendAsync(new HttpRequestMessage(HttpMethod.Get, new Uri($"{NavigationManager.BaseUri}api/servermethods/RevenueByMonth")));

            return await response.ReadAsync<IEnumerable<RevenueByMonth>>();
        }

        protected override async Task OnInitializedAsync()
        {
            monthlyStats = await MonthlyStats();
            revenueByCompany = await RevenueByCompany();
            revenueByMonth = await RevenueByMonth();
            revenueByEmployee = await RevenueByEmployee();
        }

        protected async Task getOpportunitiesResultLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await RadzenCRMService.GetOpportunities(expand: "Contact,OpportunityStatus", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: args.Filter, orderby: args.OrderBy);

                getOpportunitiesResult = result.Value.AsODataEnumerable();
                getOpportunitiesResultCount = result.Count;
            }
            catch (Exception)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load" });
            }
        }


        protected async Task getTasksResultLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await RadzenCRMService.GetTasks(expand: "Opportunity($expand=User,Contact)", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: args.Filter, orderby: args.OrderBy);

                getTasksResult = result.Value.AsODataEnumerable();
                getTasksResultCount = result.Count;
            }
            catch (Exception)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load" });
            }
        }

        Stats monthlyStats { get; set; }
        IEnumerable<Pages.RevenueByCompany> revenueByCompany { get; set; }
        IEnumerable<Pages.RevenueByMonth> revenueByMonth { get; set; }
        IEnumerable<Pages.RevenueByEmployee> revenueByEmployee { get; set; }
        IEnumerable<Server.Models.RadzenCRM.Opportunity> getOpportunitiesResult { get; set; }
        IEnumerable<Server.Models.RadzenCRM.Task> getTasksResult { get; set; }

        protected int getOpportunitiesResultCount;

        protected int getTasksResultCount;
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
