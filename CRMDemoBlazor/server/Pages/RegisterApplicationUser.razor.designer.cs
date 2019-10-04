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
    public partial class RegisterApplicationUserComponent : ComponentBase
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

        protected RadzenTextBox userName;

        protected RadzenRequiredValidator emailRequiredValidator;

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

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            Load();
        }

        protected async void Load()
        {
            user = new ApplicationUser();;
        }

        protected async void Form0Submit(dynamic args)
        {
            DialogService.Close();
        }

        protected async void UndefinedClick(MouseEventArgs args)
        {
            DialogService.Close();
        }
    }
}
