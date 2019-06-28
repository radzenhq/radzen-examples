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
    public partial class EditShipperComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        [Parameter]
        protected string ShipperID { get; set; }

        protected RadzenContent content1;

        protected RadzenLabel closeLabel;

        protected RadzenButton closeButton;

        protected RadzenTemplateForm<Shipper> form0;

        protected RadzenLabel label2;

        protected RadzenTextBox companyName;

        protected RadzenRequiredValidator companyNameRequiredValidator;

        protected RadzenLabel label3;

        protected RadzenTextBox phone;

        protected RadzenButton button2;

        protected RadzenButton button3;

        bool _canEdit;
        protected bool canEdit
        {
            get
            {
                return _canEdit;
            }
            set
            {
                if(_canEdit != value)
                {
                    _canEdit = value;
                    Invoke(() => { StateHasChanged(); });
                }
            }
        }

        Shipper _shipper;
        protected Shipper shipper
        {
            get
            {
                return _shipper;
            }
            set
            {
                if(_shipper != value)
                {
                    _shipper = value;
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
            canEdit = true;

            var northwindGetShipperByShipperIdResult = await Northwind.GetShipperByShipperId(int.Parse(ShipperID));
                shipper = northwindGetShipperByShipperIdResult;
        }

        protected async void CloseButtonClick(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async void Form0Submit(Shipper args)
        {
            var northwindUpdateShipperResult = await Northwind.UpdateShipper(int.Parse(ShipperID), shipper);
                DialogService.Close(shipper);
        }

        protected async void Button3Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
