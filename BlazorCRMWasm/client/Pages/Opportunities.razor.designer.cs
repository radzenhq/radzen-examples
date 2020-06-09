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
    public partial class OpportunitiesComponent : ComponentBase
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

        protected RadzenGrid<BlazorCrmWasm.Models.Crm.Opportunity> grid0;

        IEnumerable<BlazorCrmWasm.Models.Crm.Opportunity> _getOpportunitiesResult;
        protected IEnumerable<BlazorCrmWasm.Models.Crm.Opportunity> getOpportunitiesResult
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

        int _getOpportunitiesCount;
        protected int getOpportunitiesCount
        {
            get
            {
                return _getOpportunitiesCount;
            }
            set
            {
                if(!object.Equals(_getOpportunitiesCount, value))
                {
                    _getOpportunitiesCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddOpportunity>("Add Opportunity", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var crmGetOpportunitiesResult = await Crm.GetOpportunities(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", expand:$"Contact,OpportunityStatus,User", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getOpportunitiesResult = crmGetOpportunitiesResult.Value.AsODataEnumerable();

                getOpportunitiesCount = crmGetOpportunitiesResult.Count;
            }
            catch (Exception crmGetOpportunitiesException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to load Opportunities");
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(BlazorCrmWasm.Models.Crm.Opportunity args)
        {
            var dialogResult = await DialogService.OpenAsync<EditOpportunity>("Edit Opportunity", new Dictionary<string, object>() { {"Id", args.Id} });
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var crmDeleteOpportunityResult = await Crm.DeleteOpportunity(id:data.Id);
                if (crmDeleteOpportunityResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception crmDeleteOpportunityException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Opportunity");
            }
        }
    }
}
