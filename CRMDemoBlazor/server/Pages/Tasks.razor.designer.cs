using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using RadzenCrm.Models.Crm;
using Microsoft.AspNetCore.Identity;
using RadzenCrm.Models;

namespace RadzenCrm.Pages
{
    public partial class TasksComponent : ComponentBase
    {
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


        protected RadzenGrid<RadzenCrm.Models.Crm.Task> grid0;

        IEnumerable<RadzenCrm.Models.Crm.Task> _getTasksResult;
        protected IEnumerable<RadzenCrm.Models.Crm.Task> getTasksResult
        {
            get
            {
                return _getTasksResult;
            }
            set
            {
                if(_getTasksResult != value)
                {
                    _getTasksResult = value;
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
            var crmGetTasksResult = await Crm.GetTasks();
            getTasksResult = crmGetTasksResult;
        }

        protected async void Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddTask>("Add Task", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(RadzenCrm.Models.Crm.Task args)
        {
            var result = await DialogService.OpenAsync<EditTask>("Edit Task", new Dictionary<string, object>() { {"Id", $"{args.Id}"} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(MouseEventArgs args, RadzenCrm.Models.Crm.Task data)
        {
            try
            {
                var crmDeleteTaskResult = await Crm.DeleteTask(data.Id);
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
