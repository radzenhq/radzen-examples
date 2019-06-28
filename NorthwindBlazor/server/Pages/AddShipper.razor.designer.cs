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
    public partial class AddShipperComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

        protected RadzenTemplateForm<Shipper> form0;

        protected RadzenLabel label1;

        protected RadzenTextBox companyName;

        protected RadzenRequiredValidator companyNameRequiredValidator;

        protected RadzenLabel label2;

        protected RadzenTextBox phone;

        protected RadzenButton button1;

        protected RadzenButton button2;

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
            shipper = new Shipper();
        }

        protected async void Form0Submit(Shipper args)
        {
            var northwindCreateShipperResult = await Northwind.CreateShipper(shipper);
                DialogService.Close(shipper);
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
