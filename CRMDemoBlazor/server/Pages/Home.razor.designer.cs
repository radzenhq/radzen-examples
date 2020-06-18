using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using RadzenCrm.Models.Crm;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using RadzenCrm.Models;

namespace RadzenCrm.Pages
{
    public partial class HomeComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }


        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }


        [Inject]
        protected CrmService Crm { get; set; }

        protected RadzenGrid<Opportunity> grid0;

        protected RadzenGrid<RadzenCrm.Models.Crm.Task> grid1;

        RadzenCrm.Pages.Stats _monthlyStats;
        protected RadzenCrm.Pages.Stats monthlyStats
        {
            get
            {
                return _monthlyStats;
            }
            set
            {
                if(!object.Equals(_monthlyStats, value))
                {
                    _monthlyStats = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<RadzenCrm.Pages.RevenueByCompany> _revenueByCompany;
        protected IEnumerable<RadzenCrm.Pages.RevenueByCompany> revenueByCompany
        {
            get
            {
                return _revenueByCompany;
            }
            set
            {
                if(!object.Equals(_revenueByCompany, value))
                {
                    _revenueByCompany = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<RadzenCrm.Pages.RevenueByEmployee> _revenueByEmployee;
        protected IEnumerable<RadzenCrm.Pages.RevenueByEmployee> revenueByEmployee
        {
            get
            {
                return _revenueByEmployee;
            }
            set
            {
                if(!object.Equals(_revenueByEmployee, value))
                {
                    _revenueByEmployee = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<RadzenCrm.Pages.RevenueByMonth> _revenueByMonth;
        protected IEnumerable<RadzenCrm.Pages.RevenueByMonth> revenueByMonth
        {
            get
            {
                return _revenueByMonth;
            }
            set
            {
                if(!object.Equals(_revenueByMonth, value))
                {
                    _revenueByMonth = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<RadzenCrm.Models.Crm.Opportunity> _getOpportunitiesResult;
        protected IEnumerable<RadzenCrm.Models.Crm.Opportunity> getOpportunitiesResult
        {
            get
            {
                return _getOpportunitiesResult;
            }
            set
            {
                if(!object.Equals(_getOpportunitiesResult, value))
                {
                    _getOpportunitiesResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<RadzenCrm.Models.Crm.Task> _getTasksResult;
        protected IEnumerable<RadzenCrm.Models.Crm.Task> getTasksResult
        {
            get
            {
                return _getTasksResult;
            }
            set
            {
                if(!object.Equals(_getTasksResult, value))
                {
                    _getTasksResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }
        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            if (!Security.IsAuthenticated())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                await Load();
            }

        }
        protected async System.Threading.Tasks.Task Load()
        {
            var monthlyStatsResult = MonthlyStats();
            monthlyStats = monthlyStatsResult;

            var revenueByCompanyResult = RevenueByCompany();
            revenueByCompany = revenueByCompanyResult;

            var revenueByEmployeeResult = RevenueByEmployee();
            revenueByEmployee = revenueByEmployeeResult;

            var revenueByMonthResult = RevenueByMonth();
            revenueByMonth = revenueByMonthResult;

            var crmGetOpportunitiesResult = await Crm.GetOpportunities(new Query() { OrderBy = $"CloseDate desc" });
            getOpportunitiesResult = crmGetOpportunitiesResult;

            var crmGetTasksResult = await Crm.GetTasks(new Query() { OrderBy = $"DueDate desc" });
            getTasksResult = crmGetTasksResult;
        }
    }
}
