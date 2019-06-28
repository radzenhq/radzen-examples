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
    public partial class AddOrderDetailComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenTemplateForm<OrderDetail> form0;

        protected RadzenLabel label1;

        protected RadzenDropDown orderId;

        protected RadzenRequiredValidator orderIdRequiredValidator;

        protected RadzenLabel label2;

        protected RadzenDropDown productId;

        protected RadzenRequiredValidator productIdRequiredValidator;

        protected RadzenLabel label3;

        protected dynamic unitPrice;

        protected RadzenLabel label4;

        protected dynamic quantity;

        protected RadzenLabel label5;

        protected dynamic discount;

        protected RadzenButton button1;

        protected RadzenButton button2;

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

        IEnumerable<Product> _getProductsResult;
        protected IEnumerable<Product> getProductsResult
        {
            get
            {
                return _getProductsResult;
            }
            set
            {
                if(_getProductsResult != value)
                {
                    _getProductsResult = value;
                    Invoke(() => { StateHasChanged(); });
                }
            }
        }

        OrderDetail _orderdetail;
        protected OrderDetail orderdetail
        {
            get
            {
                return _orderdetail;
            }
            set
            {
                if(_orderdetail != value)
                {
                    _orderdetail = value;
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

            var northwindGetProductsResult = await Northwind.GetProducts();
                getProductsResult = northwindGetProductsResult;

            orderdetail = new OrderDetail();
        }

        protected async void Form0Submit(OrderDetail args)
        {
            var northwindCreateOrderDetailResult = await Northwind.CreateOrderDetail(orderdetail);
                DialogService.Close(orderdetail);
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
