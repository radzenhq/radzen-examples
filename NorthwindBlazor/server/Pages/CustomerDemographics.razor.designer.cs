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
    public partial class CustomerDemographicsComponent : ComponentBase
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

        protected RadzenGrid<NorthwindBlazor.Models.Northwind.CustomerDemographic> grid0;

        IEnumerable<NorthwindBlazor.Models.Northwind.CustomerDemographic> _getCustomerDemographicsResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.CustomerDemographic> getCustomerDemographicsResult
        {
            get
            {
                return _getCustomerDemographicsResult;
            }
            set
            {
                if(!object.Equals(_getCustomerDemographicsResult, value))
                {
                    _getCustomerDemographicsResult = value;
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
            var northwindGetCustomerDemographicsResult = await Northwind.GetCustomerDemographics();
            getCustomerDemographicsResult = northwindGetCustomerDemographicsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddCustomerDemographic>("Add Customer Demographic", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(NorthwindBlazor.Models.Northwind.CustomerDemographic args)
        {
            var result = await DialogService.OpenAsync<EditCustomerDemographic>("Edit Customer Demographic", new Dictionary<string, object>() { {"CustomerTypeID", args.CustomerTypeID} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var northwindDeleteCustomerDemographicResult = await Northwind.DeleteCustomerDemographic($"{data.CustomerTypeID}");
                if (northwindDeleteCustomerDemographicResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception northwindDeleteCustomerDemographicException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete CustomerDemographic");
            }
        }
    }
}
