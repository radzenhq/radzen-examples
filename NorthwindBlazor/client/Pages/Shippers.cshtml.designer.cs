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
    public partial class ShippersModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenButton button0;

        protected RadzenGrid<Shipper> grid0;

        protected RadzenButton gridDeleteButton;

        IEnumerable<Shipper> _getShippersResult;
        protected IEnumerable<Shipper> getShippersResult
        {
            get
            {
                return _getShippersResult;
            }
            set
            {
                if(_getShippersResult != value)
                {
                    _getShippersResult = value;
                    StateHasChanged();
                }
            }
        }

        int _getShippersCount;
        protected int getShippersCount
        {
            get
            {
                return _getShippersCount;
            }
            set
            {
                if(_getShippersCount != value)
                {
                    _getShippersCount = value;
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
            UriHelper.NavigateTo("AddShipper");
        }

        protected async void Grid0LoadData(LoadDataArgs args)
        {
            var northwindGetShippersResult = await Northwind.GetShippers(null, args.Top, args.Skip, $"{args.OrderBy}", args.Top != null && args.Skip != null, $"", null, null);
                getShippersResult = northwindGetShippersResult.Data;

                getShippersCount = northwindGetShippersResult.Count;
        }

        protected async void Grid0RowSelect(Shipper args)
        {
            UriHelper.NavigateTo($"EditShipper/{args.ShipperID}");
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Shipper data)
        {
            var northwindDeleteShipperResult = await Northwind.DeleteShipper(data.ShipperID);
                if (northwindDeleteShipperResult.IsSuccessStatusCode) {
                    grid0.Reload();
}
        }
    }
}
