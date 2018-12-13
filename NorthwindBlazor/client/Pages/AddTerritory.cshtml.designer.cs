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
    public partial class AddTerritoryModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

        protected RadzenTemplateForm<Territory> form0;

        protected RadzenLabel label1;

        protected RadzenTextBox territoryId;

        protected RadzenRequiredValidator territoryIdRequiredValidator;

        protected RadzenLabel label2;

        protected RadzenTextBox territoryDescription;

        protected RadzenRequiredValidator territoryDescriptionRequiredValidator;

        protected RadzenLabel label3;

        protected RadzenDropDown regionId;

        protected RadzenRequiredValidator regionIdRequiredValidator;

        protected RadzenButton button1;

        protected RadzenButton button2;

        IEnumerable<Region> _getRegionsResult;
        protected IEnumerable<Region> getRegionsResult
        {
            get
            {
                return _getRegionsResult;
            }
            set
            {
                if(_getRegionsResult != value)
                {
                    _getRegionsResult = value;
                    StateHasChanged();
                }
            }
        }

        Territory _territory;
        protected Territory territory
        {
            get
            {
                return _territory;
            }
            set
            {
                if(_territory != value)
                {
                    _territory = value;
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
            var northwindGetRegionsResult = await Northwind.GetRegions(null, null, null, null, null, null, null, null);
                getRegionsResult = northwindGetRegionsResult.Data;

            territory = new Territory();
        }

        protected async void Form0Submit(Territory args)
        {
            var northwindCreateTerritoryResult = await Northwind.CreateTerritory(territory);
                UriHelper.NavigateTo("Territories");
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("Territories");
        }
    }
}
