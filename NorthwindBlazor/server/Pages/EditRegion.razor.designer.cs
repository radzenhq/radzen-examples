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
    public partial class EditRegionComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        [Parameter]
        protected string RegionID { get; set; }

        protected RadzenContent content1;

        protected RadzenLabel closeLabel;

        protected RadzenButton closeButton;

        protected RadzenTemplateForm<Region> form0;

        protected RadzenLabel label2;

        protected dynamic regionId;

        protected RadzenRequiredValidator regionIdRequiredValidator;

        protected RadzenLabel label3;

        protected RadzenTextBox regionDescription;

        protected RadzenRequiredValidator regionDescriptionRequiredValidator;

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
            canEdit = true;

            var northwindGetRegionByRegionIdResult = await Northwind.GetRegionByRegionId(int.Parse(RegionID));
                region = northwindGetRegionByRegionIdResult;
        }

        protected async void CloseButtonClick(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async void Form0Submit(Region args)
        {
            var northwindUpdateRegionResult = await Northwind.UpdateRegion(int.Parse(RegionID), region);
                DialogService.Close(region);
        }

        protected async void Button3Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
