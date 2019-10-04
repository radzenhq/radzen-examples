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
    public partial class ProfileComponent : ComponentBase
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

        protected RadzenHeading pageTitle;

        protected RadzenTemplateForm<ApplicationUser> form0;

        protected RadzenPassword oldPassword;

        protected RadzenRequiredValidator oldPasswordRequiredValidator;

        protected RadzenPassword newPassword;

        protected RadzenRequiredValidator newPasswordRequiredValidator;

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
            if (Security.User != null)
            {
                var securityGetUserByIdResult = await Security.GetUserById($"{Security.User.Id}");
            user = securityGetUserByIdResult;
            }
        }

        protected async void UndefinedClick(MouseEventArgs args)
        {
            DialogService.Close();
        }
    }
}
