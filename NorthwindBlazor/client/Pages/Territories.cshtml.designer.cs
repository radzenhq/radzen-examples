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
    public partial class TerritoriesModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenButton button0;

        protected RadzenGrid<Territory> grid0;

        protected RadzenButton gridDeleteButton;

        IEnumerable<Territory> _getTerritoriesResult;
        protected IEnumerable<Territory> getTerritoriesResult
        {
            get
            {
                return _getTerritoriesResult;
            }
            set
            {
                if(_getTerritoriesResult != value)
                {
                    _getTerritoriesResult = value;
                    StateHasChanged();
                }
            }
        }

        int _getTerritoriesCount;
        protected int getTerritoriesCount
        {
            get
            {
                return _getTerritoriesCount;
            }
            set
            {
                if(_getTerritoriesCount != value)
                {
                    _getTerritoriesCount = value;
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
            UriHelper.NavigateTo("AddTerritory");
        }

        protected async void Grid0LoadData(LoadDataArgs args)
        {
            var northwindGetTerritoriesResult = await Northwind.GetTerritories(null, args.Top, args.Skip, $"{args.OrderBy}", args.Top != null && args.Skip != null, $"Region", null, null);
                getTerritoriesResult = northwindGetTerritoriesResult.Data;

                getTerritoriesCount = northwindGetTerritoriesResult.Count;
        }

        protected async void Grid0RowSelect(Territory args)
        {
            UriHelper.NavigateTo($"EditTerritory/{args.TerritoryID}");
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Territory data)
        {
            var northwindDeleteTerritoryResult = await Northwind.DeleteTerritory($"{data.TerritoryID}");
                if (northwindDeleteTerritoryResult.IsSuccessStatusCode) {
                    grid0.Reload();
}
        }
    }
}
