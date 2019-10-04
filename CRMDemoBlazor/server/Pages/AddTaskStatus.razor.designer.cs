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
    public partial class AddTaskStatusComponent : ComponentBase
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


        protected RadzenContent content0;

        protected RadzenTemplateForm<RadzenCrm.Models.Crm.TaskStatus> form0;

        protected RadzenTextBox name;

        RadzenCrm.Models.Crm.TaskStatus _taskstatus;
        protected RadzenCrm.Models.Crm.TaskStatus taskstatus
        {
            get
            {
                return _taskstatus;
            }
            set
            {
                if(_taskstatus != value)
                {
                    _taskstatus = value;
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
            taskstatus = new RadzenCrm.Models.Crm.TaskStatus();
        }

        protected async void Form0Submit(RadzenCrm.Models.Crm.TaskStatus args)
        {
            try
            {
                var crmCreateTaskStatusResult = await Crm.CreateTaskStatus(taskstatus);
                DialogService.Close(taskstatus);
            }
            catch (Exception crmCreateTaskStatusException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new TaskStatus!");
            }
        }

        protected async void UndefinedClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
