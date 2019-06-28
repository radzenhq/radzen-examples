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
    public partial class AddRolePermissionComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenTemplateForm<RolePermission> form0;

        protected RadzenLabel label1;

        protected RadzenTextBox roleName;

        protected RadzenRequiredValidator roleNameRequiredValidator;

        protected RadzenLabel label2;

        protected RadzenTextBox permissionId;

        protected RadzenRequiredValidator permissionIdRequiredValidator;

        protected RadzenButton button1;

        protected RadzenButton button2;

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
            rolepermission = new RolePermission();
        }

        protected async void Form0Submit(RolePermission args)
        {
            var northwindCreateRolePermissionResult = await Northwind.CreateRolePermission(rolepermission);
                DialogService.Close(rolepermission);
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
