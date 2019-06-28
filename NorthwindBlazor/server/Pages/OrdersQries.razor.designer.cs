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
    public partial class OrdersQriesComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenGrid<OrdersQry> grid0;

        IEnumerable<OrdersQry> _getOrdersQriesResult;
        protected IEnumerable<OrdersQry> getOrdersQriesResult
        {
            get
            {
                return _getOrdersQriesResult;
            }
            set
            {
                if(_getOrdersQriesResult != value)
                {
                    _getOrdersQriesResult = value;
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
            var northwindGetOrdersQriesResult = await Northwind.GetOrdersQries();
                getOrdersQriesResult = northwindGetOrdersQriesResult;
        }
    }
}
