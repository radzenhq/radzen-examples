using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using RadzenCrm.Models.Crm;
using Microsoft.AspNetCore.Identity;
using RadzenCrm.Models;

namespace RadzenCrm.Pages
{
    public partial class EditTaskComponent : ComponentBase
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

        RadzenCrm.Models.Crm.Task _task;
        protected RadzenCrm.Models.Crm.Task task
        {
            get
            {
                return _task;
            }
            set
            {
                if(_task != value)
                {
                    _task = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<RadzenCrm.Models.Crm.Opportunity> _getOpportunitiesResult;
        protected IEnumerable<RadzenCrm.Models.Crm.Opportunity> getOpportunitiesResult
        {
            get
            {
                return _getOpportunitiesResult;
            }
            set
            {
                if(_getOpportunitiesResult != value)
                {
                    _getOpportunitiesResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<RadzenCrm.Models.Crm.TaskType> _getTaskTypesResult;
        protected IEnumerable<RadzenCrm.Models.Crm.TaskType> getTaskTypesResult
        {
            get
            {
                return _getTaskTypesResult;
            }
            set
            {
                if(_getTaskTypesResult != value)
                {
                    _getTaskTypesResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<RadzenCrm.Models.Crm.TaskStatus> _getTaskStatusesResult;
        protected IEnumerable<RadzenCrm.Models.Crm.TaskStatus> getTaskStatusesResult
        {
            get
            {
                return _getTaskStatusesResult;
            }
            set
            {
                if(_getTaskStatusesResult != value)
                {
                    _getTaskStatusesResult = value;
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

            var crmGetTaskByIdResult = await Crm.GetTaskById(int.Parse($"{Id}"));
            task = crmGetTaskByIdResult;

            var crmGetOpportunitiesResult = await Crm.GetOpportunities();
            getOpportunitiesResult = crmGetOpportunitiesResult;

            var crmGetTaskTypesResult = await Crm.GetTaskTypes();
            getTaskTypesResult = crmGetTaskTypesResult;

            var crmGetTaskStatusesResult = await Crm.GetTaskStatuses();
            getTaskStatusesResult = crmGetTaskStatusesResult;
        }

        protected async void CloseButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async void Form0Submit(RadzenCrm.Models.Crm.Task args)
        {
            try
            {
                var crmUpdateTaskResult = await Crm.UpdateTask(int.Parse($"{Id}"), task);
                DialogService.Close(task);
            }
            catch (Exception crmUpdateTaskException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update Task");
            }
        }

        protected async void Button3Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
