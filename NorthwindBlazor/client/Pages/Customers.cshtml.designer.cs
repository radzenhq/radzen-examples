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
    public partial class CustomersModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenButton button0;

        protected RadzenGrid<Customer> grid0;

        protected RadzenButton gridDeleteButton;

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

        int _getCustomersCount;
        protected int getCustomersCount
        {
            get
            {
                return _getCustomersCount;
            }
            set
            {
                if(_getCustomersCount != value)
                {
                    _getCustomersCount = value;
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
            UriHelper.NavigateTo("AddCustomer");
        }

        protected async void Grid0LoadData(LoadDataArgs args)
        {
            var northwindGetCustomersResult = await Northwind.GetCustomers(null, args.Top, args.Skip, $"{args.OrderBy}", args.Top != null && args.Skip != null, $"", null, null);
                getCustomersResult = northwindGetCustomersResult.Data;

                getCustomersCount = northwindGetCustomersResult.Count;
        }

        protected async void Grid0RowSelect(Customer args)
        {
            UriHelper.NavigateTo($"EditCustomer/{args.CustomerID}");
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Customer data)
        {
            var northwindDeleteCustomerResult = await Northwind.DeleteCustomer($"{data.CustomerID}");
                if (northwindDeleteCustomerResult.IsSuccessStatusCode) {
                    grid0.Reload();
}
        }
    }
}
