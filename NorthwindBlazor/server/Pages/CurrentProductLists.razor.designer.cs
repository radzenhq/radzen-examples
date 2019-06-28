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
    public partial class CurrentProductListsComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenGrid<CurrentProductList> grid0;

        IEnumerable<CurrentProductList> _getCurrentProductListsResult;
        protected IEnumerable<CurrentProductList> getCurrentProductListsResult
        {
            get
            {
                return _getCurrentProductListsResult;
            }
            set
            {
                if(_getCurrentProductListsResult != value)
                {
                    _getCurrentProductListsResult = value;
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
            var northwindGetCurrentProductListsResult = await Northwind.GetCurrentProductLists();
                getCurrentProductListsResult = northwindGetCurrentProductListsResult;
        }
    }
}
