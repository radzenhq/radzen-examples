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
    public partial class EmployeeTerritoriesModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenButton button0;

        protected RadzenGrid<EmployeeTerritory> grid0;

        protected RadzenButton gridDeleteButton;

        IEnumerable<EmployeeTerritory> _getEmployeeTerritoriesResult;
        protected IEnumerable<EmployeeTerritory> getEmployeeTerritoriesResult
        {
            get
            {
                return _getEmployeeTerritoriesResult;
            }
            set
            {
                if(_getEmployeeTerritoriesResult != value)
                {
                    _getEmployeeTerritoriesResult = value;
                    StateHasChanged();
                }
            }
        }

        int _getEmployeeTerritoriesCount;
        protected int getEmployeeTerritoriesCount
        {
            get
            {
                return _getEmployeeTerritoriesCount;
            }
            set
            {
                if(_getEmployeeTerritoriesCount != value)
                {
                    _getEmployeeTerritoriesCount = value;
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
            UriHelper.NavigateTo("AddEmployeeTerritory");
        }

        protected async void Grid0LoadData(LoadDataArgs args)
        {
            var northwindGetEmployeeTerritoriesResult = await Northwind.GetEmployeeTerritories(null, args.Top, args.Skip, $"{args.OrderBy}", args.Top != null && args.Skip != null, $"Employee,Territory", null, null);
                getEmployeeTerritoriesResult = northwindGetEmployeeTerritoriesResult.Data;

                getEmployeeTerritoriesCount = northwindGetEmployeeTerritoriesResult.Count;
        }

        protected async void Grid0RowSelect(EmployeeTerritory args)
        {
            UriHelper.NavigateTo($"EditEmployeeTerritory/{args.EmployeeID}/{args.TerritoryID}");
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, EmployeeTerritory data)
        {
            var northwindDeleteEmployeeTerritoryResult = await Northwind.DeleteEmployeeTerritory(data.EmployeeID, $"{data.TerritoryID}");
                if (northwindDeleteEmployeeTerritoryResult.IsSuccessStatusCode) {
                    grid0.Reload();
}
        }
    }
}
