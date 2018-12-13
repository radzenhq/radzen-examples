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
    public partial class AddCustomerModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenTemplateForm<Customer> form0;

        protected RadzenLabel label1;

        protected RadzenTextBox customerId;

        protected RadzenRequiredValidator customerIdRequiredValidator;

        protected RadzenLabel label2;

        protected RadzenTextBox companyName;

        protected RadzenRequiredValidator companyNameRequiredValidator;

        protected RadzenLabel label3;

        protected RadzenTextBox contactName;

        protected RadzenLabel label4;

        protected RadzenTextBox contactTitle;

        protected RadzenLabel label5;

        protected RadzenTextBox address;

        protected RadzenLabel label6;

        protected RadzenTextBox city;

        protected RadzenLabel label7;

        protected RadzenTextBox region;

        protected RadzenLabel label8;

        protected RadzenTextBox postalCode;

        protected RadzenLabel label9;

        protected RadzenTextBox country;

        protected RadzenLabel label10;

        protected RadzenTextBox phone;

        protected RadzenLabel label11;

        protected RadzenTextBox fax;

        protected RadzenButton button1;

        protected RadzenButton button2;

        Customer _customer;
        protected Customer customer
        {
            get
            {
                return _customer;
            }
            set
            {
                if(_customer != value)
                {
                    _customer = value;
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
            customer = new Customer();
        }

        protected async void Form0Submit(Customer args)
        {
            var northwindCreateCustomerResult = await Northwind.CreateCustomer(customer);
                UriHelper.NavigateTo("Customers");
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("Customers");
        }
    }
}
