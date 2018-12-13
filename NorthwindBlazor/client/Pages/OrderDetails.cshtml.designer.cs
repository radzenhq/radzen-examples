using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using client.Shared;
using Radzen;
using Radzen.Blazor;
using NorthwindBlazor.Models.Northwind;

namespace NorthwindBlazor.App.Pages
{
    public partial class OrderDetailsModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

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
                    StateHasChanged();
                }
            }
        }

        int _getOrderDetailsCount;
        protected int getOrderDetailsCount
        {
            get
            {
                return _getOrderDetailsCount;
            }
            set
            {
                if(_getOrderDetailsCount != value)
                {
                    _getOrderDetailsCount = value;
                    StateHasChanged();
                }
            }
        }

        protected override async Task OnInitAsync()
        {
            Northwind.BasePath = UriHelper.GetBaseUri();

            await Task.Run(Load);
        }

        protected async void Load()
        {

        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("AddOrderDetail");
        }

        protected async void Grid0LoadData(LoadDataArgs args)
        {
            var northwindGetOrderDetailsResult = await Northwind.GetOrderDetails(null, args.Top, args.Skip, $"{args.OrderBy}", args.Top != null && args.Skip != null, $"Order,Product", null, null);
                getOrderDetailsResult = northwindGetOrderDetailsResult.Data;

                getOrderDetailsCount = northwindGetOrderDetailsResult.Count;
        }

        protected async void Grid0RowSelect(OrderDetail args)
        {
            UriHelper.NavigateTo($"EditOrderDetail/{args.OrderID}/{args.ProductID}");
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, OrderDetail data)
        {
            var northwindDeleteOrderDetailResult = await Northwind.DeleteOrderDetail(data.OrderID, data.ProductID);
                if (northwindDeleteOrderDetailResult.IsSuccessStatusCode) {
                    grid0.Reload();
}
        }
    }
}
