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
    public partial class CustomerDemographicsComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenButton button0;

        protected RadzenGrid<CustomerDemographic> grid0;

        protected RadzenButton gridDeleteButton;

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
            var northwindGetCustomerDemographicsResult = await Northwind.GetCustomerDemographics();
                getCustomerDemographicsResult = northwindGetCustomerDemographicsResult;
        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddCustomerDemographic>("Add Customer Demographic", null);
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(CustomerDemographic args)
        {
            var result = await DialogService.OpenAsync<EditCustomerDemographic>("Edit Customer Demographic", new Dictionary<string, object>() { {"CustomerTypeID", $"{args.CustomerTypeID}"} });
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, CustomerDemographic data)
        {
            var northwindDeleteCustomerDemographicResult = await Northwind.DeleteCustomerDemographic($"{data.CustomerTypeID}");
                if (northwindDeleteCustomerDemographicResult != null) {
                    grid0.Reload();
}
        }
    }
}
