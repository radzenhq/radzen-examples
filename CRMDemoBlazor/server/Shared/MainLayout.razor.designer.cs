using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using RadzenCrm.Models.Crm;
using Microsoft.AspNetCore.Identity;
using RadzenCrm.Models;
using Microsoft.JSInterop;

namespace RadzenCrm.Layouts
{
    public partial class MainLayoutComponent : LayoutComponentBase
    {
        [Inject]
        protected Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        [Inject]
        protected CrmService Crm { get; set; }


        protected RadzenBody body0;

        protected RadzenSidebar sidebar0;

        string _Culture;
        protected string Culture
        {
            get
            {
                return _Culture;
            }
            set
            {
                if(!object.Equals(_Culture, value))
                {
                    _Culture = value;
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
            Culture = "";

            Culture = await JSRuntime.InvokeAsync<string>("Radzen.getCulture");
        }

        protected async System.Threading.Tasks.Task SidebarToggle0Click(dynamic args)
        {
            sidebar0.Toggle();

            body0.Toggle();
        }

        protected async System.Threading.Tasks.Task Profilemenu0Click(dynamic args)
        {
            if (args.Value == "Logout") {
                await Security.Logout();
}
        }
    }
}
