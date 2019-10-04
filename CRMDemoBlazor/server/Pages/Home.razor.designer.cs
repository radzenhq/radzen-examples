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


        protected RadzenContent content0;

        protected RadzenHeading pageTitle;

        protected RadzenCard card0;

        protected RadzenIcon icon0;

        protected RadzenHeading heading0;

        protected RadzenHeading heading1;

        protected RadzenHeading heading2;

        protected RadzenCard card1;

        protected RadzenIcon icon1;

        protected RadzenHeading heading3;

        protected RadzenHeading heading4;

        protected RadzenHeading heading5;

        protected RadzenCard card3;

        protected RadzenIcon icon3;

        protected RadzenHeading heading9;

        protected RadzenHeading heading10;

        protected RadzenHeading heading11;

        protected RadzenCard card2;

        protected RadzenIcon icon2;

        protected RadzenHeading heading6;

        protected RadzenHeading heading7;

        protected RadzenHeading heading8;

        protected RadzenCard card4;

        protected RadzenHeading heading12;

        protected RadzenGrid<Opportunity> grid0;

        protected RadzenCard card5;

        protected RadzenHeading heading13;

        protected RadzenGrid<RadzenCrm.Models.Crm.Task> grid1;

        protected RadzenImage image0;

        protected RadzenLabel label0;

        protected RadzenLabel label1;

        protected RadzenLabel label2;

        protected RadzenLabel label3;

        protected RadzenLabel label4;

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

            var crmGetOpportunitiesResult = await Crm.GetOpportunities("CloseDate desc");
            getOpportunitiesResult = crmGetOpportunitiesResult;

            var crmGetTasksResult = await Crm.GetTasks("DueDate desc");
            getTasksResult = crmGetTasksResult;
        }
    }
}
