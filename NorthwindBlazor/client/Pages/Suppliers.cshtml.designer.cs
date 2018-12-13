using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using client.Shared;
using Radzen;
using Radzen.Blazor;
using NorthwindBlazor.Models.Northwind;

namespace NorthwindBlazor.App.Pages
{
    public partial class SuppliersModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

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
                    StateHasChanged();
                }
            }
        }

        int _getSuppliersCount;
        protected int getSuppliersCount
        {
            get
            {
                return _getSuppliersCount;
            }
            set
            {
                if(_getSuppliersCount != value)
                {
                    _getSuppliersCount = value;
                    StateHasChanged();
                }
            }
        }

        protected override async Task OnInitAsync()
        {
            Northwind.BasePath = UriHelper.GetBaseUri();

            await Task.Run(Load);
        }

        protected async void Load()
        {

        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("AddSupplier");
        }

        protected async void Grid0LoadData(LoadDataArgs args)
        {
            var northwindGetSuppliersResult = await Northwind.GetSuppliers(null, args.Top, args.Skip, $"{args.OrderBy}", args.Top != null && args.Skip != null, $"", null, null);
                getSuppliersResult = northwindGetSuppliersResult.Data;

                getSuppliersCount = northwindGetSuppliersResult.Count;
        }

        protected async void Grid0RowSelect(Supplier args)
        {
            UriHelper.NavigateTo($"EditSupplier/{args.SupplierID}");
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Supplier data)
        {
            var northwindDeleteSupplierResult = await Northwind.DeleteSupplier(data.SupplierID);
                if (northwindDeleteSupplierResult.IsSuccessStatusCode) {
                    grid0.Reload();
}
        }
    }
}
