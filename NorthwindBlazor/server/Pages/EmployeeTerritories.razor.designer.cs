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
    public partial class EmployeeTerritoriesComponent : ComponentBase
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

        protected RadzenGrid<NorthwindBlazor.Models.Northwind.EmployeeTerritory> grid0;

        IEnumerable<NorthwindBlazor.Models.Northwind.EmployeeTerritory> _getEmployeeTerritoriesResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.EmployeeTerritory> getEmployeeTerritoriesResult
        {
            get
            {
                return _getEmployeeTerritoriesResult;
            }
            set
            {
                if(!object.Equals(_getEmployeeTerritoriesResult, value))
                {
                    _getEmployeeTerritoriesResult = value;
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
            var northwindGetEmployeeTerritoriesResult = await Northwind.GetEmployeeTerritories();
            getEmployeeTerritoriesResult = northwindGetEmployeeTerritoriesResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddEmployeeTerritory>("Add Employee Territory", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(NorthwindBlazor.Models.Northwind.EmployeeTerritory args)
        {
            var result = await DialogService.OpenAsync<EditEmployeeTerritory>("Edit Employee Territory", new Dictionary<string, object>() { {"EmployeeID", args.EmployeeID}, {"TerritoryID", args.TerritoryID} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var northwindDeleteEmployeeTerritoryResult = await Northwind.DeleteEmployeeTerritory(data.EmployeeID, $"{data.TerritoryID}");
                if (northwindDeleteEmployeeTerritoryResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception northwindDeleteEmployeeTerritoryException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete EmployeeTerritory");
            }
        }
    }
}
