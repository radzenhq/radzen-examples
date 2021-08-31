using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using RadzenCrm.Models.Crm;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using RadzenCrm.Models;

namespace RadzenCrm.Pages
{
    public partial class EditOpportunityComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected CrmService Crm { get; set; }

        [Parameter]
        public dynamic Id { get; set; }

        RadzenCrm.Models.Crm.Opportunity _opportunity;
        protected RadzenCrm.Models.Crm.Opportunity opportunity
        {
            get
            {
                return _opportunity;
            }
            set
            {
                if (!object.Equals(_opportunity, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "opportunity", NewValue = value, OldValue = _opportunity };
                    _opportunity = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<RadzenCrm.Models.Crm.Contact> _getContactsResult;
        protected IEnumerable<RadzenCrm.Models.Crm.Contact> getContactsResult
        {
            get
            {
                return _getContactsResult;
            }
            set
            {
                if (!object.Equals(_getContactsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getContactsResult", NewValue = value, OldValue = _getContactsResult };
                    _getContactsResult = value;
                    OnPropertyChanged(args);
                    Reload();
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
                if (!object.Equals(_getOpportunityStatusesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getOpportunityStatusesResult", NewValue = value, OldValue = _getOpportunityStatusesResult };
                    _getOpportunityStatusesResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Security.InitializeAsync(AuthenticationStateProvider);
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
            var crmGetOpportunityByIdResult = await Crm.GetOpportunityById(Id);
            opportunity = crmGetOpportunityByIdResult;

            var crmGetContactsResult = await Crm.GetContacts();
            getContactsResult = crmGetContactsResult;

            var crmGetOpportunityStatusesResult = await Crm.GetOpportunityStatuses();
            getOpportunityStatusesResult = crmGetOpportunityStatusesResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(RadzenCrm.Models.Crm.Opportunity args)
        {
            try
            {
                var crmUpdateOpportunityResult = await Crm.UpdateOpportunity(Id, opportunity);
                DialogService.Close(opportunity);
            }
            catch (System.Exception crmUpdateOpportunityException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update Opportunity" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
