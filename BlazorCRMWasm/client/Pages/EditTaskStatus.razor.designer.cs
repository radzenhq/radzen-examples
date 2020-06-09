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
    public partial class EditTaskStatusComponent : ComponentBase
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

        [Parameter]
        public dynamic Id { get; set; }

        BlazorCrmWasm.Models.Crm.TaskStatus _taskstatus;
        protected BlazorCrmWasm.Models.Crm.TaskStatus taskstatus
        {
            get
            {
                return _taskstatus;
            }
            set
            {
                if(!object.Equals(_taskstatus, value))
                {
                    _taskstatus = value;
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
            var crmGetTaskStatusByIdResult = await Crm.GetTaskStatusById(id:Id);
            taskstatus = crmGetTaskStatusByIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(BlazorCrmWasm.Models.Crm.TaskStatus args)
        {
            try
            {
                var crmUpdateTaskStatusResult = await Crm.UpdateTaskStatus(id:Id, taskStatus:taskstatus);
                DialogService.Close(taskstatus);
            }
            catch (Exception crmUpdateTaskStatusException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update TaskStatus");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
