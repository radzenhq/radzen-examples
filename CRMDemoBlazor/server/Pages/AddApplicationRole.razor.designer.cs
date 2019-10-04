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
    public partial class AddApplicationRoleComponent : ComponentBase
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

        protected RadzenTemplateForm<IdentityRole> form0;

        protected RadzenTextBox name;

        protected RadzenRequiredValidator nameRequiredValidator;

        IdentityRole _role;
        protected IdentityRole role
        {
            get
            {
                return _role;
            }
            set
            {
                if(_role != value)
                {
                    _role = value;
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
            role = new IdentityRole();
        }

        protected async void Form0Submit(dynamic args)
        {
            try
            {
                var securityCreateRoleResult = await Security.CreateRole(args);
                UriHelper.NavigateTo("application-roles");
            }
            catch (Exception securityCreateRoleException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Cannot create role", $"{securityCreateRoleException.Message}");
            }
        }

        protected async void UndefinedClick(MouseEventArgs args)
        {
            DialogService.Close();
        }
    }
}
