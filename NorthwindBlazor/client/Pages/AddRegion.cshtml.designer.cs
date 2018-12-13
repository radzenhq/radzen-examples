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
    public partial class AddRegionModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenTemplateForm<Region> form0;

        protected RadzenLabel label1;

        protected dynamic regionId;

        protected RadzenRequiredValidator regionIdRequiredValidator;

        protected RadzenLabel label2;

        protected RadzenTextBox regionDescription;

        protected RadzenRequiredValidator regionDescriptionRequiredValidator;

        protected RadzenButton button1;

        protected RadzenButton button2;

        Region _region;
        protected Region region
        {
            get
            {
                return _region;
            }
            set
            {
                if(_region != value)
                {
                    _region = value;
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
            region = new Region();
        }

        protected async void Form0Submit(Region args)
        {
            var northwindCreateRegionResult = await Northwind.CreateRegion(region);
                UriHelper.NavigateTo("Regions");
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("Regions");
        }
    }
}
