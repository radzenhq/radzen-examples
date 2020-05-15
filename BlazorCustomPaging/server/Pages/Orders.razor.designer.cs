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
        protected SampleService Sample { get; set; }

        protected RadzenGrid<BlazorCustomPaging.Models.Sample.Order> grid0;

        int _PageSize;
        protected int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                if(!object.Equals(_PageSize, value))
                {
                    _PageSize = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        int _Start;
        protected int Start
        {
            get
            {
                return _Start;
            }
            set
            {
                if(!object.Equals(_Start, value))
                {
                    _Start = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        int _Count;
        protected int Count
        {
            get
            {
                return _Count;
            }
            set
            {
                if(!object.Equals(_Count, value))
                {
                    _Count = value;
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

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }

        protected async System.Threading.Tasks.Task Load()
        {
            PageSize = 10;

            Start = 0;

            Count = 0;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddOrder>("Add Order", null);
            grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            var sampleGetOrdersResult = await Sample.GetOrders(new Query() { Filter = $@"{args.Filter}", OrderBy = $"{args.OrderBy}", Top = PageSize, Skip = Start });
            getOrdersResult = sampleGetOrdersResult;

            var sampleGetOrdersResult0 = await Sample.GetOrders(new Query() { Filter = $@"{args.Filter}" });
            Count = sampleGetOrdersResult0.Count();
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(BlazorCustomPaging.Models.Sample.Order args)
        {
            var dialogResult = await DialogService.OpenAsync<EditOrder>("Edit Order", new Dictionary<string, object>() { {"Id", args.Id} });
            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
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

        protected async System.Threading.Tasks.Task Button1Click(MouseEventArgs args)
        {
            if (this.Start - this.PageSize >= 0) {
                Start = this.Start - this.PageSize;
            }

            grid0.Reload();
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            if (this.Start + this.PageSize <= this.Count - this.PageSize) {
                Start = this.Start + this.PageSize;
            }

            grid0.Reload();
        }

        protected async System.Threading.Tasks.Task Dropdown0Change(dynamic args)
        {
            grid0.Reload();
        }
    }
}
