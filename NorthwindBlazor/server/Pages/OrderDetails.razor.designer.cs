using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using NorthwindBlazor.Models.Northwind;
using Microsoft.EntityFrameworkCore;

namespace NorthwindBlazor.Pages
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
        protected NorthwindService Northwind { get; set; }

        protected RadzenGrid<NorthwindBlazor.Models.Northwind.OrderDetail> grid0;

        IEnumerable<NorthwindBlazor.Models.Northwind.OrderDetail> _getOrderDetailsResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.OrderDetail> getOrderDetailsResult
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
            var northwindGetOrderDetailsResult = await Northwind.GetOrderDetails();
            getOrderDetailsResult = northwindGetOrderDetailsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddOrderDetail>("Add Order Detail", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(NorthwindBlazor.Models.Northwind.OrderDetail args)
        {
            var result = await DialogService.OpenAsync<EditOrderDetail>("Edit Order Detail", new Dictionary<string, object>() { {"OrderID", args.OrderID}, {"ProductID", args.ProductID} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var northwindDeleteOrderDetailResult = await Northwind.DeleteOrderDetail(data.OrderID, data.ProductID);
                if (northwindDeleteOrderDetailResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception northwindDeleteOrderDetailException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete OrderDetail");
            }
        }
    }
}
