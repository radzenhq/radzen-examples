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
    public partial class EditTerritoryModel : BlazorComponent
    {
        [Inject]
        NorthwindService Northwind { get; set; }

        [Inject]
        IUriHelper UriHelper { get; set; }

        [Parameter]
        protected string TerritoryID { get; set; }

        protected RadzenContent content1;

        protected RadzenHeading pageTitle;

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

        protected override async Task OnInitAsync()
        {
            Northwind.BasePath = UriHelper.GetBaseUri();

            await Task.Run(Load);
        }

        protected async void Load()
        {
            canEdit = true;

            var northwindGetTerritoryByTerritoryIdResult = await Northwind.GetTerritoryByTerritoryId($"{TerritoryID}");
                territory = northwindGetTerritoryByTerritoryIdResult;

            var northwindGetRegionsResult = await Northwind.GetRegions(null, null, null, null, null, null, null, null);
                getRegionsResult = northwindGetRegionsResult.Data;
        }

        protected async void CloseButtonClick(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("Territories");
        }

        protected async void Form0Submit(Territory args)
        {
            var northwindUpdateTerritoryResult = await Northwind.UpdateTerritory($"{TerritoryID}", territory);
                UriHelper.NavigateTo("Territories");
        }

        protected async void Button3Click(UIMouseEventArgs args)
        {
            UriHelper.NavigateTo("Territories");
        }
    }
}
