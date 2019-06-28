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
    public partial class CustomersComponent : ComponentBase
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
            var northwindGetCustomersResult = await Northwind.GetCustomers();
                getCustomersResult = northwindGetCustomersResult;
        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddCustomer>("Add Customer", null);
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(Customer args)
        {
            var result = await DialogService.OpenAsync<EditCustomer>("Edit Customer", new Dictionary<string, object>() { {"CustomerID", $"{args.CustomerID}"} });
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Customer data)
        {
            var northwindDeleteCustomerResult = await Northwind.DeleteCustomer($"{data.CustomerID}");
                if (northwindDeleteCustomerResult != null) {
                    grid0.Reload();
}
        }
    }
}
