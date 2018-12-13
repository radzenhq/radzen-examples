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
    public partial class ProductsModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

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
                    StateHasChanged();
                }
            }
        }

        int _getProductsCount;
        protected int getProductsCount
        {
            get
            {
                return _getProductsCount;
            }
            set
            {
                if(_getProductsCount != value)
                {
                    _getProductsCount = value;
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
            UriHelper.NavigateTo("AddProduct");
        }

        protected async void Grid0LoadData(LoadDataArgs args)
        {
            var northwindGetProductsResult = await Northwind.GetProducts(null, args.Top, args.Skip, $"{args.OrderBy}", args.Top != null && args.Skip != null, $"Supplier,Category", null, null);
                getProductsResult = northwindGetProductsResult.Data;

                getProductsCount = northwindGetProductsResult.Count;
        }

        protected async void Grid0RowSelect(Product args)
        {
            UriHelper.NavigateTo($"EditProduct/{args.ProductID}");
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Product data)
        {
            var northwindDeleteProductResult = await Northwind.DeleteProduct(data.ProductID);
                if (northwindDeleteProductResult.IsSuccessStatusCode) {
                    grid0.Reload();
}
        }
    }
}
