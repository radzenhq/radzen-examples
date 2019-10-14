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

        protected async void TemplateForm0Submit(RadzenCrm.Models.ApplicationUser args)
        {
            var securityUpdateUserResult = await Security.UpdateUser($"{Security.User.Id}", user);
                NotificationService.Notify(NotificationSeverity.Success, $"Success", $"Personal data updated successfully!");
        }

        protected async void Button1Click(MouseEventArgs args)
        {
            DialogService.Close();
        }
    }
}
