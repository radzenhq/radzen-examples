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
    public partial class EditTaskStatusComponent : ComponentBase
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


        [Parameter]
        public string Id { get; set; }

        protected RadzenContent content0;

        protected RadzenLabel closeLabel;

        protected RadzenButton closeButton;

        protected RadzenTemplateForm<RadzenCrm.Models.Crm.TaskStatus> form0;

        protected RadzenTextBox name;

        bool _canEdit;
        protected bool canEdit
        {
            get
            {
                return _canEdit;
            }
            set
            {
                if(_canEdit != value)
                {
                    _canEdit = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

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
            canEdit = true;

            var crmGetTaskStatusByIdResult = await Crm.GetTaskStatusById(int.Parse(Id));
            taskstatus = crmGetTaskStatusByIdResult;
        }

        protected async void CloseButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async void Form0Submit(RadzenCrm.Models.Crm.TaskStatus args)
        {
            try
            {
                var crmUpdateTaskStatusResult = await Crm.UpdateTaskStatus(int.Parse(Id), taskstatus);
                DialogService.Close(taskstatus);
            }
            catch (Exception crmUpdateTaskStatusException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update TaskStatus");
            }
        }

        protected async void UndefinedClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
