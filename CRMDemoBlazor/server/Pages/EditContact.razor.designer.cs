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
    public partial class EditContactComponent : ComponentBase
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

        protected RadzenTemplateForm<RadzenCrm.Models.Crm.Contact> form0;

        protected RadzenTextBox email;

        protected RadzenRequiredValidator emailRequiredValidator;

        protected RadzenTextBox company;

        protected RadzenTextBox lastName;

        protected RadzenTextBox firstName;

        protected RadzenTextBox phone;

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

        RadzenCrm.Models.Crm.Contact _contact;
        protected RadzenCrm.Models.Crm.Contact contact
        {
            get
            {
                return _contact;
            }
            set
            {
                if(_contact != value)
                {
                    _contact = value;
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

            var crmGetContactByIdResult = await Crm.GetContactById(int.Parse(Id));
            contact = crmGetContactByIdResult;
        }

        protected async void CloseButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async void Form0Submit(RadzenCrm.Models.Crm.Contact args)
        {
            try
            {
                var crmUpdateContactResult = await Crm.UpdateContact(int.Parse(Id), contact);
                DialogService.Close(contact);
            }
            catch (Exception crmUpdateContactException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update Contact");
            }
        }

        protected async void UndefinedClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
