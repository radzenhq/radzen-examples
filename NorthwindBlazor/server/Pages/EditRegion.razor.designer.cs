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
    public partial class EditRegionComponent : ComponentBase
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
        public dynamic RegionID { get; set; }

        NorthwindBlazor.Models.Northwind.Region _region;
        protected NorthwindBlazor.Models.Northwind.Region region
        {
            get
            {
                return _region;
            }
            set
            {
                if(!object.Equals(_region, value))
                {
                    _region = value;
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
            var northwindGetRegionByRegionIdResult = await Northwind.GetRegionByRegionId(int.Parse($"{RegionID}"));
            region = northwindGetRegionByRegionIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(NorthwindBlazor.Models.Northwind.Region args)
        {
            try
            {
                var northwindUpdateRegionResult = await Northwind.UpdateRegion(int.Parse($"{RegionID}"), region);
                DialogService.Close(region);
            }
            catch (Exception northwindUpdateRegionException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update Region");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
