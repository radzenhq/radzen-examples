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
    public partial class OrdersComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenButton button0;

        protected RadzenGrid<Order> grid0;

        protected RadzenButton gridDeleteButton;

        IEnumerable<Order> _getOrdersResult;
        protected IEnumerable<Order> getOrdersResult
        {
            get
            {
                return _getOrdersResult;
            }
            set
            {
                if(_getOrdersResult != value)
                {
                    _getOrdersResult = value;
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
            var northwindGetOrdersResult = await Northwind.GetOrders();
                getOrdersResult = northwindGetOrdersResult;
        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddOrder>("Add Order", null);
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(Order args)
        {
            var result = await DialogService.OpenAsync<EditOrder>("Edit Order", new Dictionary<string, object>() { {"OrderID", $"{args.OrderID}"} });
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Order data)
        {
            var northwindDeleteOrderResult = await Northwind.DeleteOrder(data.OrderID);
                if (northwindDeleteOrderResult != null) {
                    grid0.Reload();
}
        }
    }
}
