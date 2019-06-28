using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Layouts;
using Radzen;
using Radzen.Blazor;
using NorthwindBlazor.Models.Northwind;

namespace NorthwindBlazor.Layouts
{
    public partial class MainLayoutComponent : LayoutComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenHeader header0;

        protected RadzenSidebarToggle sidebarToggle0;

        protected RadzenLabel label0;

        protected RadzenBody body0;

        protected RadzenContentContainer main;

        protected RadzenSidebar sidebar0;

        protected RadzenPanelMenu panelmenu0;

        protected RadzenFooter footer0;

        protected RadzenLabel footerText;

        protected override async Task OnInitAsync()
        {
        }
                

        protected async void SidebarToggle0Click(dynamic args)
        {
            sidebar0.Toggle();

            body0.Toggle();
        }
    }
}
