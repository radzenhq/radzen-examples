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
    public partial class CustomerCustomerDemosComponent : ComponentBase
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
            var northwindGetCustomerCustomerDemosResult = await Northwind.GetCustomerCustomerDemos();
                getCustomerCustomerDemosResult = northwindGetCustomerCustomerDemosResult;
        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddCustomerCustomerDemo>("Add Customer Customer Demo", null);
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(CustomerCustomerDemo args)
        {
            var result = await DialogService.OpenAsync<EditCustomerCustomerDemo>("Edit Customer Customer Demo", new Dictionary<string, object>() { {"CustomerID", $"{args.CustomerID}"}, {"CustomerTypeID", $"{args.CustomerTypeID}"} });
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, CustomerCustomerDemo data)
        {
            var northwindDeleteCustomerCustomerDemoResult = await Northwind.DeleteCustomerCustomerDemo($"{data.CustomerID}", $"{data.CustomerTypeID}");
                if (northwindDeleteCustomerCustomerDemoResult != null) {
                    grid0.Reload();
}
        }
    }
}
