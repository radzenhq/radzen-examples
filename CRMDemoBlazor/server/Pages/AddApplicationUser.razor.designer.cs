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
    public partial class AddApplicationUserComponent : ComponentBase
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

        protected RadzenTemplateForm<ApplicationUser> form0;

        protected RadzenTextBox email;

        protected RadzenRequiredValidator emailRequiredValidator;

        protected RadzenLabel label0;

        protected RadzenTextBox firstName;

        protected RadzenRequiredValidator requiredValidator0;

        protected RadzenLabel label13;

        protected RadzenTextBox lastName;

        protected RadzenRequiredValidator requiredValidator1;

        protected RadzenLabel label2;

        protected RadzenFileInput picture;

        protected RadzenRequiredValidator requiredValidator2;

        protected RadzenDropDown roleNames;

        protected RadzenPassword password;

        protected RadzenRequiredValidator passwordRequiredValidator;

        protected RadzenPassword confirmPassword;

        protected RadzenRequiredValidator confirmPasswordRequiredValidator;

        ApplicationUser _user;
        protected ApplicationUser user
        {
            get
            {
                return _user;
            }
            set
            {
                if(_user != value)
                {
                    _user = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<dynamic> _roles;
        protected IEnumerable<dynamic> roles
        {
            get
            {
                return _roles;
            }
            set
            {
                if(_roles != value)
                {
                    _roles = value;
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
            user = new ApplicationUser();

            var securityGetRolesResult = await Security.GetRoles();
            roles = securityGetRolesResult;
        }

        protected async void Form0Submit(dynamic args)
        {
            try
            {
                var securityCreateUserResult = await Security.CreateUser(args);
                DialogService.Close();
            }
            catch (Exception securityCreateUserException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Cannot add user", $"{securityCreateUserException.Message}");
            }
        }

        protected async void UndefinedClick(MouseEventArgs args)
        {
            DialogService.Close();
        }
    }
}
