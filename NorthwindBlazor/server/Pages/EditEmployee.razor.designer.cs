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
    public partial class EditEmployeeComponent : ComponentBase
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

        [Parameter]
        public dynamic EmployeeID { get; set; }

        NorthwindBlazor.Models.Northwind.Employee _employee;
        protected NorthwindBlazor.Models.Northwind.Employee employee
        {
            get
            {
                return _employee;
            }
            set
            {
                if(!object.Equals(_employee, value))
                {
                    _employee = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

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
            var northwindGetEmployeeByEmployeeIdResult = await Northwind.GetEmployeeByEmployeeId(int.Parse($"{EmployeeID}"));
            employee = northwindGetEmployeeByEmployeeIdResult;

            var northwindGetEmployeesResult = await Northwind.GetEmployees();
            getEmployeesResult = northwindGetEmployeesResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(NorthwindBlazor.Models.Northwind.Employee args)
        {
            try
            {
                var northwindUpdateEmployeeResult = await Northwind.UpdateEmployee(int.Parse($"{EmployeeID}"), employee);
                DialogService.Close(employee);
            }
            catch (Exception northwindUpdateEmployeeException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update Employee");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
