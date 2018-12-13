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
    public partial class AddCustomerCustomerDemoModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

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
                    StateHasChanged();
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
                    StateHasChanged();
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

            var northwindGetCustomerDemographicsResult = await Northwind.GetCustomerDemographics(null, null, null, null, null, null, null, null);
                getCustomerDemographicsResult = northwindGetCustomerDemographicsResult.Data;

            customercustomerdemo = new CustomerCustomerDemo();
        }

        protected async void Form0Submit(CustomerCustomerDemo args)
        {
            var northwindCreateCustomerCustomerDemoResult = await Northwind.CreateCustomerCustomerDemo(customercustomerdemo);
                UriHelper.NavigateTo("CustomerCustomerDemos");
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("CustomerCustomerDemos");
        }
    }
}
