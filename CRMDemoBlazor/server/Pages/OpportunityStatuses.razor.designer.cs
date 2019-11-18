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
using Microsoft.AspNetCore.Identity;
using RadzenCrm.Models;

namespace RadzenCrm.Pages
{
    public partial class OpportunityStatusesComponent : ComponentBase
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


        protected RadzenGrid<RadzenCrm.Models.Crm.OpportunityStatus> grid0;

        IEnumerable<RadzenCrm.Models.Crm.OpportunityStatus> _getOpportunityStatusesResult;
        protected IEnumerable<RadzenCrm.Models.Crm.OpportunityStatus> getOpportunityStatusesResult
        {
            get
            {
                return _getOpportunityStatusesResult;
            }
            set
            {
                if(_getOpportunityStatusesResult != value)
                {
                    _getOpportunityStatusesResult = value;
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
            var crmGetOpportunityStatusesResult = await Crm.GetOpportunityStatuses();
            getOpportunityStatusesResult = crmGetOpportunityStatusesResult;
        }

        protected async void Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddOpportunityStatus>("Add Opportunity Status", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(RadzenCrm.Models.Crm.OpportunityStatus args)
        {
            var result = await DialogService.OpenAsync<EditOpportunityStatus>("Edit Opportunity Status", new Dictionary<string, object>() { {"Id", args.Id} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(MouseEventArgs args, RadzenCrm.Models.Crm.OpportunityStatus data)
        {
            try
            {
                var crmDeleteOpportunityStatusResult = await Crm.DeleteOpportunityStatus(data.Id);
                if (crmDeleteOpportunityStatusResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception crmDeleteOpportunityStatusException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete OpportunityStatus");
            }
        }
    }
}
