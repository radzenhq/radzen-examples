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
    public partial class TaskTypesComponent : ComponentBase
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

        protected RadzenGrid<BlazorCrmWasm.Models.Crm.TaskType> grid0;

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

        int _getTaskTypesCount;
        protected int getTaskTypesCount
        {
            get
            {
                return _getTaskTypesCount;
            }
            set
            {
                if(!object.Equals(_getTaskTypesCount, value))
                {
                    _getTaskTypesCount = value;
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
            var dialogResult = await DialogService.OpenAsync<AddTaskType>("Add Task Type", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var crmGetTaskTypesResult = await Crm.GetTaskTypes(filter:$"{args.Filter}", orderby:$"{args.OrderBy}", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getTaskTypesResult = crmGetTaskTypesResult.Value.AsODataEnumerable();

                getTaskTypesCount = crmGetTaskTypesResult.Count;
            }
            catch (Exception crmGetTaskTypesException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to load TaskTypes");
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(BlazorCrmWasm.Models.Crm.TaskType args)
        {
            var dialogResult = await DialogService.OpenAsync<EditTaskType>("Edit Task Type", new Dictionary<string, object>() { {"Id", args.Id} });
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var crmDeleteTaskTypeResult = await Crm.DeleteTaskType(id:data.Id);
                if (crmDeleteTaskTypeResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception crmDeleteTaskTypeException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete TaskType");
            }
        }
    }
}
