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
    public partial class AddCustomerDemographicComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenTemplateForm<CustomerDemographic> form0;

        protected RadzenLabel label1;

        protected RadzenTextBox customerTypeId;

        protected RadzenRequiredValidator customerTypeIdRequiredValidator;

        protected RadzenLabel label2;

        protected RadzenTextBox customerDesc;

        protected RadzenButton button1;

        protected RadzenButton button2;

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
            customerdemographic = new CustomerDemographic();
        }

        protected async void Form0Submit(CustomerDemographic args)
        {
            var northwindCreateCustomerDemographicResult = await Northwind.CreateCustomerDemographic(customerdemographic);
                DialogService.Close(customerdemographic);
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
