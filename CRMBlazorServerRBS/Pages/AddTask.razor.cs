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
    public partial class AddTask
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
            task = new CRMBlazorServerRBS.Models.RadzenCRM.Task();

            opportunitiesForOpportunityId = await RadzenCRMService.GetOpportunities();

            taskStatusesForStatusId = await RadzenCRMService.GetTaskStatuses();

            taskTypesForTypeId = await RadzenCRMService.GetTaskTypes();
        }
        protected bool errorVisible;
        protected CRMBlazorServerRBS.Models.RadzenCRM.Task task;

        protected IEnumerable<CRMBlazorServerRBS.Models.RadzenCRM.Opportunity> opportunitiesForOpportunityId;

        protected IEnumerable<CRMBlazorServerRBS.Models.RadzenCRM.TaskStatus> taskStatusesForStatusId;

        protected IEnumerable<CRMBlazorServerRBS.Models.RadzenCRM.TaskType> taskTypesForTypeId;

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task FormSubmit()
        {
            try
            {
                await RadzenCRMService.CreateTask(task);
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