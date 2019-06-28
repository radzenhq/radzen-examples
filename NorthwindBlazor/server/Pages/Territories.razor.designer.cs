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
    public partial class TerritoriesComponent : ComponentBase
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
            var northwindGetTerritoriesResult = await Northwind.GetTerritories();
                getTerritoriesResult = northwindGetTerritoriesResult;
        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddTerritory>("Add Territory", null);
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(Territory args)
        {
            var result = await DialogService.OpenAsync<EditTerritory>("Edit Territory", new Dictionary<string, object>() { {"TerritoryID", $"{args.TerritoryID}"} });
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Territory data)
        {
            var northwindDeleteTerritoryResult = await Northwind.DeleteTerritory($"{data.TerritoryID}");
                if (northwindDeleteTerritoryResult != null) {
                    grid0.Reload();
}
        }
    }
}
