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
    public partial class AddOpportunityComponent : ComponentBase
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


        IEnumerable<RadzenCrm.Models.Crm.Contact> _getContactsResult;
        protected IEnumerable<RadzenCrm.Models.Crm.Contact> getContactsResult
        {
            get
            {
                return _getContactsResult;
            }
            set
            {
                if(_getContactsResult != value)
                {
                    _getContactsResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

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

        RadzenCrm.Models.Crm.Opportunity _opportunity;
        protected RadzenCrm.Models.Crm.Opportunity opportunity
        {
            get
            {
                return _opportunity;
            }
            set
            {
                if(_opportunity != value)
                {
                    _opportunity = value;
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
            var crmGetContactsResult = await Crm.GetContacts();
            getContactsResult = crmGetContactsResult;

            var crmGetOpportunityStatusesResult = await Crm.GetOpportunityStatuses();
            getOpportunityStatusesResult = crmGetOpportunityStatusesResult;

            opportunity = new RadzenCrm.Models.Crm.Opportunity();
        }

        protected async void Form0Submit(RadzenCrm.Models.Crm.Opportunity args)
        {
            try
            {
                var crmCreateOpportunityResult = await Crm.CreateOpportunity(opportunity);
                DialogService.Close(opportunity);
            }
            catch (Exception crmCreateOpportunityException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new Opportunity!");
            }
        }

        protected async void Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
