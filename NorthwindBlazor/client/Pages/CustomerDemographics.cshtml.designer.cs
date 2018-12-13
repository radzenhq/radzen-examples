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
    public partial class CustomerDemographicsModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

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
                    StateHasChanged();
                }
            }
        }

        int _getCustomerDemographicsCount;
        protected int getCustomerDemographicsCount
        {
            get
            {
                return _getCustomerDemographicsCount;
            }
            set
            {
                if(_getCustomerDemographicsCount != value)
                {
                    _getCustomerDemographicsCount = value;
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

        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("AddCustomerDemographic");
        }

        protected async void Grid0LoadData(LoadDataArgs args)
        {
            var northwindGetCustomerDemographicsResult = await Northwind.GetCustomerDemographics(null, args.Top, args.Skip, $"{args.OrderBy}", args.Top != null && args.Skip != null, $"", null, null);
                getCustomerDemographicsResult = northwindGetCustomerDemographicsResult.Data;

                getCustomerDemographicsCount = northwindGetCustomerDemographicsResult.Count;
        }

        protected async void Grid0RowSelect(CustomerDemographic args)
        {
            UriHelper.NavigateTo($"EditCustomerDemographic/{args.CustomerTypeID}");
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, CustomerDemographic data)
        {
            var northwindDeleteCustomerDemographicResult = await Northwind.DeleteCustomerDemographic($"{data.CustomerTypeID}");
                if (northwindDeleteCustomerDemographicResult.IsSuccessStatusCode) {
                    grid0.Reload();
}
        }
    }
}
