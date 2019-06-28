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
    public partial class OrderSubtotalsComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenGrid<OrderSubtotal> grid0;

        IEnumerable<OrderSubtotal> _getOrderSubtotalsResult;
        protected IEnumerable<OrderSubtotal> getOrderSubtotalsResult
        {
            get
            {
                return _getOrderSubtotalsResult;
            }
            set
            {
                if(_getOrderSubtotalsResult != value)
                {
                    _getOrderSubtotalsResult = value;
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
            var northwindGetOrderSubtotalsResult = await Northwind.GetOrderSubtotals();
                getOrderSubtotalsResult = northwindGetOrderSubtotalsResult;
        }
    }
}
