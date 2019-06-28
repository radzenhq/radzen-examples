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
    public partial class ProductsByCategoriesComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenGrid<ProductsByCategory> grid0;

        IEnumerable<ProductsByCategory> _getProductsByCategoriesResult;
        protected IEnumerable<ProductsByCategory> getProductsByCategoriesResult
        {
            get
            {
                return _getProductsByCategoriesResult;
            }
            set
            {
                if(_getProductsByCategoriesResult != value)
                {
                    _getProductsByCategoriesResult = value;
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
            var northwindGetProductsByCategoriesResult = await Northwind.GetProductsByCategories();
                getProductsByCategoriesResult = northwindGetProductsByCategoriesResult;
        }
    }
}
