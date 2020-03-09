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
    public partial class ProductSalesFor1997SComponent : ComponentBase
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

        protected RadzenGrid<NorthwindBlazor.Models.Northwind.ProductSalesFor1997> grid0;

        IEnumerable<NorthwindBlazor.Models.Northwind.ProductSalesFor1997> _getProductSalesFor1997sResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.ProductSalesFor1997> getProductSalesFor1997sResult
        {
            get
            {
                return _getProductSalesFor1997sResult;
            }
            set
            {
                if(!object.Equals(_getProductSalesFor1997sResult, value))
                {
                    _getProductSalesFor1997sResult = value;
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
            var northwindGetProductSalesFor1997SResult = await Northwind.GetProductSalesFor1997S();
            getProductSalesFor1997sResult = northwindGetProductSalesFor1997SResult;
        }
    }
}
