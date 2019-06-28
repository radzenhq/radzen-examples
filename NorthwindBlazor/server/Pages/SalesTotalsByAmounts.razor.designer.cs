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
    public partial class SalesTotalsByAmountsComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenGrid<SalesTotalsByAmount> grid0;

        IEnumerable<SalesTotalsByAmount> _getSalesTotalsByAmountsResult;
        protected IEnumerable<SalesTotalsByAmount> getSalesTotalsByAmountsResult
        {
            get
            {
                return _getSalesTotalsByAmountsResult;
            }
            set
            {
                if(_getSalesTotalsByAmountsResult != value)
                {
                    _getSalesTotalsByAmountsResult = value;
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
            var northwindGetSalesTotalsByAmountsResult = await Northwind.GetSalesTotalsByAmounts();
                getSalesTotalsByAmountsResult = northwindGetSalesTotalsByAmountsResult;
        }
    }
}
