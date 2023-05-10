using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace CRMBlazorWasmRBS.Client.Pages
{
    public partial class ApplicationUsers
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        protected IEnumerable<CRMBlazorWasmRBS.Server.Models.ApplicationUser> users;
        protected RadzenDataGrid<CRMBlazorWasmRBS.Server.Models.ApplicationUser> grid0;
        protected string error;
        protected bool errorVisible;

        [Inject]
        protected SecurityService Security { get; set; }

        protected override async Task OnInitializedAsync()
        {
            users = await Security.GetUsers();
        }

        protected async Task AddClick()
        {
            await DialogService.OpenAsync<AddApplicationUser>("Add Application User");

            users = await Security.GetUsers();
        }

        protected async Task RowSelect(CRMBlazorWasmRBS.Server.Models.ApplicationUser user)
        {
            await DialogService.OpenAsync<EditApplicationUser>("Edit Application User", new Dictionary<string, object>{ {"Id", user.Id} });

            users = await Security.GetUsers();
        }

        protected async Task DeleteClick(CRMBlazorWasmRBS.Server.Models.ApplicationUser user)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this user?") == true)
                {
                    await Security.DeleteUser($"{user.Id}");

                    users = await Security.GetUsers();
                }
            }
            catch (Exception ex)
            {
                errorVisible = true;
                error = ex.Message;
            }
        }
    }
}