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
    public partial class RegionsComponent : ComponentBase
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

        protected RadzenGrid<Region> grid0;

        protected RadzenButton gridDeleteButton;

        IEnumerable<Region> _getRegionsResult;
        protected IEnumerable<Region> getRegionsResult
        {
            get
            {
                return _getRegionsResult;
            }
            set
            {
                if(_getRegionsResult != value)
                {
                    _getRegionsResult = value;
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
            var northwindGetRegionsResult = await Northwind.GetRegions();
                getRegionsResult = northwindGetRegionsResult;
        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddRegion>("Add Region", null);
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(Region args)
        {
            var result = await DialogService.OpenAsync<EditRegion>("Edit Region", new Dictionary<string, object>() { {"RegionID", $"{args.RegionID}"} });
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Region data)
        {
            var northwindDeleteRegionResult = await Northwind.DeleteRegion(data.RegionID);
                if (northwindDeleteRegionResult != null) {
                    grid0.Reload();
}
        }
    }
}
