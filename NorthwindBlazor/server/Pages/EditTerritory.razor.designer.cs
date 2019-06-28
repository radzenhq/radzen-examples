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
    public partial class EditTerritoryComponent : ComponentBase
    {
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }
        [Inject]
        protected NorthwindService Northwind { get; set; }


        [Parameter]
        protected string TerritoryID { get; set; }

        protected RadzenContent content1;

        protected RadzenLabel closeLabel;

        protected RadzenButton closeButton;

        protected RadzenTemplateForm<Territory> form0;

        protected RadzenLabel label2;

        protected RadzenTextBox territoryId;

        protected RadzenRequiredValidator territoryIdRequiredValidator;

        protected RadzenLabel label3;

        protected RadzenTextBox territoryDescription;

        protected RadzenRequiredValidator territoryDescriptionRequiredValidator;

        protected RadzenLabel label4;

        protected RadzenDropDown regionId;

        protected RadzenRequiredValidator regionIdRequiredValidator;

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

        protected override async Task OnInitAsync()
        {
            await Task.Run(Load);
        }

        protected async void Load()
        {
            canEdit = true;

            var northwindGetTerritoryByTerritoryIdResult = await Northwind.GetTerritoryByTerritoryId($"{TerritoryID}");
                territory = northwindGetTerritoryByTerritoryIdResult;

            var northwindGetRegionsResult = await Northwind.GetRegions();
                getRegionsResult = northwindGetRegionsResult;
        }

        protected async void CloseButtonClick(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }

        protected async void Form0Submit(Territory args)
        {
            var northwindUpdateTerritoryResult = await Northwind.UpdateTerritory($"{TerritoryID}", territory);
                DialogService.Close(territory);
        }

        protected async void Button3Click(UIMouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
