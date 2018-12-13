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
    public partial class EditCustomerCustomerDemoModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        [Parameter]
        protected string CustomerID { get; set; }

        [Parameter]
        protected string CustomerTypeID { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenLabel closeLabel;

        protected RadzenButton closeButton;

        protected RadzenTemplateForm<CustomerCustomerDemo> form0;

        protected RadzenLabel label2;

        protected RadzenDropDown customerId;

        protected RadzenRequiredValidator customerIdRequiredValidator;

        protected RadzenLabel label3;

        protected RadzenDropDown customerTypeId;

        protected RadzenRequiredValidator customerTypeIdRequiredValidator;

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

        protected override async Task OnInitAsync()
        {
            Northwind.BasePath = UriHelper.GetBaseUri();

            await Task.Run(Load);
        }

        protected async void Load()
        {
            canEdit = true;

            var northwindGetCustomerCustomerDemoByCustomerIdAndCustomerTypeIdResult = await Northwind.GetCustomerCustomerDemoByCustomerIdAndCustomerTypeId($"{CustomerID}", $"{CustomerTypeID}");
                customercustomerdemo = northwindGetCustomerCustomerDemoByCustomerIdAndCustomerTypeIdResult;

            var northwindGetCustomersResult = await Northwind.GetCustomers(null, null, null, null, null, null, null, null);
                getCustomersResult = northwindGetCustomersResult.Data;

            var northwindGetCustomerDemographicsResult = await Northwind.GetCustomerDemographics(null, null, null, null, null, null, null, null);
                getCustomerDemographicsResult = northwindGetCustomerDemographicsResult.Data;
        }

        protected async void CloseButtonClick(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("CustomerCustomerDemos");
        }

        protected async void Form0Submit(CustomerCustomerDemo args)
        {
            var northwindUpdateCustomerCustomerDemoResult = await Northwind.UpdateCustomerCustomerDemo($"{CustomerID}", $"{CustomerTypeID}", customercustomerdemo);
                UriHelper.NavigateTo("CustomerCustomerDemos");
        }

        protected async void Button3Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("CustomerCustomerDemos");
        }
    }
}
