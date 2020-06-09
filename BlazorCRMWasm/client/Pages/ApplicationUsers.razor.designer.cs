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
    public partial class ApplicationUsersComponent : ComponentBase
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

        protected RadzenGrid<ApplicationUser> grid0;

        IEnumerable<ApplicationUser> _users;
        protected IEnumerable<ApplicationUser> users
        {
            get
            {
                return _users;
            }
            set
            {
                if(!object.Equals(_users, value))
                {
                    _users = value;
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
            var securityGetUsersResult = await Security.GetUsers();
            users = securityGetUsersResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddApplicationUser>("Add Application User", null);
            await Load();

            grid0.Reload();
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(ApplicationUser args)
        {
            var dialogResult = await DialogService.OpenAsync<EditApplicationUser>("Edit Application User", new Dictionary<string, object>() { {"Id", args.Id} });
            await Load();

            grid0.Reload();
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, ApplicationUser data)
        {
            try
            {
                var securityDeleteUserResult = await Security.DeleteUser($"{data.Id}");
                await Load();

                if (securityDeleteUserResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception securityDeleteUserException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete user");
            }
        }
    }
}
