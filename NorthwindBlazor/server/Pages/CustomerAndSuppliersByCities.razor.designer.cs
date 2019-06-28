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
    public partial class CustomerAndSuppliersByCitiesComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenGrid<CustomerAndSuppliersByCity> grid0;

        IEnumerable<CustomerAndSuppliersByCity> _getCustomerAndSuppliersByCitiesResult;
        protected IEnumerable<CustomerAndSuppliersByCity> getCustomerAndSuppliersByCitiesResult
        {
            get
            {
                return _getCustomerAndSuppliersByCitiesResult;
            }
            set
            {
                if(_getCustomerAndSuppliersByCitiesResult != value)
                {
                    _getCustomerAndSuppliersByCitiesResult = value;
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
            var northwindGetCustomerAndSuppliersByCitiesResult = await Northwind.GetCustomerAndSuppliersByCities();
                getCustomerAndSuppliersByCitiesResult = northwindGetCustomerAndSuppliersByCitiesResult;
        }
    }
}
