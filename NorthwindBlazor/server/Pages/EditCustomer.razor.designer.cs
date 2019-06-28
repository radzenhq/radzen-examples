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
    public partial class EditCustomerComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        [Parameter]
        protected string CustomerID { get; set; }

        protected RadzenContent content1;

        protected RadzenLabel closeLabel;

        protected RadzenButton closeButton;

        protected RadzenTemplateForm<Customer> form0;

        protected RadzenLabel label2;

        protected RadzenTextBox customerId;

        protected RadzenRequiredValidator customerIdRequiredValidator;

        protected RadzenLabel label3;

        protected RadzenTextBox companyName;

        protected RadzenRequiredValidator companyNameRequiredValidator;

        protected RadzenLabel label4;

        protected RadzenTextBox contactName;

        protected RadzenLabel label5;

        protected RadzenTextBox contactTitle;

        protected RadzenLabel label6;

        protected RadzenTextBox address;

        protected RadzenLabel label7;

        protected RadzenTextBox city;

        protected RadzenLabel label8;

        protected RadzenTextBox region;

        protected RadzenLabel label9;

        protected RadzenTextBox postalCode;

        protected RadzenLabel label10;

        protected RadzenTextBox country;

        protected RadzenLabel label11;

        protected RadzenTextBox phone;

        protected RadzenLabel label12;

        protected RadzenTextBox fax;

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
            canEdit = true;

            var northwindGetCustomerByCustomerIdResult = await Northwind.GetCustomerByCustomerId($"{CustomerID}");
                customer = northwindGetCustomerByCustomerIdResult;
        }

        protected async void CloseButtonClick(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async void Form0Submit(Customer args)
        {
            var northwindUpdateCustomerResult = await Northwind.UpdateCustomer($"{CustomerID}", customer);
                DialogService.Close(customer);
        }

        protected async void Button3Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
