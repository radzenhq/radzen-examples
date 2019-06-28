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
    public partial class SummaryOfSalesByQuartersComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenGrid<SummaryOfSalesByQuarter> grid0;

        IEnumerable<SummaryOfSalesByQuarter> _getSummaryOfSalesByQuartersResult;
        protected IEnumerable<SummaryOfSalesByQuarter> getSummaryOfSalesByQuartersResult
        {
            get
            {
                return _getSummaryOfSalesByQuartersResult;
            }
            set
            {
                if(_getSummaryOfSalesByQuartersResult != value)
                {
                    _getSummaryOfSalesByQuartersResult = value;
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
            var northwindGetSummaryOfSalesByQuartersResult = await Northwind.GetSummaryOfSalesByQuarters();
                getSummaryOfSalesByQuartersResult = northwindGetSummaryOfSalesByQuartersResult;
        }
    }
}
