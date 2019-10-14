using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using RadzenCrm.Models.Crm;
using Microsoft.AspNetCore.Identity;
using RadzenCrm.Models;

namespace RadzenCrm.Pages
{
    public partial class HomeComponent : ComponentBase
    {
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
                if(_monthlyStats != value)
                {
                    _monthlyStats = value;
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
                if(_getOpportunitiesResult != value)
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
                if(_getTasksResult != value)
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
                Load();
            }

        }

        protected async void Load()
        {
            var monthlyStatsResult = MonthlyStats();
            monthlyStats = monthlyStatsResult;

            var crmGetOpportunitiesResult = await Crm.GetOpportunities(new Query() { OrderBy = "CloseDate desc" });
            getOpportunitiesResult = crmGetOpportunitiesResult;

            var crmGetTasksResult = await Crm.GetTasks(new Query() { OrderBy = "DueDate desc" });
            getTasksResult = crmGetTasksResult;
        }
    }
}
