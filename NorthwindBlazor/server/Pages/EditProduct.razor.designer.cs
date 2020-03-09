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
    public partial class EditProductComponent : ComponentBase
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

        [Parameter]
        public dynamic ProductID { get; set; }

        NorthwindBlazor.Models.Northwind.Product _product;
        protected NorthwindBlazor.Models.Northwind.Product product
        {
            get
            {
                return _product;
            }
            set
            {
                if(!object.Equals(_product, value))
                {
                    _product = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<NorthwindBlazor.Models.Northwind.Supplier> _getSuppliersResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.Supplier> getSuppliersResult
        {
            get
            {
                return _getSuppliersResult;
            }
            set
            {
                if(!object.Equals(_getSuppliersResult, value))
                {
                    _getSuppliersResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<NorthwindBlazor.Models.Northwind.Category> _getCategoriesResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.Category> getCategoriesResult
        {
            get
            {
                return _getCategoriesResult;
            }
            set
            {
                if(!object.Equals(_getCategoriesResult, value))
                {
                    _getCategoriesResult = value;
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
            var northwindGetProductByProductIdResult = await Northwind.GetProductByProductId(int.Parse($"{ProductID}"));
            product = northwindGetProductByProductIdResult;

            var northwindGetSuppliersResult = await Northwind.GetSuppliers();
            getSuppliersResult = northwindGetSuppliersResult;

            var northwindGetCategoriesResult = await Northwind.GetCategories();
            getCategoriesResult = northwindGetCategoriesResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(NorthwindBlazor.Models.Northwind.Product args)
        {
            try
            {
                var northwindUpdateProductResult = await Northwind.UpdateProduct(int.Parse($"{ProductID}"), product);
                DialogService.Close(product);
            }
            catch (Exception northwindUpdateProductException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update Product");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
