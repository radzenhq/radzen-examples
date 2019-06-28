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
    public partial class EditRolePermissionComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        [Parameter]
        protected string RoleName { get; set; }

        [Parameter]
        protected string PermissionId { get; set; }

        protected RadzenContent content1;

        protected RadzenLabel closeLabel;

        protected RadzenButton closeButton;

        protected RadzenTemplateForm<RolePermission> form0;

        protected RadzenLabel label2;

        protected RadzenTextBox roleName;

        protected RadzenRequiredValidator roleNameRequiredValidator;

        protected RadzenLabel label3;

        protected RadzenTextBox permissionId;

        protected RadzenRequiredValidator permissionIdRequiredValidator;

        protected RadzenButton button2;

        protected RadzenButton button3;

        bool _canEdit;
        protected bool canEdit
        {
            get
            {
                return _canEdit;
            }
            set
            {
                if(_canEdit != value)
                {
                    _canEdit = value;
                    Invoke(() => { StateHasChanged(); });
                }
            }
        }

        RolePermission _rolepermission;
        protected RolePermission rolepermission
        {
            get
            {
                return _rolepermission;
            }
            set
            {
                if(_rolepermission != value)
                {
                    _rolepermission = value;
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
            canEdit = true;

            var northwindGetRolePermissionByRoleNameAndPermissionIdResult = await Northwind.GetRolePermissionByRoleNameAndPermissionId($"{RoleName}", $"{PermissionId}");
                rolepermission = northwindGetRolePermissionByRoleNameAndPermissionIdResult;
        }

        protected async void CloseButtonClick(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async void Form0Submit(RolePermission args)
        {
            var northwindUpdateRolePermissionResult = await Northwind.UpdateRolePermission($"{RoleName}", $"{PermissionId}", rolepermission);
                DialogService.Close(rolepermission);
        }

        protected async void Button3Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
