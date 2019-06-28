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
    public partial class EditCustomerCustomerDemoComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        [Parameter]
        protected string CustomerID { get; set; }

        [Parameter]
        protected string CustomerTypeID { get; set; }

        protected RadzenContent content1;

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

        protected override async Task OnInitAsync()
        {
            await Task.Run(Load);
        }

        protected async void Load()
        {
            canEdit = true;

            var northwindGetCustomerCustomerDemoByCustomerIdAndCustomerTypeIdResult = await Northwind.GetCustomerCustomerDemoByCustomerIdAndCustomerTypeId($"{CustomerID}", $"{CustomerTypeID}");
                customercustomerdemo = northwindGetCustomerCustomerDemoByCustomerIdAndCustomerTypeIdResult;

            var northwindGetCustomersResult = await Northwind.GetCustomers();
                getCustomersResult = northwindGetCustomersResult;

            var northwindGetCustomerDemographicsResult = await Northwind.GetCustomerDemographics();
                getCustomerDemographicsResult = northwindGetCustomerDemographicsResult;
        }

        protected async void CloseButtonClick(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async void Form0Submit(CustomerCustomerDemo args)
        {
            var northwindUpdateCustomerCustomerDemoResult = await Northwind.UpdateCustomerCustomerDemo($"{CustomerID}", $"{CustomerTypeID}", customercustomerdemo);
                DialogService.Close(customercustomerdemo);
        }

        protected async void Button3Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
