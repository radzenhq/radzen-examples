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
    public partial class AddCustomerComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

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
            customer = new Customer();
        }

        protected async void Form0Submit(Customer args)
        {
            var northwindCreateCustomerResult = await Northwind.CreateCustomer(customer);
                DialogService.Close(customer);
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
