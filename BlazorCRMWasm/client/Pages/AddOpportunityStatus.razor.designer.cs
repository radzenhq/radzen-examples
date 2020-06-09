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
    public partial class AddOpportunityStatusComponent : ComponentBase
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

        BlazorCrmWasm.Models.Crm.OpportunityStatus _opportunitystatus;
        protected BlazorCrmWasm.Models.Crm.OpportunityStatus opportunitystatus
        {
            get
            {
                return _opportunitystatus;
            }
            set
            {
                if(!object.Equals(_opportunitystatus, value))
                {
                    _opportunitystatus = value;
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
            opportunitystatus = new BlazorCrmWasm.Models.Crm.OpportunityStatus(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(BlazorCrmWasm.Models.Crm.OpportunityStatus args)
        {
            try
            {
                var crmCreateOpportunityStatusResult = await Crm.CreateOpportunityStatus(opportunityStatus:opportunitystatus);
                DialogService.Close(opportunitystatus);
            }
            catch (Exception crmCreateOpportunityStatusException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new OpportunityStatus!");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
