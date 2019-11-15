using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using BlazorMultiTenant.Models.Sample;
using Microsoft.AspNetCore.Identity;
using BlazorMultiTenant.Models;

namespace BlazorMultiTenant.Pages
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
        protected SecurityService Security { get; set; }

        [Inject]
        protected SampleService Sample { get; set; }


        [Parameter]
        public dynamic Id { get; set; }

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
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        BlazorMultiTenant.Models.Sample.OrderDetail _orderdetail;
        protected BlazorMultiTenant.Models.Sample.OrderDetail orderdetail
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
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<BlazorMultiTenant.Models.Sample.Order> _getOrdersResult;
        protected IEnumerable<BlazorMultiTenant.Models.Sample.Order> getOrdersResult
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
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<BlazorMultiTenant.Models.Sample.Product> _getProductsResult;
        protected IEnumerable<BlazorMultiTenant.Models.Sample.Product> getProductsResult
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
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            if (!Security.IsAuthenticated())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                Load();
            }

        }

        protected async void Load()
        {
            canEdit = true;

            var sampleGetOrderDetailByIdResult = await Sample.GetOrderDetailById(int.Parse($"{Id}"));
            orderdetail = sampleGetOrderDetailByIdResult;

            var sampleGetOrdersResult = await Sample.GetOrders();
            getOrdersResult = sampleGetOrdersResult;

            var sampleGetProductsResult = await Sample.GetProducts();
            getProductsResult = sampleGetProductsResult;
        }

        protected async void CloseButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async void Form0Submit(BlazorMultiTenant.Models.Sample.OrderDetail args)
        {
            try
            {
                var sampleUpdateOrderDetailResult = await Sample.UpdateOrderDetail(int.Parse($"{Id}"), orderdetail);
                DialogService.Close(orderdetail);
            }
            catch (Exception sampleUpdateOrderDetailException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update OrderDetail");
            }
        }

        protected async void Button3Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
