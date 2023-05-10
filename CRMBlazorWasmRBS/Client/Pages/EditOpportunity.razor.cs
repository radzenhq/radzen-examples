using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace CRMBlazorWasmRBS.Client.Pages
{
    public partial class EditOpportunity
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

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            opportunity = await RadzenCRMService.GetOpportunityById(id:Id);
        }
        protected bool errorVisible;
        protected CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity opportunity;

        protected IEnumerable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact> contactsForContactId;

        protected IEnumerable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus> opportunityStatusesForStatusId;


        protected int contactsForContactIdCount;
        protected CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact contactsForContactIdValue;
        protected async Task contactsForContactIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await RadzenCRMService.GetContacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                contactsForContactId = result.Value.AsODataEnumerable();
                contactsForContactIdCount = result.Count;

                if (!object.Equals(opportunity.ContactId, null))
                {
                    var valueResult = await RadzenCRMService.GetContacts(filter: $"Id eq {opportunity.ContactId}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        contactsForContactIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Contact" });
            }
        }

        protected int opportunityStatusesForStatusIdCount;
        protected CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus opportunityStatusesForStatusIdValue;

        [Inject]
        protected SecurityService Security { get; set; }
        protected async Task opportunityStatusesForStatusIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await RadzenCRMService.GetOpportunityStatuses(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                opportunityStatusesForStatusId = result.Value.AsODataEnumerable();
                opportunityStatusesForStatusIdCount = result.Count;

                if (!object.Equals(opportunity.StatusId, null))
                {
                    var valueResult = await RadzenCRMService.GetOpportunityStatuses(filter: $"Id eq {opportunity.StatusId}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        opportunityStatusesForStatusIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load OpportunityStatus" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await RadzenCRMService.UpdateOpportunity(id:Id, opportunity);
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