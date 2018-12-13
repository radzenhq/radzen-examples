using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using client.Shared;
using Radzen;
using Radzen.Blazor;
using NorthwindBlazor.Models.Northwind;

namespace NorthwindBlazor.App.Pages
{
    public partial class RegionsModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

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
                    StateHasChanged();
                }
            }
        }

        int _getRegionsCount;
        protected int getRegionsCount
        {
            get
            {
                return _getRegionsCount;
            }
            set
            {
                if(_getRegionsCount != value)
                {
                    _getRegionsCount = value;
                    StateHasChanged();
                }
            }
        }

        protected override async Task OnInitAsync()
        {
            Northwind.BasePath = UriHelper.GetBaseUri();

            await Task.Run(Load);
        }

        protected async void Load()
        {

        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("AddRegion");
        }

        protected async void Grid0LoadData(LoadDataArgs args)
        {
            var northwindGetRegionsResult = await Northwind.GetRegions(null, args.Top, args.Skip, $"{args.OrderBy}", args.Top != null && args.Skip != null, $"", null, null);
                getRegionsResult = northwindGetRegionsResult.Data;

                getRegionsCount = northwindGetRegionsResult.Count;
        }

        protected async void Grid0RowSelect(Region args)
        {
            UriHelper.NavigateTo($"EditRegion/{args.RegionID}");
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Region data)
        {
            var northwindDeleteRegionResult = await Northwind.DeleteRegion(data.RegionID);
                if (northwindDeleteRegionResult.IsSuccessStatusCode) {
                    grid0.Reload();
}
        }
    }
}
