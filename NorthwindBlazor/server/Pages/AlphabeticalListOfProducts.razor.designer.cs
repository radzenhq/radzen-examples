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
    public partial class AlphabeticalListOfProductsComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenGrid<AlphabeticalListOfProduct> grid0;

        IEnumerable<AlphabeticalListOfProduct> _getAlphabeticalListOfProductsResult;
        protected IEnumerable<AlphabeticalListOfProduct> getAlphabeticalListOfProductsResult
        {
            get
            {
                return _getAlphabeticalListOfProductsResult;
            }
            set
            {
                if(_getAlphabeticalListOfProductsResult != value)
                {
                    _getAlphabeticalListOfProductsResult = value;
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
            var northwindGetAlphabeticalListOfProductsResult = await Northwind.GetAlphabeticalListOfProducts();
                getAlphabeticalListOfProductsResult = northwindGetAlphabeticalListOfProductsResult;
        }
    }
}
