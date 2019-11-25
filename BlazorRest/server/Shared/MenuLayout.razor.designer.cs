using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace Blazor.Layouts
{
    public partial class MenuLayoutComponent : LayoutComponentBase
    {
        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        protected RadzenHeader header0;

        protected RadzenSidebarToggle sidebarToggle0;

        protected RadzenLabel label0;

        protected RadzenMenu menu0;

        protected RadzenBody body0;

        protected RadzenContentContainer main;

        protected RadzenFooter footer0;

        protected RadzenLabel footerText;

        protected override async Task OnInitializedAsync()
        {
        }

    }
}
