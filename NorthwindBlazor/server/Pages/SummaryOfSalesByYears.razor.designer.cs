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
    public partial class SummaryOfSalesByYearsComponent : ComponentBase
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

        protected RadzenGrid<NorthwindBlazor.Models.Northwind.SummaryOfSalesByYear> grid0;

        IEnumerable<NorthwindBlazor.Models.Northwind.SummaryOfSalesByYear> _getSummaryOfSalesByYearsResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.SummaryOfSalesByYear> getSummaryOfSalesByYearsResult
        {
            get
            {
                return _getSummaryOfSalesByYearsResult;
            }
            set
            {
                if(!object.Equals(_getSummaryOfSalesByYearsResult, value))
                {
                    _getSummaryOfSalesByYearsResult = value;
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
            var northwindGetSummaryOfSalesByYearsResult = await Northwind.GetSummaryOfSalesByYears();
            getSummaryOfSalesByYearsResult = northwindGetSummaryOfSalesByYearsResult;
        }
    }
}
