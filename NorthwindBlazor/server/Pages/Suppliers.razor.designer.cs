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
    public partial class SuppliersComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenButton button0;

        protected RadzenGrid<Supplier> grid0;

        protected RadzenButton gridDeleteButton;

        IEnumerable<Supplier> _getSuppliersResult;
        protected IEnumerable<Supplier> getSuppliersResult
        {
            get
            {
                return _getSuppliersResult;
            }
            set
            {
                if(_getSuppliersResult != value)
                {
                    _getSuppliersResult = value;
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
            var northwindGetSuppliersResult = await Northwind.GetSuppliers();
                getSuppliersResult = northwindGetSuppliersResult;
        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddSupplier>("Add Supplier", null);
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(Supplier args)
        {
            var result = await DialogService.OpenAsync<EditSupplier>("Edit Supplier", new Dictionary<string, object>() { {"SupplierID", $"{args.SupplierID}"} });
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Supplier data)
        {
            var northwindDeleteSupplierResult = await Northwind.DeleteSupplier(data.SupplierID);
                if (northwindDeleteSupplierResult != null) {
                    grid0.Reload();
}
        }
    }
}
