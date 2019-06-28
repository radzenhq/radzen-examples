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
    public partial class AddTerritoryComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        protected RadzenContent content1;

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
                    Invoke(() => { StateHasChanged(); });
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
            var northwindGetRegionsResult = await Northwind.GetRegions();
                getRegionsResult = northwindGetRegionsResult;

            territory = new Territory();
        }

        protected async void Form0Submit(Territory args)
        {
            var northwindCreateTerritoryResult = await Northwind.CreateTerritory(territory);
                DialogService.Close(territory);
        }

        protected async void Button2Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
