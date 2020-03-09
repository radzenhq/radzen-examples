using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using NorthwindBlazor.Models.Northwind;
using Microsoft.EntityFrameworkCore;

namespace NorthwindBlazor.Pages
{
    public partial class EditCustomerDemographicComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected NorthwindService Northwind { get; set; }

        [Parameter]
        public dynamic CustomerTypeID { get; set; }

        NorthwindBlazor.Models.Northwind.CustomerDemographic _customerdemographic;
        protected NorthwindBlazor.Models.Northwind.CustomerDemographic customerdemographic
        {
            get
            {
                return _customerdemographic;
            }
            set
            {
                if(!object.Equals(_customerdemographic, value))
                {
                    _customerdemographic = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var northwindGetCustomerDemographicByCustomerTypeIdResult = await Northwind.GetCustomerDemographicByCustomerTypeId($"{CustomerTypeID}");
            customerdemographic = northwindGetCustomerDemographicByCustomerTypeIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(NorthwindBlazor.Models.Northwind.CustomerDemographic args)
        {
            try
            {
                var northwindUpdateCustomerDemographicResult = await Northwind.UpdateCustomerDemographic($"{CustomerTypeID}", customerdemographic);
                DialogService.Close(customerdemographic);
            }
            catch (Exception northwindUpdateCustomerDemographicException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update CustomerDemographic");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
