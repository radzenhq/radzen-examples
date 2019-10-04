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
    public partial class AddTaskComponent : ComponentBase
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

        protected RadzenTemplateForm<RadzenCrm.Models.Crm.Task> form0;

        protected RadzenTextBox title;

        protected RadzenDropDown opportunityId;

        protected RadzenRequiredValidator opportunityIdRequiredValidator;

        protected RadzenDatePicker dueDate;

        protected RadzenRequiredValidator dueDateRequiredValidator;

        protected RadzenDropDown typeId;

        protected RadzenRequiredValidator typeIdRequiredValidator;

        protected RadzenDropDown statusId;

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
            var crmGetOpportunitiesResult = await Crm.GetOpportunities();
            getOpportunitiesResult = crmGetOpportunitiesResult;

            var crmGetTaskTypesResult = await Crm.GetTaskTypes();
            getTaskTypesResult = crmGetTaskTypesResult;

            var crmGetTaskStatusesResult = await Crm.GetTaskStatuses();
            getTaskStatusesResult = crmGetTaskStatusesResult;

            task = new RadzenCrm.Models.Crm.Task();
        }

        protected async void Form0Submit(RadzenCrm.Models.Crm.Task args)
        {
            try
            {
                var crmCreateTaskResult = await Crm.CreateTask(task);
                DialogService.Close(task);
            }
            catch (Exception crmCreateTaskException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new Task!");
            }
        }

        protected async void UndefinedClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
