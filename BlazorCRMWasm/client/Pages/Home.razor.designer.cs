using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using BlazorCrmWasm.Models.Crm;
using Microsoft.AspNetCore.Identity;
using BlazorCrmWasm.Models;
using BlazorCrmWasm.Client.Pages;

namespace BlazorCrmWasm.Pages
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

        protected RadzenGrid<BlazorCrmWasm.Models.Crm.Task> grid1;

        Stats _monthlyStats;
        protected Stats monthlyStats
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

        IEnumerable<BlazorCrmWasm.Pages.RevenueByCompany> _revenueByCompany;
        protected IEnumerable<BlazorCrmWasm.Pages.RevenueByCompany> revenueByCompany
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

        IEnumerable<BlazorCrmWasm.Pages.RevenueByEmployee> _revenueByEmployee;
        protected IEnumerable<BlazorCrmWasm.Pages.RevenueByEmployee> revenueByEmployee
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

        IEnumerable<BlazorCrmWasm.Pages.RevenueByMonth> _revenueByMonth;
        protected IEnumerable<BlazorCrmWasm.Pages.RevenueByMonth> revenueByMonth
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

        IEnumerable<Opportunity> _getOpportunitiesResult ;
        protected IEnumerable<Opportunity> getOpportunitiesResult 
        {
            get
            {
                return _getOpportunitiesResult ;
            }
            set
            {
                if(!object.Equals(_getOpportunitiesResult , value))
                {
                    _getOpportunitiesResult  = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<BlazorCrmWasm.Models.Crm.Task> _getTasksResult ;
        protected IEnumerable<BlazorCrmWasm.Models.Crm.Task> getTasksResult 
        {
            get
            {
                return _getTasksResult ;
            }
            set
            {
                if(!object.Equals(_getTasksResult , value))
                {
                    _getTasksResult  = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }
        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            if (!await Security.IsAuthenticatedAsync())
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
            monthlyStats = await MonthlyStats();

            revenueByCompany = await RevenueByCompany();

            revenueByEmployee = await RevenueByEmployee();

            revenueByMonth = await RevenueByMonth();

            var crmGetOpportunitiesResult = await Crm.GetOpportunities(orderby:$@"CloseDate desc", expand:$"Contact,OpportunityStatus");
            getOpportunitiesResult  = crmGetOpportunitiesResult.Value;

            var crmGetTasksResult = await Crm.GetTasks(orderby:$@"DueDate desc", expand:$"Opportunity($expand=User,Contact)");
            getTasksResult  = crmGetTasksResult.Value;
        }
    }
}
