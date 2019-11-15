using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using BlazorMultiTenant.Models.Sample;
using Microsoft.AspNetCore.Identity;
using BlazorMultiTenant.Models;

namespace BlazorMultiTenant.Pages
{
    public partial class ApplicationRolesComponent : ComponentBase
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
        protected SampleService Sample { get; set; }


        protected RadzenGrid<IdentityRole> grid0;

        IEnumerable<IdentityRole> _roles;
        protected IEnumerable<IdentityRole> roles
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
            var securityGetRolesResult = await Security.GetRoles();
            roles = securityGetRolesResult;
        }

        protected async void Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddApplicationRole>("Add Application Role", null);
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(MouseEventArgs args, IdentityRole data)
        {
            try
            {
                var securityDeleteRoleResult = await Security.DeleteRole($"{data.Id}");
                if (securityDeleteRoleResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception securityDeleteRoleException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete role");
            }
        }
    }
}
