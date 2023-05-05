using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace CRMBlazorServerRBS.Pages
{
    public partial class AddOpportunity
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        [Inject]
        public RadzenCRMService RadzenCRMService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            opportunity = new CRMBlazorServerRBS.Models.RadzenCRM.Opportunity();

            contactsForContactId = await RadzenCRMService.GetContacts();

            opportunityStatusesForStatusId = await RadzenCRMService.GetOpportunityStatuses();
        }
        protected bool errorVisible;
        protected CRMBlazorServerRBS.Models.RadzenCRM.Opportunity opportunity;

        protected IEnumerable<CRMBlazorServerRBS.Models.RadzenCRM.Contact> contactsForContactId;

        protected IEnumerable<CRMBlazorServerRBS.Models.RadzenCRM.OpportunityStatus> opportunityStatusesForStatusId;

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task FormSubmit()
        {
            try
            {
                await RadzenCRMService.CreateOpportunity(opportunity);
                DialogService.Close(opportunity);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}