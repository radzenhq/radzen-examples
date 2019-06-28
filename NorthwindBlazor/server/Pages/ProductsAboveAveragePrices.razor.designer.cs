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
    public partial class ProductsAboveAveragePricesComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenGrid<ProductsAboveAveragePrice> grid0;

        IEnumerable<ProductsAboveAveragePrice> _getProductsAboveAveragePricesResult;
        protected IEnumerable<ProductsAboveAveragePrice> getProductsAboveAveragePricesResult
        {
            get
            {
                return _getProductsAboveAveragePricesResult;
            }
            set
            {
                if(_getProductsAboveAveragePricesResult != value)
                {
                    _getProductsAboveAveragePricesResult = value;
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
            var northwindGetProductsAboveAveragePricesResult = await Northwind.GetProductsAboveAveragePrices();
                getProductsAboveAveragePricesResult = northwindGetProductsAboveAveragePricesResult;
        }
    }
}
