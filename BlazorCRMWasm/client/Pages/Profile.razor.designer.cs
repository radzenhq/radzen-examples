using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using BlazorCrmWasm.Models.Crm;
using Microsoft.AspNetCore.Identity;
using BlazorCrmWasm.Models;
using BlazorCrmWasm.Client.Pages;

namespace BlazorCrmWasm.Pages
{
    public partial class ProfileComponent : ComponentBase
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

        ApplicationUser _user;
        protected ApplicationUser user
        {
            get
            {
                return _user;
            }
            set
            {
                if(!object.Equals(_user, value))
                {
                    _user = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }
        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            if (!await Security.IsAuthenticatedAsync())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                await Load();
            }

        }
        protected async System.Threading.Tasks.Task Load()
        {
            if (Security.User != null)
            {
                var securityGetUserByIdResult = await Security.GetUserById($"{Security.User.Id}");
                user = securityGetUserByIdResult;
            }
        }

        protected async System.Threading.Tasks.Task TemplateForm0Submit(ApplicationUser args)
        {
            var securityUpdateUserResult = await Security.UpdateUser($"{Security.User.Id}", user);
                NotificationService.Notify(NotificationSeverity.Success, $"Success", $"Personal data updated successfully!");
        }

        protected async System.Threading.Tasks.Task Button1Click(MouseEventArgs args)
        {
            DialogService.Close();
            await JSRuntime.InvokeAsync<string>("window.history.back");
        }
    }
}
