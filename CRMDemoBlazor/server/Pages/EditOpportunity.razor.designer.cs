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
    public partial class EditOpportunityComponent : ComponentBase
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

        protected RadzenTemplateForm<RadzenCrm.Models.Crm.Opportunity> form0;

        protected dynamic amount;

        protected RadzenRequiredValidator amountRequiredValidator;

        protected RadzenTextBox userId;

        protected RadzenRequiredValidator userIdRequiredValidator;

        protected RadzenDropDown contactId;

        protected RadzenRequiredValidator contactIdRequiredValidator;

        protected RadzenDropDown statusId;

        protected RadzenRequiredValidator statusIdRequiredValidator;

        protected RadzenDatePicker closeDate;

        protected RadzenRequiredValidator closeDateRequiredValidator;

        protected RadzenTextBox name;

        protected RadzenRequiredValidator nameRequiredValidator;

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

        RadzenCrm.Models.Crm.Opportunity _opportunity;
        protected RadzenCrm.Models.Crm.Opportunity opportunity
        {
            get
            {
                return _opportunity;
            }
            set
            {
                if(_opportunity != value)
                {
                    _opportunity = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<RadzenCrm.Models.Crm.Contact> _getContactsResult;
        protected IEnumerable<RadzenCrm.Models.Crm.Contact> getContactsResult
        {
            get
            {
                return _getContactsResult;
            }
            set
            {
                if(_getContactsResult != value)
                {
                    _getContactsResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<RadzenCrm.Models.Crm.OpportunityStatus> _getOpportunityStatusesResult;
        protected IEnumerable<RadzenCrm.Models.Crm.OpportunityStatus> getOpportunityStatusesResult
        {
            get
            {
                return _getOpportunityStatusesResult;
            }
            set
            {
                if(_getOpportunityStatusesResult != value)
                {
                    _getOpportunityStatusesResult = value;
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

            var crmGetOpportunityByIdResult = await Crm.GetOpportunityById(int.Parse(Id));
            opportunity = crmGetOpportunityByIdResult;

            var crmGetContactsResult = await Crm.GetContacts();
            getContactsResult = crmGetContactsResult;

            var crmGetOpportunityStatusesResult = await Crm.GetOpportunityStatuses();
            getOpportunityStatusesResult = crmGetOpportunityStatusesResult;
        }

        protected async void CloseButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async void Form0Submit(RadzenCrm.Models.Crm.Opportunity args)
        {
            try
            {
                var crmUpdateOpportunityResult = await Crm.UpdateOpportunity(int.Parse(Id), opportunity);
                DialogService.Close(opportunity);
            }
            catch (Exception crmUpdateOpportunityException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update Opportunity");
            }
        }

        protected async void UndefinedClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
