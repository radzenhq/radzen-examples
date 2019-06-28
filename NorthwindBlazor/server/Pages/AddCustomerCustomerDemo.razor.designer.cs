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
    public partial class AddCustomerCustomerDemoComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenTemplateForm<CustomerCustomerDemo> form0;

        protected RadzenLabel label1;

        protected RadzenDropDown customerId;

        protected RadzenRequiredValidator customerIdRequiredValidator;

        protected RadzenLabel label2;

        protected RadzenDropDown customerTypeId;

        protected RadzenRequiredValidator customerTypeIdRequiredValidator;

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
                    Invoke(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<CustomerDemographic> _getCustomerDemographicsResult;
        protected IEnumerable<CustomerDemographic> getCustomerDemographicsResult
        {
            get
            {
                return _getCustomerDemographicsResult;
            }
            set
            {
                if(_getCustomerDemographicsResult != value)
                {
                    _getCustomerDemographicsResult = value;
                    Invoke(() => { StateHasChanged(); });
                }
            }
        }

        CustomerCustomerDemo _customercustomerdemo;
        protected CustomerCustomerDemo customercustomerdemo
        {
            get
            {
                return _customercustomerdemo;
            }
            set
            {
                if(_customercustomerdemo != value)
                {
                    _customercustomerdemo = value;
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
            var northwindGetCustomersResult = await Northwind.GetCustomers();
                getCustomersResult = northwindGetCustomersResult;

            var northwindGetCustomerDemographicsResult = await Northwind.GetCustomerDemographics();
                getCustomerDemographicsResult = northwindGetCustomerDemographicsResult;

            customercustomerdemo = new CustomerCustomerDemo();
        }

        protected async void Form0Submit(CustomerCustomerDemo args)
        {
            var northwindCreateCustomerCustomerDemoResult = await Northwind.CreateCustomerCustomerDemo(customercustomerdemo);
                DialogService.Close(customercustomerdemo);
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
