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
    public partial class OrderDetailsComponent : ComponentBase
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

        protected RadzenGrid<OrderDetail> grid0;

        protected RadzenButton gridDeleteButton;

        IEnumerable<OrderDetail> _getOrderDetailsResult;
        protected IEnumerable<OrderDetail> getOrderDetailsResult
        {
            get
            {
                return _getOrderDetailsResult;
            }
            set
            {
                if(_getOrderDetailsResult != value)
                {
                    _getOrderDetailsResult = value;
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
            var northwindGetOrderDetailsResult = await Northwind.GetOrderDetails();
                getOrderDetailsResult = northwindGetOrderDetailsResult;
        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddOrderDetail>("Add Order Detail", null);
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(OrderDetail args)
        {
            var result = await DialogService.OpenAsync<EditOrderDetail>("Edit Order Detail", new Dictionary<string, object>() { {"OrderID", $"{args.OrderID}"}, {"ProductID", $"{args.ProductID}"} });
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, OrderDetail data)
        {
            var northwindDeleteOrderDetailResult = await Northwind.DeleteOrderDetail(data.OrderID, data.ProductID);
                if (northwindDeleteOrderDetailResult != null) {
                    grid0.Reload();
}
        }
    }
}
