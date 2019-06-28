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
    public partial class EditEmployeeTerritoryComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        [Parameter]
        protected string EmployeeID { get; set; }

        [Parameter]
        protected string TerritoryID { get; set; }

        protected RadzenContent content1;

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

        protected override async Task OnInitAsync()
        {
            await Task.Run(Load);
        }

        protected async void Load()
        {
            canEdit = true;

            var northwindGetEmployeeTerritoryByEmployeeIdAndTerritoryIdResult = await Northwind.GetEmployeeTerritoryByEmployeeIdAndTerritoryId(int.Parse(EmployeeID), $"{TerritoryID}");
                employeeterritory = northwindGetEmployeeTerritoryByEmployeeIdAndTerritoryIdResult;

            var northwindGetEmployeesResult = await Northwind.GetEmployees();
                getEmployeesResult = northwindGetEmployeesResult;

            var northwindGetTerritoriesResult = await Northwind.GetTerritories();
                getTerritoriesResult = northwindGetTerritoriesResult;
        }

        protected async void CloseButtonClick(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async void Form0Submit(EmployeeTerritory args)
        {
            var northwindUpdateEmployeeTerritoryResult = await Northwind.UpdateEmployeeTerritory(int.Parse(EmployeeID), $"{TerritoryID}", employeeterritory);
                DialogService.Close(employeeterritory);
        }

        protected async void Button3Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
