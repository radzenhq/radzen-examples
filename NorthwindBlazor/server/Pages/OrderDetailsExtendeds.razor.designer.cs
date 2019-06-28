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
    public partial class OrderDetailsExtendedsComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenGrid<OrderDetailsExtended> grid0;

        IEnumerable<OrderDetailsExtended> _getOrderDetailsExtendedsResult;
        protected IEnumerable<OrderDetailsExtended> getOrderDetailsExtendedsResult
        {
            get
            {
                return _getOrderDetailsExtendedsResult;
            }
            set
            {
                if(_getOrderDetailsExtendedsResult != value)
                {
                    _getOrderDetailsExtendedsResult = value;
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
            var northwindGetOrderDetailsExtendedsResult = await Northwind.GetOrderDetailsExtendeds();
                getOrderDetailsExtendedsResult = northwindGetOrderDetailsExtendedsResult;
        }
    }
}
