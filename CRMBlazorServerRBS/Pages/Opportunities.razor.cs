using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace CRMBlazorServerRBS.Pages
{
    public partial class Opportunities
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        public RadzenCRMService RadzenCRMService { get; set; }

        protected IEnumerable<CRMBlazorServerRBS.Models.RadzenCRM.Opportunity> opportunities;

        protected RadzenDataGrid<CRMBlazorServerRBS.Models.RadzenCRM.Opportunity> grid0;

        protected string search = "";

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            opportunities = await RadzenCRMService.GetOpportunities(new Query { Filter = $@"i => i.Name.Contains(@0) || i.UserId.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Contact,OpportunityStatus" });
        }
        protected override async Task OnInitializedAsync()
        {
            opportunities = await RadzenCRMService.GetOpportunities(new Query { Filter = $@"i => i.Name.Contains(@0) || i.UserId.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Contact,OpportunityStatus" });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddOpportunity>("Add Opportunity", null);
            await grid0.Reload();
        }

        protected async Task EditRow(CRMBlazorServerRBS.Models.RadzenCRM.Opportunity args)
        {
            await DialogService.OpenAsync<EditOpportunity>("Edit Opportunity", new Dictionary<string, object> { {"Id", args.Id} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, CRMBlazorServerRBS.Models.RadzenCRM.Opportunity opportunity)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await RadzenCRMService.DeleteOpportunity(opportunity.Id);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                { 
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error", 
                    Detail = $"Unable to delete Opportunity" 
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await RadzenCRMService.ExportOpportunitiesToCSV(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "Contact,OpportunityStatus", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Opportunities");
            }

            if (args == null || args.Value == "xlsx")
            {
                await RadzenCRMService.ExportOpportunitiesToExcel(new Query
{ 
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", 
    OrderBy = $"{grid0.Query.OrderBy}", 
    Expand = "Contact,OpportunityStatus", 
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Opportunities");
            }
        }
    }
}