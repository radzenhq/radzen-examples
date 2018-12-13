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
    public partial class AddSupplierModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenTemplateForm<Supplier> form0;

        protected RadzenLabel label1;

        protected RadzenTextBox companyName;

        protected RadzenRequiredValidator companyNameRequiredValidator;

        protected RadzenLabel label2;

        protected RadzenTextBox contactName;

        protected RadzenLabel label3;

        protected RadzenTextBox contactTitle;

        protected RadzenLabel label4;

        protected RadzenTextBox address;

        protected RadzenLabel label5;

        protected RadzenTextBox city;

        protected RadzenLabel label6;

        protected RadzenTextBox region;

        protected RadzenLabel label7;

        protected RadzenTextBox postalCode;

        protected RadzenLabel label8;

        protected RadzenTextBox country;

        protected RadzenLabel label9;

        protected RadzenTextBox phone;

        protected RadzenLabel label10;

        protected RadzenTextBox fax;

        protected RadzenLabel label11;

        protected RadzenTextBox homePage;

        protected RadzenButton button1;

        protected RadzenButton button2;

        Supplier _supplier;
        protected Supplier supplier
        {
            get
            {
                return _supplier;
            }
            set
            {
                if(_supplier != value)
                {
                    _supplier = value;
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
            supplier = new Supplier();
        }

        protected async void Form0Submit(Supplier args)
        {
            var northwindCreateSupplierResult = await Northwind.CreateSupplier(supplier);
                UriHelper.NavigateTo("Suppliers");
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("Suppliers");
        }
    }
}
