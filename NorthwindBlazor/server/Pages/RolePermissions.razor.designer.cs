using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using NorthwindBlazor.Models.Northwind;

namespace NorthwindBlazor.Pages
{
    public partial class RolePermissionsComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenButton button0;

        protected RadzenGrid<RolePermission> grid0;

        protected RadzenButton gridDeleteButton;

        IEnumerable<RolePermission> _getRolePermissionsResult;
        protected IEnumerable<RolePermission> getRolePermissionsResult
        {
            get
            {
                return _getRolePermissionsResult;
            }
            set
            {
                if(_getRolePermissionsResult != value)
                {
                    _getRolePermissionsResult = value;
                    Invoke(() => { StateHasChanged(); });
                }
            }
        }

        protected override async Task OnInitAsync()
        {
            await Task.Run(Load);
        }

        protected async void Load()
        {
            var northwindGetRolePermissionsResult = await Northwind.GetRolePermissions();
                getRolePermissionsResult = northwindGetRolePermissionsResult;
        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddRolePermission>("Add Role Permission", null);
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(RolePermission args)
        {
            var result = await DialogService.OpenAsync<EditRolePermission>("Edit Role Permission", new Dictionary<string, object>() { {"RoleName", $"{args.RoleName}"}, {"PermissionId", $"{args.PermissionId}"} });
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, RolePermission data)
        {
            var northwindDeleteRolePermissionResult = await Northwind.DeleteRolePermission($"{data.RoleName}", $"{data.PermissionId}");
                if (northwindDeleteRolePermissionResult != null) {
                    grid0.Reload();
}
        }
    }
}
