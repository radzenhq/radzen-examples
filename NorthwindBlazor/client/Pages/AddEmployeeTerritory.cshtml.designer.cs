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
    public partial class AddEmployeeTerritoryModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

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
                    StateHasChanged();
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
                    StateHasChanged();
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
            var northwindGetEmployeesResult = await Northwind.GetEmployees(null, null, null, null, null, null, null, null);
                getEmployeesResult = northwindGetEmployeesResult.Data;

            var northwindGetTerritoriesResult = await Northwind.GetTerritories(null, null, null, null, null, null, null, null);
                getTerritoriesResult = northwindGetTerritoriesResult.Data;

            employeeterritory = new EmployeeTerritory();
        }

        protected async void Form0Submit(EmployeeTerritory args)
        {
            var northwindCreateEmployeeTerritoryResult = await Northwind.CreateEmployeeTerritory(employeeterritory);
                UriHelper.NavigateTo("EmployeeTerritories");
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("EmployeeTerritories");
        }
    }
}
