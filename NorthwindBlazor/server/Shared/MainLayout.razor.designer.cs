using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using NorthwindBlazor.Models.Northwind;
namespace NorthwindBlazor.Layouts
{
    public partial class MainLayoutComponent : LayoutComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenBody body0;

        protected RadzenSidebar sidebar0;

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
        }


        protected async System.Threading.Tasks.Task SidebarToggle0Click(dynamic args)
        {
            sidebar0.Toggle();

            body0.Toggle();
        }
    }
}
