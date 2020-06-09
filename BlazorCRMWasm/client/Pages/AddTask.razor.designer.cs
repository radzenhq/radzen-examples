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
    public partial class AddTaskComponent : ComponentBase
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

        IEnumerable<BlazorCrmWasm.Models.Crm.TaskType> _getTaskTypesResult;
        protected IEnumerable<BlazorCrmWasm.Models.Crm.TaskType> getTaskTypesResult
        {
            get
            {
                return _getTaskTypesResult;
            }
            set
            {
                if(!object.Equals(_getTaskTypesResult, value))
                {
                    _getTaskTypesResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<BlazorCrmWasm.Models.Crm.TaskStatus> _getTaskStatusesResult;
        protected IEnumerable<BlazorCrmWasm.Models.Crm.TaskStatus> getTaskStatusesResult
        {
            get
            {
                return _getTaskStatusesResult;
            }
            set
            {
                if(!object.Equals(_getTaskStatusesResult, value))
                {
                    _getTaskStatusesResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        BlazorCrmWasm.Models.Crm.Task _task;
        protected BlazorCrmWasm.Models.Crm.Task task
        {
            get
            {
                return _task;
            }
            set
            {
                if(!object.Equals(_task, value))
                {
                    _task = value;
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
            var crmGetOpportunitiesResult = await Crm.GetOpportunities();
            getOpportunitiesResult = crmGetOpportunitiesResult.Value.AsODataEnumerable();

            var crmGetTaskTypesResult = await Crm.GetTaskTypes();
            getTaskTypesResult = crmGetTaskTypesResult.Value.AsODataEnumerable();

            var crmGetTaskStatusesResult = await Crm.GetTaskStatuses();
            getTaskStatusesResult = crmGetTaskStatusesResult.Value.AsODataEnumerable();

            task = new BlazorCrmWasm.Models.Crm.Task(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(BlazorCrmWasm.Models.Crm.Task args)
        {
            try
            {
                var crmCreateTaskResult = await Crm.CreateTask(task:task);
                DialogService.Close(task);
            }
            catch (Exception crmCreateTaskException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new Task!");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
