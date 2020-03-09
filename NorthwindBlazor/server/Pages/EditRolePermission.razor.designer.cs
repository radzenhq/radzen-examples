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
    public partial class EditRolePermissionComponent : ComponentBase
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

        [Parameter]
        public dynamic RoleName { get; set; }

        [Parameter]
        public dynamic PermissionId { get; set; }

        NorthwindBlazor.Models.Northwind.RolePermission _rolepermission;
        protected NorthwindBlazor.Models.Northwind.RolePermission rolepermission
        {
            get
            {
                return _rolepermission;
            }
            set
            {
                if(!object.Equals(_rolepermission, value))
                {
                    _rolepermission = value;
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
            var northwindGetRolePermissionByRoleNameAndPermissionIdResult = await Northwind.GetRolePermissionByRoleNameAndPermissionId($"{RoleName}", $"{PermissionId}");
            rolepermission = northwindGetRolePermissionByRoleNameAndPermissionIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(NorthwindBlazor.Models.Northwind.RolePermission args)
        {
            try
            {
                var northwindUpdateRolePermissionResult = await Northwind.UpdateRolePermission($"{RoleName}", $"{PermissionId}", rolepermission);
                DialogService.Close(rolepermission);
            }
            catch (Exception northwindUpdateRolePermissionException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update RolePermission");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
