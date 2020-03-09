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
    public partial class TerritoriesComponent : ComponentBase
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

        protected RadzenGrid<NorthwindBlazor.Models.Northwind.Territory> grid0;

        IEnumerable<NorthwindBlazor.Models.Northwind.Territory> _getTerritoriesResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.Territory> getTerritoriesResult
        {
            get
            {
                return _getTerritoriesResult;
            }
            set
            {
                if(!object.Equals(_getTerritoriesResult, value))
                {
                    _getTerritoriesResult = value;
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
            var northwindGetTerritoriesResult = await Northwind.GetTerritories();
            getTerritoriesResult = northwindGetTerritoriesResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddTerritory>("Add Territory", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(NorthwindBlazor.Models.Northwind.Territory args)
        {
            var result = await DialogService.OpenAsync<EditTerritory>("Edit Territory", new Dictionary<string, object>() { {"TerritoryID", args.TerritoryID} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var northwindDeleteTerritoryResult = await Northwind.DeleteTerritory($"{data.TerritoryID}");
                if (northwindDeleteTerritoryResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception northwindDeleteTerritoryException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Territory");
            }
        }
    }
}
