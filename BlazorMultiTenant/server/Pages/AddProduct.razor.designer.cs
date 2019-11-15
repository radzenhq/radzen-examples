using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using BlazorMultiTenant.Models.Sample;
using Microsoft.AspNetCore.Identity;
using BlazorMultiTenant.Models;

namespace BlazorMultiTenant.Pages
{
    public partial class AddProductComponent : ComponentBase
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
        protected SecurityService Security { get; set; }

        [Inject]
        protected SampleService Sample { get; set; }


        BlazorMultiTenant.Models.Sample.Product _product;
        protected BlazorMultiTenant.Models.Sample.Product product
        {
            get
            {
                return _product;
            }
            set
            {
                if(_product != value)
                {
                    _product = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            if (!Security.IsAuthenticated())
            {
                UriHelper.NavigateTo("Login", true);
            }
            else
            {
                Load();
            }

        }

        protected async void Load()
        {
            product = new BlazorMultiTenant.Models.Sample.Product();
        }

        protected async void Form0Submit(BlazorMultiTenant.Models.Sample.Product args)
        {
            try
            {
                var sampleCreateProductResult = await Sample.CreateProduct(product);
                DialogService.Close(product);
            }
            catch (Exception sampleCreateProductException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to create new Product!");
            }
        }

        protected async void Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
