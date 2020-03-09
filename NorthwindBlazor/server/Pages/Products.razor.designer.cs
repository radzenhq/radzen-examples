using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using NorthwindBlazor.Models.Northwind;
using Microsoft.EntityFrameworkCore;

namespace NorthwindBlazor.Pages
{
    public partial class ProductsComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected NorthwindService Northwind { get; set; }

        protected RadzenGrid<NorthwindBlazor.Models.Northwind.Product> grid0;

        IEnumerable<NorthwindBlazor.Models.Northwind.Product> _getProductsResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.Product> getProductsResult
        {
            get
            {
                return _getProductsResult;
            }
            set
            {
                if(!object.Equals(_getProductsResult, value))
                {
                    _getProductsResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var northwindGetProductsResult = await Northwind.GetProducts();
            getProductsResult = northwindGetProductsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddProduct>("Add Product", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(NorthwindBlazor.Models.Northwind.Product args)
        {
            var result = await DialogService.OpenAsync<EditProduct>("Edit Product", new Dictionary<string, object>() { {"ProductID", args.ProductID} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var northwindDeleteProductResult = await Northwind.DeleteProduct(data.ProductID);
                if (northwindDeleteProductResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception northwindDeleteProductException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Product");
            }
        }
    }
}
