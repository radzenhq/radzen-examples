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
    public partial class AddShipperModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

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
            shipper = new Shipper();
        }

        protected async void Form0Submit(Shipper args)
        {
            var northwindCreateShipperResult = await Northwind.CreateShipper(shipper);
                UriHelper.NavigateTo("Shippers");
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("Shippers");
        }
    }
}
