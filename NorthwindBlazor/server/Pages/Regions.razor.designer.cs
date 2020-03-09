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
    public partial class RegionsComponent : ComponentBase
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

        protected RadzenGrid<NorthwindBlazor.Models.Northwind.Region> grid0;

        IEnumerable<NorthwindBlazor.Models.Northwind.Region> _getRegionsResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.Region> getRegionsResult
        {
            get
            {
                return _getRegionsResult;
            }
            set
            {
                if(!object.Equals(_getRegionsResult, value))
                {
                    _getRegionsResult = value;
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
            var northwindGetRegionsResult = await Northwind.GetRegions();
            getRegionsResult = northwindGetRegionsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddRegion>("Add Region", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(NorthwindBlazor.Models.Northwind.Region args)
        {
            var result = await DialogService.OpenAsync<EditRegion>("Edit Region", new Dictionary<string, object>() { {"RegionID", args.RegionID} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var northwindDeleteRegionResult = await Northwind.DeleteRegion(data.RegionID);
                if (northwindDeleteRegionResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception northwindDeleteRegionException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Region");
            }
        }
    }
}
