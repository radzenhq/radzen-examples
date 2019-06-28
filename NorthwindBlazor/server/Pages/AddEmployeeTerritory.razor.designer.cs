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
    public partial class AddEmployeeTerritoryComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenTemplateForm<EmployeeTerritory> form0;

        protected RadzenLabel label1;

        protected RadzenDropDown employeeId;

        protected RadzenRequiredValidator employeeIdRequiredValidator;

        protected RadzenLabel label2;

        protected RadzenDropDown territoryId;

        protected RadzenRequiredValidator territoryIdRequiredValidator;

        protected RadzenButton button1;

        protected RadzenButton button2;

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

        EmployeeTerritory _employeeterritory;
        protected EmployeeTerritory employeeterritory
        {
            get
            {
                return _employeeterritory;
            }
            set
            {
                if(_employeeterritory != value)
                {
                    _employeeterritory = value;
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

            var northwindGetTerritoriesResult = await Northwind.GetTerritories();
                getTerritoriesResult = northwindGetTerritoriesResult;

            employeeterritory = new EmployeeTerritory();
        }

        protected async void Form0Submit(EmployeeTerritory args)
        {
            var northwindCreateEmployeeTerritoryResult = await Northwind.CreateEmployeeTerritory(employeeterritory);
                DialogService.Close(employeeterritory);
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
