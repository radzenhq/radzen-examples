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
    public partial class EditTaskTypeComponent : ComponentBase
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

        protected RadzenTemplateForm<RadzenCrm.Models.Crm.TaskType> form0;

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

        RadzenCrm.Models.Crm.TaskType _tasktype;
        protected RadzenCrm.Models.Crm.TaskType tasktype
        {
            get
            {
                return _tasktype;
            }
            set
            {
                if(_tasktype != value)
                {
                    _tasktype = value;
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

            var crmGetTaskTypeByIdResult = await Crm.GetTaskTypeById(int.Parse(Id));
            tasktype = crmGetTaskTypeByIdResult;
        }

        protected async void CloseButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async void Form0Submit(RadzenCrm.Models.Crm.TaskType args)
        {
            try
            {
                var crmUpdateTaskTypeResult = await Crm.UpdateTaskType(int.Parse(Id), tasktype);
                DialogService.Close(tasktype);
            }
            catch (Exception crmUpdateTaskTypeException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update TaskType");
            }
        }

        protected async void UndefinedClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
