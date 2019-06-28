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
    public partial class AddRegionComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

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
            region = new Region();
        }

        protected async void Form0Submit(Region args)
        {
            var northwindCreateRegionResult = await Northwind.CreateRegion(region);
                DialogService.Close(region);
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
