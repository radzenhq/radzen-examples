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
    public partial class EditCustomerDemographicModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        [Parameter]
        protected string CustomerTypeID { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenLabel closeLabel;

        protected RadzenButton closeButton;

        protected RadzenTemplateForm<CustomerDemographic> form0;

        protected RadzenLabel label2;

        protected RadzenTextBox customerTypeId;

        protected RadzenRequiredValidator customerTypeIdRequiredValidator;

        protected RadzenLabel label3;

        protected RadzenTextBox customerDesc;

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

        CustomerDemographic _customerdemographic;
        protected CustomerDemographic customerdemographic
        {
            get
            {
                return _customerdemographic;
            }
            set
            {
                if(_customerdemographic != value)
                {
                    _customerdemographic = value;
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

            var northwindGetCustomerDemographicByCustomerTypeIdResult = await Northwind.GetCustomerDemographicByCustomerTypeId($"{CustomerTypeID}");
                customerdemographic = northwindGetCustomerDemographicByCustomerTypeIdResult;
        }

        protected async void CloseButtonClick(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("CustomerDemographics");
        }

        protected async void Form0Submit(CustomerDemographic args)
        {
            var northwindUpdateCustomerDemographicResult = await Northwind.UpdateCustomerDemographic($"{CustomerTypeID}", customerdemographic);
                UriHelper.NavigateTo("CustomerDemographics");
        }

        protected async void Button3Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("CustomerDemographics");
        }
    }
}
