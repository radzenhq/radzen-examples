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
    public partial class TaskStatusesComponent : ComponentBase
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

        protected RadzenGrid<BlazorCrmWasm.Models.Crm.TaskStatus> grid0;

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

        int _getTaskStatusesCount;
        protected int getTaskStatusesCount
        {
            get
            {
                return _getTaskStatusesCount;
            }
            set
            {
                if(!object.Equals(_getTaskStatusesCount, value))
                {
                    _getTaskStatusesCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddTaskStatus>("Add Task Status", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var crmGetTaskStatusesResult = await Crm.GetTaskStatuses(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getTaskStatusesResult = crmGetTaskStatusesResult.Value.AsODataEnumerable();

                getTaskStatusesCount = crmGetTaskStatusesResult.Count;
            }
            catch (Exception crmGetTaskStatusesException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to load TaskStatuses");
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(BlazorCrmWasm.Models.Crm.TaskStatus args)
        {
            var dialogResult = await DialogService.OpenAsync<EditTaskStatus>("Edit Task Status", new Dictionary<string, object>() { {"Id", args.Id} });
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var crmDeleteTaskStatusResult = await Crm.DeleteTaskStatus(id:data.Id);
                if (crmDeleteTaskStatusResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception crmDeleteTaskStatusException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete TaskStatus");
            }
        }
    }
}
