using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using BlazorCustomPaging.Models.Sample;
using Microsoft.EntityFrameworkCore;

namespace BlazorCustomPaging.Pages
{
    public partial class EditOrderDetailComponent : ComponentBase
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
        protected SampleService Sample { get; set; }

        [Parameter]
        public dynamic Id { get; set; }

        BlazorCustomPaging.Models.Sample.OrderDetail _orderdetail;
        protected BlazorCustomPaging.Models.Sample.OrderDetail orderdetail
        {
            get
            {
                return _orderdetail;
            }
            set
            {
                if(!object.Equals(_orderdetail, value))
                {
                    _orderdetail = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<BlazorCustomPaging.Models.Sample.Order> _getOrdersResult;
        protected IEnumerable<BlazorCustomPaging.Models.Sample.Order> getOrdersResult
        {
            get
            {
                return _getOrdersResult;
            }
            set
            {
                if(!object.Equals(_getOrdersResult, value))
                {
                    _getOrdersResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<BlazorCustomPaging.Models.Sample.Product> _getProductsResult;
        protected IEnumerable<BlazorCustomPaging.Models.Sample.Product> getProductsResult
        {
            get
            {
                return _getProductsResult;
            }
            set
            {
                if(!object.Equals(_getProductsResult, value))
                {
                    _getProductsResult = value;
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
            var sampleGetOrderDetailByIdResult = await Sample.GetOrderDetailById(Id);
            orderdetail = sampleGetOrderDetailByIdResult;

            var sampleGetOrdersResult = await Sample.GetOrders();
            getOrdersResult = sampleGetOrdersResult;

            var sampleGetProductsResult = await Sample.GetProducts();
            getProductsResult = sampleGetProductsResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(BlazorCustomPaging.Models.Sample.OrderDetail args)
        {
            try
            {
                var sampleUpdateOrderDetailResult = await Sample.UpdateOrderDetail(Id, orderdetail);
                DialogService.Close(orderdetail);
            }
            catch (Exception sampleUpdateOrderDetailException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update OrderDetail");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
