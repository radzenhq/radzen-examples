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
    public partial class CustomerCustomerDemosComponent : ComponentBase
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

        protected RadzenGrid<NorthwindBlazor.Models.Northwind.CustomerCustomerDemo> grid0;

        IEnumerable<NorthwindBlazor.Models.Northwind.CustomerCustomerDemo> _getCustomerCustomerDemosResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.CustomerCustomerDemo> getCustomerCustomerDemosResult
        {
            get
            {
                return _getCustomerCustomerDemosResult;
            }
            set
            {
                if(!object.Equals(_getCustomerCustomerDemosResult, value))
                {
                    _getCustomerCustomerDemosResult = value;
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
            var northwindGetCustomerCustomerDemosResult = await Northwind.GetCustomerCustomerDemos();
            getCustomerCustomerDemosResult = northwindGetCustomerCustomerDemosResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddCustomerCustomerDemo>("Add Customer Customer Demo", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(NorthwindBlazor.Models.Northwind.CustomerCustomerDemo args)
        {
            var result = await DialogService.OpenAsync<EditCustomerCustomerDemo>("Edit Customer Customer Demo", new Dictionary<string, object>() { {"CustomerID", args.CustomerID}, {"CustomerTypeID", args.CustomerTypeID} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var northwindDeleteCustomerCustomerDemoResult = await Northwind.DeleteCustomerCustomerDemo($"{data.CustomerID}", $"{data.CustomerTypeID}");
                if (northwindDeleteCustomerCustomerDemoResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception northwindDeleteCustomerCustomerDemoException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete CustomerCustomerDemo");
            }
        }
    }
}
