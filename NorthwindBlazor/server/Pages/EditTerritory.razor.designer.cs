using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using NorthwindBlazor.Models.Northwind;
using Microsoft.EntityFrameworkCore;

namespace NorthwindBlazor.Pages
{
    public partial class EditTerritoryComponent : ComponentBase
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
        protected NorthwindService Northwind { get; set; }

        [Parameter]
        public dynamic TerritoryID { get; set; }

        NorthwindBlazor.Models.Northwind.Territory _territory;
        protected NorthwindBlazor.Models.Northwind.Territory territory
        {
            get
            {
                return _territory;
            }
            set
            {
                if(!object.Equals(_territory, value))
                {
                    _territory = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        IEnumerable<NorthwindBlazor.Models.Northwind.Region> _getRegionsResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.Region> getRegionsResult
        {
            get
            {
                return _getRegionsResult;
            }
            set
            {
                if(!object.Equals(_getRegionsResult, value))
                {
                    _getRegionsResult = value;
                    InvokeAsync(() => { StateHasChanged(); });
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var northwindGetTerritoryByTerritoryIdResult = await Northwind.GetTerritoryByTerritoryId($"{TerritoryID}");
            territory = northwindGetTerritoryByTerritoryIdResult;

            var northwindGetRegionsResult = await Northwind.GetRegions();
            getRegionsResult = northwindGetRegionsResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(NorthwindBlazor.Models.Northwind.Territory args)
        {
            try
            {
                var northwindUpdateTerritoryResult = await Northwind.UpdateTerritory($"{TerritoryID}", territory);
                DialogService.Close(territory);
            }
            catch (Exception northwindUpdateTerritoryException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to update Territory");
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
