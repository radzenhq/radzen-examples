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
    public partial class EmployeesComponent : ComponentBase
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

        protected RadzenGrid<Employee> grid0;

        protected RadzenButton gridDeleteButton;

        IEnumerable<Employee> _getEmployeesResult;
        protected IEnumerable<Employee> getEmployeesResult
        {
            get
            {
                return _getEmployeesResult;
            }
            set
            {
                if(_getEmployeesResult != value)
                {
                    _getEmployeesResult = value;
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
            var northwindGetEmployeesResult = await Northwind.GetEmployees();
                getEmployeesResult = northwindGetEmployeesResult;
        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddEmployee>("Add Employee", null);
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(Employee args)
        {
            var result = await DialogService.OpenAsync<EditEmployee>("Edit Employee", new Dictionary<string, object>() { {"EmployeeID", $"{args.EmployeeID}"} });
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Employee data)
        {
            var northwindDeleteEmployeeResult = await Northwind.DeleteEmployee(data.EmployeeID);
                if (northwindDeleteEmployeeResult != null) {
                    grid0.Reload();
}
        }
    }
}
