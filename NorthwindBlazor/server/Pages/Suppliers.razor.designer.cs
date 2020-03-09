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
    public partial class SuppliersComponent : ComponentBase
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

        protected RadzenGrid<NorthwindBlazor.Models.Northwind.Supplier> grid0;

        IEnumerable<NorthwindBlazor.Models.Northwind.Supplier> _getSuppliersResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.Supplier> getSuppliersResult
        {
            get
            {
                return _getSuppliersResult;
            }
            set
            {
                if(!object.Equals(_getSuppliersResult, value))
                {
                    _getSuppliersResult = value;
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
            var northwindGetSuppliersResult = await Northwind.GetSuppliers();
            getSuppliersResult = northwindGetSuppliersResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddSupplier>("Add Supplier", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(NorthwindBlazor.Models.Northwind.Supplier args)
        {
            var result = await DialogService.OpenAsync<EditSupplier>("Edit Supplier", new Dictionary<string, object>() { {"SupplierID", args.SupplierID} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var northwindDeleteSupplierResult = await Northwind.DeleteSupplier(data.SupplierID);
                if (northwindDeleteSupplierResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception northwindDeleteSupplierException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Supplier");
            }
        }
    }
}
