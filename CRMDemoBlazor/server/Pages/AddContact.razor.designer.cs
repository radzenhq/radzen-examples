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
    public partial class AddContactComponent : ComponentBase
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
            contact = new RadzenCrm.Models.Crm.Contact();
        }

        protected async void Form0Submit(RadzenCrm.Models.Crm.Contact args)
        {
            try
            {
                var crmCreateContactResult = await Crm.CreateContact(contact);
                DialogService.Close(contact);
            }
            catch (Exception crmCreateContactException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new Contact!");
            }
        }

        protected async void Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
