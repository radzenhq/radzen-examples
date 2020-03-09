using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using NorthwindBlazor.Models.Northwind;
using Microsoft.EntityFrameworkCore;

namespace NorthwindBlazor.Pages
{
    public partial class RolePermissionsComponent : ComponentBase
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
        protected NorthwindService Northwind { get; set; }

        protected RadzenGrid<NorthwindBlazor.Models.Northwind.RolePermission> grid0;

        IEnumerable<NorthwindBlazor.Models.Northwind.RolePermission> _getRolePermissionsResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.RolePermission> getRolePermissionsResult
        {
            get
            {
                return _getRolePermissionsResult;
            }
            set
            {
                if(!object.Equals(_getRolePermissionsResult, value))
                {
                    _getRolePermissionsResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var northwindGetRolePermissionsResult = await Northwind.GetRolePermissions();
            getRolePermissionsResult = northwindGetRolePermissionsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddRolePermission>("Add Role Permission", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(NorthwindBlazor.Models.Northwind.RolePermission args)
        {
            var result = await DialogService.OpenAsync<EditRolePermission>("Edit Role Permission", new Dictionary<string, object>() { {"RoleName", args.RoleName}, {"PermissionId", args.PermissionId} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var northwindDeleteRolePermissionResult = await Northwind.DeleteRolePermission($"{data.RoleName}", $"{data.PermissionId}");
                if (northwindDeleteRolePermissionResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception northwindDeleteRolePermissionException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete RolePermission");
            }
        }
    }
}
