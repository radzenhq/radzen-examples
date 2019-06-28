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
    public partial class EditCustomerDemographicComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        [Parameter]
        protected string CustomerTypeID { get; set; }

        protected RadzenContent content1;

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
                    Invoke(() => { StateHasChanged(); });
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

            var northwindGetCustomerDemographicByCustomerTypeIdResult = await Northwind.GetCustomerDemographicByCustomerTypeId($"{CustomerTypeID}");
                customerdemographic = northwindGetCustomerDemographicByCustomerTypeIdResult;
        }

        protected async void CloseButtonClick(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async void Form0Submit(CustomerDemographic args)
        {
            var northwindUpdateCustomerDemographicResult = await Northwind.UpdateCustomerDemographic($"{CustomerTypeID}", customerdemographic);
                DialogService.Close(customerdemographic);
        }

        protected async void Button3Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
