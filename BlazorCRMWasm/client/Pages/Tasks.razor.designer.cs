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
    public partial class TasksComponent : ComponentBase
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

        protected RadzenGrid<BlazorCrmWasm.Models.Crm.Task> grid0;

        IEnumerable<BlazorCrmWasm.Models.Crm.Task> _getTasksResult;
        protected IEnumerable<BlazorCrmWasm.Models.Crm.Task> getTasksResult
        {
            get
            {
                return _getTasksResult;
            }
            set
            {
                if(!object.Equals(_getTasksResult, value))
                {
                    _getTasksResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        int _getTasksCount;
        protected int getTasksCount
        {
            get
            {
                return _getTasksCount;
            }
            set
            {
                if(!object.Equals(_getTasksCount, value))
                {
                    _getTasksCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddTask>("Add Task", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var crmGetTasksResult = await Crm.GetTasks(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", expand:$"Opportunity,TaskType,TaskStatus", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getTasksResult = crmGetTasksResult.Value.AsODataEnumerable();

                getTasksCount = crmGetTasksResult.Count;
            }
            catch (Exception crmGetTasksException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to load Tasks");
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(BlazorCrmWasm.Models.Crm.Task args)
        {
            var dialogResult = await DialogService.OpenAsync<EditTask>("Edit Task", new Dictionary<string, object>() { {"Id", args.Id} });
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var crmDeleteTaskResult = await Crm.DeleteTask(id:data.Id);
                if (crmDeleteTaskResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception crmDeleteTaskException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Task");
            }
        }
    }
}
