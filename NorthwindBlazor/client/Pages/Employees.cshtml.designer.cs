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
    public partial class EmployeesModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

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
                    StateHasChanged();
                }
            }
        }

        int _getEmployeesCount;
        protected int getEmployeesCount
        {
            get
            {
                return _getEmployeesCount;
            }
            set
            {
                if(_getEmployeesCount != value)
                {
                    _getEmployeesCount = value;
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
            UriHelper.NavigateTo("AddEmployee");
        }

        protected async void Grid0LoadData(LoadDataArgs args)
        {
            var northwindGetEmployeesResult = await Northwind.GetEmployees(null, args.Top, args.Skip, $"{args.OrderBy}", args.Top != null && args.Skip != null, $"Employee1", null, null);
                getEmployeesResult = northwindGetEmployeesResult.Data;

                getEmployeesCount = northwindGetEmployeesResult.Count;
        }

        protected async void Grid0RowSelect(Employee args)
        {
            UriHelper.NavigateTo($"EditEmployee/{args.EmployeeID}");
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Employee data)
        {
            var northwindDeleteEmployeeResult = await Northwind.DeleteEmployee(data.EmployeeID);
                if (northwindDeleteEmployeeResult.IsSuccessStatusCode) {
                    grid0.Reload();
}
        }
    }
}
