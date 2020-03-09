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
    public partial class EditShipperComponent : ComponentBase
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

        [Parameter]
        public dynamic ShipperID { get; set; }

        NorthwindBlazor.Models.Northwind.Shipper _shipper;
        protected NorthwindBlazor.Models.Northwind.Shipper shipper
        {
            get
            {
                return _shipper;
            }
            set
            {
                if(!object.Equals(_shipper, value))
                {
                    _shipper = value;
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
            var northwindGetShipperByShipperIdResult = await Northwind.GetShipperByShipperId(int.Parse($"{ShipperID}"));
            shipper = northwindGetShipperByShipperIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(NorthwindBlazor.Models.Northwind.Shipper args)
        {
            try
            {
                var northwindUpdateShipperResult = await Northwind.UpdateShipper(int.Parse($"{ShipperID}"), shipper);
                DialogService.Close(shipper);
            }
            catch (Exception northwindUpdateShipperException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update Shipper");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
