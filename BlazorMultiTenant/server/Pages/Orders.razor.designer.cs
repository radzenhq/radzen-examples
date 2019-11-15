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
    public partial class OrdersComponent : ComponentBase
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


        protected RadzenGrid<BlazorMultiTenant.Models.Sample.Order> grid0;

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
            var sampleGetOrdersResult = await Sample.GetOrders();
            getOrdersResult = sampleGetOrdersResult;
        }

        protected async void Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddOrder>("Add Order", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(BlazorMultiTenant.Models.Sample.Order args)
        {
            var result = await DialogService.OpenAsync<EditOrder>("Edit Order", new Dictionary<string, object>() { {"Id", args.Id} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var sampleDeleteOrderResult = await Sample.DeleteOrder(data.Id);
                if (sampleDeleteOrderResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception sampleDeleteOrderException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Order");
            }
        }
    }
}
