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
    public partial class EditEmployeeModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        [Parameter]
        protected string EmployeeID { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenLabel closeLabel;

        protected RadzenButton closeButton;

        protected RadzenTemplateForm<Employee> form0;

        protected RadzenLabel label2;

        protected RadzenTextBox lastName;

        protected RadzenRequiredValidator lastNameRequiredValidator;

        protected RadzenLabel label3;

        protected RadzenTextBox firstName;

        protected RadzenRequiredValidator firstNameRequiredValidator;

        protected RadzenLabel label4;

        protected RadzenTextBox title;

        protected RadzenLabel label5;

        protected RadzenTextBox titleOfCourtesy;

        protected RadzenLabel label6;

        protected RadzenDatePicker birthDate;

        protected RadzenLabel label7;

        protected RadzenDatePicker hireDate;

        protected RadzenLabel label8;

        protected RadzenTextBox address;

        protected RadzenLabel label9;

        protected RadzenTextBox city;

        protected RadzenLabel label10;

        protected RadzenTextBox region;

        protected RadzenLabel label11;

        protected RadzenTextBox postalCode;

        protected RadzenLabel label12;

        protected RadzenTextBox country;

        protected RadzenLabel label13;

        protected RadzenTextBox homePhone;

        protected RadzenLabel label14;

        protected RadzenTextBox extension;

        protected RadzenLabel label15;

        protected RadzenTextBox photo;

        protected RadzenLabel label16;

        protected RadzenTextBox notes;

        protected RadzenLabel label17;

        protected RadzenDropDown reportsTo;

        protected RadzenLabel label18;

        protected RadzenTextBox photoPath;

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

        Employee _employee;
        protected Employee employee
        {
            get
            {
                return _employee;
            }
            set
            {
                if(_employee != value)
                {
                    _employee = value;
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

        protected override async Task OnInitAsync()
        {
            Northwind.BasePath = UriHelper.GetBaseUri();

            await Task.Run(Load);
        }

        protected async void Load()
        {
            canEdit = true;

            var northwindGetEmployeeByEmployeeIdResult = await Northwind.GetEmployeeByEmployeeId(int.Parse(EmployeeID));
                employee = northwindGetEmployeeByEmployeeIdResult;

            var northwindGetEmployeesResult = await Northwind.GetEmployees(null, null, null, null, null, null, null, null);
                getEmployeesResult = northwindGetEmployeesResult.Data;
        }

        protected async void CloseButtonClick(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("Employees");
        }

        protected async void Form0Submit(Employee args)
        {
            var northwindUpdateEmployeeResult = await Northwind.UpdateEmployee(int.Parse(EmployeeID), employee);
                UriHelper.NavigateTo("Employees");
        }

        protected async void Button3Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("Employees");
        }
    }
}
