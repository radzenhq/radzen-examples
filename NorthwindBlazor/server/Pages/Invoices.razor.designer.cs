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
    public partial class InvoicesComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenGrid<Invoice> grid0;

        IEnumerable<Invoice> _getInvoicesResult;
        protected IEnumerable<Invoice> getInvoicesResult
        {
            get
            {
                return _getInvoicesResult;
            }
            set
            {
                if(_getInvoicesResult != value)
                {
                    _getInvoicesResult = value;
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
            var northwindGetInvoicesResult = await Northwind.GetInvoices();
                getInvoicesResult = northwindGetInvoicesResult;
        }
    }
}
