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
    public partial class CustomerCustomerDemosModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenButton button0;

        protected RadzenGrid<CustomerCustomerDemo> grid0;

        protected RadzenButton gridDeleteButton;

        IEnumerable<CustomerCustomerDemo> _getCustomerCustomerDemosResult;
        protected IEnumerable<CustomerCustomerDemo> getCustomerCustomerDemosResult
        {
            get
            {
                return _getCustomerCustomerDemosResult;
            }
            set
            {
                if(_getCustomerCustomerDemosResult != value)
                {
                    _getCustomerCustomerDemosResult = value;
                    StateHasChanged();
                }
            }
        }

        int _getCustomerCustomerDemosCount;
        protected int getCustomerCustomerDemosCount
        {
            get
            {
                return _getCustomerCustomerDemosCount;
            }
            set
            {
                if(_getCustomerCustomerDemosCount != value)
                {
                    _getCustomerCustomerDemosCount = value;
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
            UriHelper.NavigateTo("AddCustomerCustomerDemo");
        }

        protected async void Grid0LoadData(LoadDataArgs args)
        {
            var northwindGetCustomerCustomerDemosResult = await Northwind.GetCustomerCustomerDemos(null, args.Top, args.Skip, $"{args.OrderBy}", args.Top != null && args.Skip != null, $"Customer,CustomerDemographic", null, null);
                getCustomerCustomerDemosResult = northwindGetCustomerCustomerDemosResult.Data;

                getCustomerCustomerDemosCount = northwindGetCustomerCustomerDemosResult.Count;
        }

        protected async void Grid0RowSelect(CustomerCustomerDemo args)
        {
            UriHelper.NavigateTo($"EditCustomerCustomerDemo/{args.CustomerID}/{args.CustomerTypeID}");
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, CustomerCustomerDemo data)
        {
            var northwindDeleteCustomerCustomerDemoResult = await Northwind.DeleteCustomerCustomerDemo($"{data.CustomerID}", $"{data.CustomerTypeID}");
                if (northwindDeleteCustomerCustomerDemoResult.IsSuccessStatusCode) {
                    grid0.Reload();
}
        }
    }
}
