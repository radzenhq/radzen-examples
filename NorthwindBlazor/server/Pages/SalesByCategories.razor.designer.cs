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
    public partial class SalesByCategoriesComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenGrid<SalesByCategory> grid0;

        IEnumerable<SalesByCategory> _getSalesByCategoriesResult;
        protected IEnumerable<SalesByCategory> getSalesByCategoriesResult
        {
            get
            {
                return _getSalesByCategoriesResult;
            }
            set
            {
                if(_getSalesByCategoriesResult != value)
                {
                    _getSalesByCategoriesResult = value;
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
            var northwindGetSalesByCategoriesResult = await Northwind.GetSalesByCategories();
                getSalesByCategoriesResult = northwindGetSalesByCategoriesResult;
        }
    }
}
