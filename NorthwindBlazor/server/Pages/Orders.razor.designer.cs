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
        protected NorthwindService Northwind { get; set; }

        protected RadzenGrid<NorthwindBlazor.Models.Northwind.Order> grid0;

        IEnumerable<NorthwindBlazor.Models.Northwind.Order> _getOrdersResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.Order> getOrdersResult
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

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var northwindGetOrdersResult = await Northwind.GetOrders();
            getOrdersResult = northwindGetOrdersResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddOrder>("Add Order", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(NorthwindBlazor.Models.Northwind.Order args)
        {
            var result = await DialogService.OpenAsync<EditOrder>("Edit Order", new Dictionary<string, object>() { {"OrderID", args.OrderID} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var northwindDeleteOrderResult = await Northwind.DeleteOrder(data.OrderID);
                if (northwindDeleteOrderResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception northwindDeleteOrderException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Order");
            }
        }
    }
}
