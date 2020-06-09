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
    public partial class AddApplicationRoleComponent : ComponentBase
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

        IdentityRole _role;
        protected IdentityRole role
        {
            get
            {
                return _role;
            }
            set
            {
                if(!object.Equals(_role, value))
                {
                    _role = value;
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
            role = new IdentityRole();
        }

        protected async System.Threading.Tasks.Task Form0Submit(IdentityRole args)
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

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
