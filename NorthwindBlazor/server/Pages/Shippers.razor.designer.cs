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
    public partial class ShippersComponent : ComponentBase
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

        protected RadzenGrid<NorthwindBlazor.Models.Northwind.Shipper> grid0;

        IEnumerable<NorthwindBlazor.Models.Northwind.Shipper> _getShippersResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.Shipper> getShippersResult
        {
            get
            {
                return _getShippersResult;
            }
            set
            {
                if(!object.Equals(_getShippersResult, value))
                {
                    _getShippersResult = value;
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
            var northwindGetShippersResult = await Northwind.GetShippers();
            getShippersResult = northwindGetShippersResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddShipper>("Add Shipper", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(NorthwindBlazor.Models.Northwind.Shipper args)
        {
            var result = await DialogService.OpenAsync<EditShipper>("Edit Shipper", new Dictionary<string, object>() { {"ShipperID", args.ShipperID} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var northwindDeleteShipperResult = await Northwind.DeleteShipper(data.ShipperID);
                if (northwindDeleteShipperResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception northwindDeleteShipperException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Shipper");
            }
        }
    }
}
