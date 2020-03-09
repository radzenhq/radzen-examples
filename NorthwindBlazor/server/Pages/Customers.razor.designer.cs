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
    public partial class CustomersComponent : ComponentBase
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

        protected RadzenGrid<NorthwindBlazor.Models.Northwind.Customer> grid0;

        IEnumerable<NorthwindBlazor.Models.Northwind.Customer> _getCustomersResult;
        protected IEnumerable<NorthwindBlazor.Models.Northwind.Customer> getCustomersResult
        {
            get
            {
                return _getCustomersResult;
            }
            set
            {
                if(!object.Equals(_getCustomersResult, value))
                {
                    _getCustomersResult = value;
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
            var northwindGetCustomersResult = await Northwind.GetCustomers();
            getCustomersResult = northwindGetCustomersResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var result = await DialogService.OpenAsync<AddCustomer>("Add Customer", null);
              grid0.Reload();

              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(NorthwindBlazor.Models.Northwind.Customer args)
        {
            var result = await DialogService.OpenAsync<EditCustomer>("Edit Customer", new Dictionary<string, object>() { {"CustomerID", args.CustomerID} });
              await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                var northwindDeleteCustomerResult = await Northwind.DeleteCustomer($"{data.CustomerID}");
                if (northwindDeleteCustomerResult != null) {
                    grid0.Reload();
}
            }
            catch (Exception northwindDeleteCustomerException)
            {
                    NotificationService.Notify(NotificationSeverity.Error, $"Error", $"Unable to delete Customer");
            }
        }
    }
}
