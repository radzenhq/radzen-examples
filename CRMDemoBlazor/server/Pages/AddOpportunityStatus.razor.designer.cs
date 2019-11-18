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


        RadzenCrm.Models.Crm.OpportunityStatus _opportunitystatus;
        protected RadzenCrm.Models.Crm.OpportunityStatus opportunitystatus
        {
            get
            {
                return _opportunitystatus;
            }
            set
            {
                if(_opportunitystatus != value)
                {
                    _opportunitystatus = value;
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
            opportunitystatus = new RadzenCrm.Models.Crm.OpportunityStatus();
        }

        protected async void Form0Submit(RadzenCrm.Models.Crm.OpportunityStatus args)
        {
            try
            {
                var crmCreateOpportunityStatusResult = await Crm.CreateOpportunityStatus(opportunitystatus);
                DialogService.Close(opportunitystatus);
            }
            catch (Exception crmCreateOpportunityStatusException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new OpportunityStatus!");
            }
        }

        protected async void Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
