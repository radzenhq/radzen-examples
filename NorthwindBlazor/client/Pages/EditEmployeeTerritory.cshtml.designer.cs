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
    public partial class EditEmployeeTerritoryModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        [Parameter]
        protected string EmployeeID { get; set; }

        [Parameter]
        protected string TerritoryID { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenLabel closeLabel;

        protected RadzenButton closeButton;

        protected RadzenTemplateForm<EmployeeTerritory> form0;

        protected RadzenLabel label2;

        protected RadzenDropDown employeeId;

        protected RadzenRequiredValidator employeeIdRequiredValidator;

        protected RadzenLabel label3;

        protected RadzenDropDown territoryId;

        protected RadzenRequiredValidator territoryIdRequiredValidator;

        protected RadzenButton button2;

        protected RadzenButton button3;

        bool _canEdit;
        protected bool canEdit
        {
            get
            {
                return _canEdit;
            }
            set
            {
                if(_canEdit != value)
                {
                    _canEdit = value;
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

        protected override async Task OnInitAsync()
        {
            Northwind.BasePath = UriHelper.GetBaseUri();

            await Task.Run(Load);
        }

        protected async void Load()
        {
            canEdit = true;

            var northwindGetEmployeeTerritoryByEmployeeIdAndTerritoryIdResult = await Northwind.GetEmployeeTerritoryByEmployeeIdAndTerritoryId(int.Parse(EmployeeID), $"{TerritoryID}");
                employeeterritory = northwindGetEmployeeTerritoryByEmployeeIdAndTerritoryIdResult;

            var northwindGetEmployeesResult = await Northwind.GetEmployees(null, null, null, null, null, null, null, null);
                getEmployeesResult = northwindGetEmployeesResult.Data;

            var northwindGetTerritoriesResult = await Northwind.GetTerritories(null, null, null, null, null, null, null, null);
                getTerritoriesResult = northwindGetTerritoriesResult.Data;
        }

        protected async void CloseButtonClick(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("EmployeeTerritories");
        }

        protected async void Form0Submit(EmployeeTerritory args)
        {
            var northwindUpdateEmployeeTerritoryResult = await Northwind.UpdateEmployeeTerritory(int.Parse(EmployeeID), $"{TerritoryID}", employeeterritory);
                UriHelper.NavigateTo("EmployeeTerritories");
        }

        protected async void Button3Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("EmployeeTerritories");
        }
    }
}
