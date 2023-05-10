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
    public partial class EditTask
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
            task = await RadzenCRMService.GetTaskById(id:Id);
        }
        protected bool errorVisible;
        protected CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task task;

        protected IEnumerable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity> opportunitiesForOpportunityId;

        protected IEnumerable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus> taskStatusesForStatusId;

        protected IEnumerable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType> taskTypesForTypeId;


        protected int opportunitiesForOpportunityIdCount;
        protected CRMBlazorWasmRBS.Server.Models.RadzenCRM.Opportunity opportunitiesForOpportunityIdValue;
        protected async Task opportunitiesForOpportunityIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await RadzenCRMService.GetOpportunities(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                opportunitiesForOpportunityId = result.Value.AsODataEnumerable();
                opportunitiesForOpportunityIdCount = result.Count;

                if (!object.Equals(task.OpportunityId, null))
                {
                    var valueResult = await RadzenCRMService.GetOpportunities(filter: $"Id eq {task.OpportunityId}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        opportunitiesForOpportunityIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Opportunity" });
            }
        }

        protected int taskStatusesForStatusIdCount;
        protected CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus taskStatusesForStatusIdValue;
        protected async Task taskStatusesForStatusIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await RadzenCRMService.GetTaskStatuses(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                taskStatusesForStatusId = result.Value.AsODataEnumerable();
                taskStatusesForStatusIdCount = result.Count;

                if (!object.Equals(task.StatusId, null))
                {
                    var valueResult = await RadzenCRMService.GetTaskStatuses(filter: $"Id eq {task.StatusId}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        taskStatusesForStatusIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load TaskStatus" });
            }
        }

        protected int taskTypesForTypeIdCount;
        protected CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType taskTypesForTypeIdValue;

        [Inject]
        protected SecurityService Security { get; set; }
        protected async Task taskTypesForTypeIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await RadzenCRMService.GetTaskTypes(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"{args.Filter}", orderby: $"{args.OrderBy}");
                taskTypesForTypeId = result.Value.AsODataEnumerable();
                taskTypesForTypeIdCount = result.Count;

                if (!object.Equals(task.TypeId, null))
                {
                    var valueResult = await RadzenCRMService.GetTaskTypes(filter: $"Id eq {task.TypeId}");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        taskTypesForTypeIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load TaskType" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await RadzenCRMService.UpdateTask(id:Id, task);
                DialogService.Close(task);
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