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
    public partial class EditProductComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        [Parameter]
        protected string ProductID { get; set; }

        protected RadzenContent content1;

        protected RadzenLabel closeLabel;

        protected RadzenButton closeButton;

        protected RadzenTemplateForm<Product> form0;

        protected RadzenLabel label2;

        protected RadzenTextBox productName;

        protected RadzenRequiredValidator productNameRequiredValidator;

        protected RadzenLabel label3;

        protected RadzenDropDown supplierId;

        protected RadzenLabel label4;

        protected RadzenDropDown categoryId;

        protected RadzenLabel label5;

        protected RadzenTextBox quantityPerUnit;

        protected RadzenLabel label6;

        protected dynamic unitPrice;

        protected RadzenLabel label7;

        protected dynamic unitsInStock;

        protected RadzenLabel label8;

        protected dynamic unitsOnOrder;

        protected RadzenLabel label9;

        protected dynamic reorderLevel;

        protected RadzenLabel label10;

        protected RadzenCheckBox discontinued;

        protected RadzenButton button2;

        protected RadzenButton button3;

        bool _canEdit;
        protected bool canEdit
        {
            get
            {
                return _canEdit;
            }
            set
            {
                if(_canEdit != value)
                {
                    _canEdit = value;
                    Invoke(() => { StateHasChanged(); });
                }
            }
        }

        Product _product;
        protected Product product
        {
            get
            {
                return _product;
            }
            set
            {
                if(_product != value)
                {
                    _product = value;
                    Invoke(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<Supplier> _getSuppliersResult;
        protected IEnumerable<Supplier> getSuppliersResult
        {
            get
            {
                return _getSuppliersResult;
            }
            set
            {
                if(_getSuppliersResult != value)
                {
                    _getSuppliersResult = value;
                    Invoke(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<Category> _getCategoriesResult;
        protected IEnumerable<Category> getCategoriesResult
        {
            get
            {
                return _getCategoriesResult;
            }
            set
            {
                if(_getCategoriesResult != value)
                {
                    _getCategoriesResult = value;
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
            canEdit = true;

            var northwindGetProductByProductIdResult = await Northwind.GetProductByProductId(int.Parse(ProductID));
                product = northwindGetProductByProductIdResult;

            var northwindGetSuppliersResult = await Northwind.GetSuppliers();
                getSuppliersResult = northwindGetSuppliersResult;

            var northwindGetCategoriesResult = await Northwind.GetCategories();
                getCategoriesResult = northwindGetCategoriesResult;
        }

        protected async void CloseButtonClick(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async void Form0Submit(Product args)
        {
            var northwindUpdateProductResult = await Northwind.UpdateProduct(int.Parse(ProductID), product);
                DialogService.Close(product);
        }

        protected async void Button3Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
