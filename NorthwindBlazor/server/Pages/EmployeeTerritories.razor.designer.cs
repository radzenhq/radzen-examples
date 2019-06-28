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
    public partial class EmployeeTerritoriesComponent : ComponentBase
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
            var northwindGetEmployeeTerritoriesResult = await Northwind.GetEmployeeTerritories();
                getEmployeeTerritoriesResult = northwindGetEmployeeTerritoriesResult;
        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddEmployeeTerritory>("Add Employee Territory", null);
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(EmployeeTerritory args)
        {
            var result = await DialogService.OpenAsync<EditEmployeeTerritory>("Edit Employee Territory", new Dictionary<string, object>() { {"EmployeeID", $"{args.EmployeeID}"}, {"TerritoryID", $"{args.TerritoryID}"} });
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, EmployeeTerritory data)
        {
            var northwindDeleteEmployeeTerritoryResult = await Northwind.DeleteEmployeeTerritory(data.EmployeeID, $"{data.TerritoryID}");
                if (northwindDeleteEmployeeTerritoryResult != null) {
                    grid0.Reload();
}
        }
    }
}
