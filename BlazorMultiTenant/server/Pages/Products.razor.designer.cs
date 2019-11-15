using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using BlazorMultiTenant.Models.Sample;
using Microsoft.AspNetCore.Identity;
using BlazorMultiTenant.Models;

namespace BlazorMultiTenant.Pages
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
        protected SecurityService Security { get; set; }

        [Inject]
        protected SampleService Sample { get; set; }


        protected RadzenGrid<BlazorMultiTenant.Models.Sample.Product> grid0;

        IEnumerable<BlazorMultiTenant.Models.Sample.Product> _getProductsResult;
        protected IEnumerable<BlazorMultiTenant.Models.Sample.Product> getProductsResult
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
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            if (!Security.IsAuthenticated())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                Load();
            }

        }

        protected async void Load()
        {
            var sampleGetProductsResult = await Sample.GetProducts();
            getProductsResult = sampleGetProductsResult;
        }

        protected async void Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddProduct>("Add Product", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(BlazorMultiTenant.Models.Sample.Product args)
        {
            var result = await DialogService.OpenAsync<EditProduct>("Edit Product", new Dictionary<string, object>() { {"Id", args.Id} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var sampleDeleteProductResult = await Sample.DeleteProduct(data.Id);
                if (sampleDeleteProductResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception sampleDeleteProductException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Product");
            }
        }
    }
}
