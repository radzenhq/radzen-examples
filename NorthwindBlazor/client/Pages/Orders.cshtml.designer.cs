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
    public partial class OrdersModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

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
                    StateHasChanged();
                }
            }
        }

        int _getOrdersCount;
        protected int getOrdersCount
        {
            get
            {
                return _getOrdersCount;
            }
            set
            {
                if(_getOrdersCount != value)
                {
                    _getOrdersCount = value;
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
            UriHelper.NavigateTo("AddOrder");
        }

        protected async void Grid0LoadData(LoadDataArgs args)
        {
            var northwindGetOrdersResult = await Northwind.GetOrders(null, args.Top, args.Skip, $"{args.OrderBy}", args.Top != null && args.Skip != null, $"Customer,Employee,Shipper", null, null);
                getOrdersResult = northwindGetOrdersResult.Data;

                getOrdersCount = northwindGetOrdersResult.Count;
        }

        protected async void Grid0RowSelect(Order args)
        {
            UriHelper.NavigateTo($"EditOrder/{args.OrderID}");
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Order data)
        {
            var northwindDeleteOrderResult = await Northwind.DeleteOrder(data.OrderID);
                if (northwindDeleteOrderResult.IsSuccessStatusCode) {
                    grid0.Reload();
}
        }
    }
}
