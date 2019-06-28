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
    public partial class CategorySalesFor1997SComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenGrid<CategorySalesFor1997> grid0;

        IEnumerable<CategorySalesFor1997> _getCategorySalesFor1997sResult;
        protected IEnumerable<CategorySalesFor1997> getCategorySalesFor1997sResult
        {
            get
            {
                return _getCategorySalesFor1997sResult;
            }
            set
            {
                if(_getCategorySalesFor1997sResult != value)
                {
                    _getCategorySalesFor1997sResult = value;
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
            var northwindGetCategorySalesFor1997SResult = await Northwind.GetCategorySalesFor1997S();
                getCategorySalesFor1997sResult = northwindGetCategorySalesFor1997SResult;
        }
    }
}
