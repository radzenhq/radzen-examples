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
    public partial class ShippersComponent : ComponentBase
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
            var northwindGetShippersResult = await Northwind.GetShippers();
                getShippersResult = northwindGetShippersResult;
        }

        protected async void Button0Click(UIMouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddShipper>("Add Shipper", null);
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void Grid0RowSelect(Shipper args)
        {
            var result = await DialogService.OpenAsync<EditShipper>("Edit Shipper", new Dictionary<string, object>() { {"ShipperID", $"{args.ShipperID}"} });
              await Invoke(() => { StateHasChanged(); });
        }

        protected async void GridDeleteButtonClick(UIMouseEventArgs args, Shipper data)
        {
            var northwindDeleteShipperResult = await Northwind.DeleteShipper(data.ShipperID);
                if (northwindDeleteShipperResult != null) {
                    grid0.Reload();
}
        }
    }
}
