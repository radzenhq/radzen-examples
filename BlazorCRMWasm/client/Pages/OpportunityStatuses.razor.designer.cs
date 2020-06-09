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

        protected RadzenGrid<BlazorCrmWasm.Models.Crm.OpportunityStatus> grid0;

        IEnumerable<BlazorCrmWasm.Models.Crm.OpportunityStatus> _getOpportunityStatusesResult;
        protected IEnumerable<BlazorCrmWasm.Models.Crm.OpportunityStatus> getOpportunityStatusesResult
        {
            get
            {
                return _getOpportunityStatusesResult;
            }
            set
            {
                if(!object.Equals(_getOpportunityStatusesResult, value))
                {
                    _getOpportunityStatusesResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        int _getOpportunityStatusesCount;
        protected int getOpportunityStatusesCount
        {
            get
            {
                return _getOpportunityStatusesCount;
            }
            set
            {
                if(!object.Equals(_getOpportunityStatusesCount, value))
                {
                    _getOpportunityStatusesCount = value;
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

        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddOpportunityStatus>("Add Opportunity Status", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var crmGetOpportunityStatusesResult = await Crm.GetOpportunityStatuses(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getOpportunityStatusesResult = crmGetOpportunityStatusesResult.Value.AsODataEnumerable();

                getOpportunityStatusesCount = crmGetOpportunityStatusesResult.Count;
            }
            catch (Exception crmGetOpportunityStatusesException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to load OpportunityStatuses");
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(BlazorCrmWasm.Models.Crm.OpportunityStatus args)
        {
            var dialogResult = await DialogService.OpenAsync<EditOpportunityStatus>("Edit Opportunity Status", new Dictionary<string, object>() { {"Id", args.Id} });
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var crmDeleteOpportunityStatusResult = await Crm.DeleteOpportunityStatus(id:data.Id);
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
