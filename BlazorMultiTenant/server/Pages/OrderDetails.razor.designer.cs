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
    public partial class OrderDetailsComponent : ComponentBase
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


        protected RadzenGrid<BlazorMultiTenant.Models.Sample.OrderDetail> grid0;

        IEnumerable<BlazorMultiTenant.Models.Sample.OrderDetail> _getOrderDetailsResult;
        protected IEnumerable<BlazorMultiTenant.Models.Sample.OrderDetail> getOrderDetailsResult
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
            var sampleGetOrderDetailsResult = await Sample.GetOrderDetails();
            getOrderDetailsResult = sampleGetOrderDetailsResult;
        }

        protected async void Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddOrderDetail>("Add Order Detail", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(BlazorMultiTenant.Models.Sample.OrderDetail args)
        {
            var result = await DialogService.OpenAsync<EditOrderDetail>("Edit Order Detail", new Dictionary<string, object>() { {"Id", args.Id} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var sampleDeleteOrderDetailResult = await Sample.DeleteOrderDetail(data.Id);
                if (sampleDeleteOrderDetailResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception sampleDeleteOrderDetailException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete OrderDetail");
            }
        }
    }
}
