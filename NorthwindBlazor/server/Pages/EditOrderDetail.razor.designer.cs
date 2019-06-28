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
    public partial class EditOrderDetailComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        [Parameter]
        protected string OrderID { get; set; }

        [Parameter]
        protected string ProductID { get; set; }

        protected RadzenContent content1;

        protected RadzenLabel closeLabel;

        protected RadzenButton closeButton;

        protected RadzenTemplateForm<OrderDetail> form0;

        protected RadzenLabel label2;

        protected RadzenDropDown orderId;

        protected RadzenRequiredValidator orderIdRequiredValidator;

        protected RadzenLabel label3;

        protected RadzenDropDown productId;

        protected RadzenRequiredValidator productIdRequiredValidator;

        protected RadzenLabel label4;

        protected dynamic unitPrice;

        protected RadzenLabel label5;

        protected dynamic quantity;

        protected RadzenLabel label6;

        protected dynamic discount;

        protected RadzenButton button2;

        protected RadzenButton button3;

        bool _canEdit;
        protected bool canEdit
        {
            get
            {
                return _canEdit;
            }
            set
            {
                if(_canEdit != value)
                {
                    _canEdit = value;
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

        protected override async Task OnInitAsync()
        {
            await Task.Run(Load);
        }

        protected async void Load()
        {
            canEdit = true;

            var northwindGetOrderDetailByOrderIdAndProductIdResult = await Northwind.GetOrderDetailByOrderIdAndProductId(int.Parse(OrderID), int.Parse(ProductID));
                orderdetail = northwindGetOrderDetailByOrderIdAndProductIdResult;

            var northwindGetOrdersResult = await Northwind.GetOrders();
                getOrdersResult = northwindGetOrdersResult;

            var northwindGetProductsResult = await Northwind.GetProducts();
                getProductsResult = northwindGetProductsResult;
        }

        protected async void CloseButtonClick(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async void Form0Submit(OrderDetail args)
        {
            var northwindUpdateOrderDetailResult = await Northwind.UpdateOrderDetail(int.Parse(OrderID), int.Parse(ProductID), orderdetail);
                DialogService.Close(orderdetail);
        }

        protected async void Button3Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
