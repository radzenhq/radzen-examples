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
    public partial class SummaryOfSalesByYearsComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenGrid<SummaryOfSalesByYear> grid0;

        IEnumerable<SummaryOfSalesByYear> _getSummaryOfSalesByYearsResult;
        protected IEnumerable<SummaryOfSalesByYear> getSummaryOfSalesByYearsResult
        {
            get
            {
                return _getSummaryOfSalesByYearsResult;
            }
            set
            {
                if(_getSummaryOfSalesByYearsResult != value)
                {
                    _getSummaryOfSalesByYearsResult = value;
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
            var northwindGetSummaryOfSalesByYearsResult = await Northwind.GetSummaryOfSalesByYears();
                getSummaryOfSalesByYearsResult = northwindGetSummaryOfSalesByYearsResult;
        }
    }
}
