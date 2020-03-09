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
    public partial class EmployeesComponent : ComponentBase
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

        protected RadzenGrid<NorthwindBlazor.Models.Northwind.Employee> grid0;

        IEnumerable<NorthwindBlazor.Models.Northwind.Employee> _getEmployeesResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.Employee> getEmployeesResult
        {
            get
            {
                return _getEmployeesResult;
            }
            set
            {
                if(!object.Equals(_getEmployeesResult, value))
                {
                    _getEmployeesResult = value;
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
            var northwindGetEmployeesResult = await Northwind.GetEmployees();
            getEmployeesResult = northwindGetEmployeesResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddEmployee>("Add Employee", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(NorthwindBlazor.Models.Northwind.Employee args)
        {
            var result = await DialogService.OpenAsync<EditEmployee>("Edit Employee", new Dictionary<string, object>() { {"EmployeeID", args.EmployeeID} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var northwindDeleteEmployeeResult = await Northwind.DeleteEmployee(data.EmployeeID);
                if (northwindDeleteEmployeeResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception northwindDeleteEmployeeException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Employee");
            }
        }
    }
}
