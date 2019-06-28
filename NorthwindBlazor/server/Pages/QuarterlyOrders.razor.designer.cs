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
    public partial class QuarterlyOrdersComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenGrid<QuarterlyOrder> grid0;

        IEnumerable<QuarterlyOrder> _getQuarterlyOrdersResult;
        protected IEnumerable<QuarterlyOrder> getQuarterlyOrdersResult
        {
            get
            {
                return _getQuarterlyOrdersResult;
            }
            set
            {
                if(_getQuarterlyOrdersResult != value)
                {
                    _getQuarterlyOrdersResult = value;
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
            var northwindGetQuarterlyOrdersResult = await Northwind.GetQuarterlyOrders();
                getQuarterlyOrdersResult = northwindGetQuarterlyOrdersResult;
        }
    }
}
