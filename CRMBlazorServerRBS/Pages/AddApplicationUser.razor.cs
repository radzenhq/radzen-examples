using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace CRMBlazorServerRBS.Pages
{
    public partial class AddApplicationUser
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

        protected IEnumerable<CRMBlazorServerRBS.Models.ApplicationRole> roles;
        protected CRMBlazorServerRBS.Models.ApplicationUser user;
        protected IEnumerable<string> userRoles = Enumerable.Empty<string>();
        protected string error;
        protected bool errorVisible;

        [Inject]
        protected SecurityService Security { get; set; }

        protected override async Task OnInitializedAsync()
        {
            user = new CRMBlazorServerRBS.Models.ApplicationUser();

            roles = await Security.GetRoles();
        }

        protected async Task FormSubmit(CRMBlazorServerRBS.Models.ApplicationUser user)
        {
            try
            {
                user.Roles = roles.Where(role => userRoles.Contains(role.Id)).ToList();
                await Security.CreateUser(user);
                DialogService.Close(null);
            }
            catch (Exception ex)
            {
                errorVisible = true;
                error = ex.Message;
            }
        }

        protected async Task CancelClick()
        {
            DialogService.Close(null);
        }
    }
}