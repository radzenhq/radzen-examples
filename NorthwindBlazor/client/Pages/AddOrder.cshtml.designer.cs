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
    public partial class AddOrderModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenTemplateForm<Order> form0;

        protected RadzenLabel label1;

        protected RadzenDropDown customerId;

        protected RadzenLabel label2;

        protected RadzenDropDown employeeId;

        protected RadzenLabel label3;

        protected RadzenDatePicker orderDate;

        protected RadzenLabel label4;

        protected RadzenDatePicker requiredDate;

        protected RadzenLabel label5;

        protected RadzenDatePicker shippedDate;

        protected RadzenLabel label6;

        protected RadzenDropDown shipVia;

        protected RadzenLabel label7;

        protected dynamic freight;

        protected RadzenLabel label8;

        protected RadzenTextBox shipName;

        protected RadzenLabel label9;

        protected RadzenTextBox shipAddress;

        protected RadzenLabel label10;

        protected RadzenTextBox shipCity;

        protected RadzenLabel label11;

        protected RadzenTextBox shipRegion;

        protected RadzenLabel label12;

        protected RadzenTextBox shipPostalCode;

        protected RadzenLabel label13;

        protected RadzenTextBox shipCountry;

        protected RadzenButton button1;

        protected RadzenButton button2;

        IEnumerable<Customer> _getCustomersResult;
        protected IEnumerable<Customer> getCustomersResult
        {
            get
            {
                return _getCustomersResult;
            }
            set
            {
                if(_getCustomersResult != value)
                {
                    _getCustomersResult = value;
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

        IEnumerable<Shipper> _getShippersResult;
        protected IEnumerable<Shipper> getShippersResult
        {
            get
            {
                return _getShippersResult;
            }
            set
            {
                if(_getShippersResult != value)
                {
                    _getShippersResult = value;
                    StateHasChanged();
                }
            }
        }

        Order _order;
        protected Order order
        {
            get
            {
                return _order;
            }
            set
            {
                if(_order != value)
                {
                    _order = value;
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
            var northwindGetCustomersResult = await Northwind.GetCustomers(null, null, null, null, null, null, null, null);
                getCustomersResult = northwindGetCustomersResult.Data;

            var northwindGetEmployeesResult = await Northwind.GetEmployees(null, null, null, null, null, null, null, null);
                getEmployeesResult = northwindGetEmployeesResult.Data;

            var northwindGetShippersResult = await Northwind.GetShippers(null, null, null, null, null, null, null, null);
                getShippersResult = northwindGetShippersResult.Data;

            order = new Order();
        }

        protected async void Form0Submit(Order args)
        {
            var northwindCreateOrderResult = await Northwind.CreateOrder(order);
                UriHelper.NavigateTo("Orders");
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("Orders");
        }
    }
}
