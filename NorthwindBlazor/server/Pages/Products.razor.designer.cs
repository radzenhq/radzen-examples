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
    public partial class ProductsComponent : ComponentBase
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

        protected RadzenGrid<Product> grid0;

        protected RadzenButton gridDeleteButton;

        IEnumerable<Product> _getProductsResult;
        protected IEnumerable<Product> getProductsResult
        {
            get
            {
                return _getProductsResult;
            }
            set
            {
                if(_getProductsResult != value)
                {
                    _getProductsResult = value;
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
            var northwindGetProductsResult = await Northwind.GetProducts();
                getProductsResult = northwindGetProductsResult;
        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddProduct>("Add Product", null);
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(Product args)
        {
            var result = await DialogService.OpenAsync<EditProduct>("Edit Product", new Dictionary<string, object>() { {"ProductID", $"{args.ProductID}"} });
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Product data)
        {
            var northwindDeleteProductResult = await Northwind.DeleteProduct(data.ProductID);
                if (northwindDeleteProductResult != null) {
                    grid0.Reload();
}
        }
    }
}
