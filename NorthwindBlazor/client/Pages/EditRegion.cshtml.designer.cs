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
    public partial class EditRegionModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        [Parameter]
        protected string RegionID { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

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
                    StateHasChanged();
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
            canEdit = true;

            var northwindGetRegionByRegionIdResult = await Northwind.GetRegionByRegionId(int.Parse(RegionID));
                region = northwindGetRegionByRegionIdResult;
        }

        protected async void CloseButtonClick(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("Regions");
        }

        protected async void Form0Submit(Region args)
        {
            var northwindUpdateRegionResult = await Northwind.UpdateRegion(int.Parse(RegionID), region);
                UriHelper.NavigateTo("Regions");
        }

        protected async void Button3Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("Regions");
        }
    }
}
