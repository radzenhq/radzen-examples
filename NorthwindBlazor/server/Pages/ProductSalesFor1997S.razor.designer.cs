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
    public partial class ProductSalesFor1997SComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenGrid<ProductSalesFor1997> grid0;

        IEnumerable<ProductSalesFor1997> _getProductSalesFor1997sResult;
        protected IEnumerable<ProductSalesFor1997> getProductSalesFor1997sResult
        {
            get
            {
                return _getProductSalesFor1997sResult;
            }
            set
            {
                if(_getProductSalesFor1997sResult != value)
                {
                    _getProductSalesFor1997sResult = value;
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
            var northwindGetProductSalesFor1997SResult = await Northwind.GetProductSalesFor1997S();
                getProductSalesFor1997sResult = northwindGetProductSalesFor1997SResult;
        }
    }
}
