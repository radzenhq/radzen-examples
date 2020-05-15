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
        protected SampleService Sample { get; set; }

        protected RadzenGrid<BlazorCustomPaging.Models.Sample.OrderDetail> grid0;

        IEnumerable<BlazorCustomPaging.Models.Sample.OrderDetail> _getOrderDetailsResult;
        protected IEnumerable<BlazorCustomPaging.Models.Sample.OrderDetail> getOrderDetailsResult
        {
            get
            {
                return _getOrderDetailsResult;
            }
            set
            {
                if(!object.Equals(_getOrderDetailsResult, value))
                {
                    _getOrderDetailsResult = value;
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
            var sampleGetOrderDetailsResult = await Sample.GetOrderDetails();
            getOrderDetailsResult = sampleGetOrderDetailsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddOrderDetail>("Add Order Detail", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(BlazorCustomPaging.Models.Sample.OrderDetail args)
        {
            var dialogResult = await DialogService.OpenAsync<EditOrderDetail>("Edit Order Detail", new Dictionary<string, object>() { {"Id", args.Id} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
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
