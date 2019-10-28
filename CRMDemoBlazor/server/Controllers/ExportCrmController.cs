using Microsoft.AspNetCore.Mvc;
using RadzenCrm.Data;

namespace RadzenCrm
{
    public partial class ExportCrmController : ExportController
    {
        private readonly CrmContext context;

        public ExportCrmController(CrmContext context)
        {
            this.context = context;
        }

        [HttpGet("/export/Crm/contacts/csv")]
        public FileStreamResult ExportContactsToCSV()
        {
            return ToCSV(ApplyQuery(context.Contacts, Request.Query));
        }

        [HttpGet("/export/Crm/contacts/excel")]
        public FileStreamResult ExportContactsToExcel()
        {
            return ToExcel(ApplyQuery(context.Contacts, Request.Query));
        }

        [HttpGet("/export/Crm/opportunities/csv")]
        public FileStreamResult ExportOpportunitiesToCSV()
        {
            return ToCSV(ApplyQuery(context.Opportunities, Request.Query));
        }

        [HttpGet("/export/Crm/opportunities/excel")]
        public FileStreamResult ExportOpportunitiesToExcel()
        {
            return ToExcel(ApplyQuery(context.Opportunities, Request.Query));
        }

        [HttpGet("/export/Crm/opportunitystatuses/csv")]
        public FileStreamResult ExportOpportunityStatusesToCSV()
        {
            return ToCSV(ApplyQuery(context.OpportunityStatuses, Request.Query));
        }

        [HttpGet("/export/Crm/opportunitystatuses/excel")]
        public FileStreamResult ExportOpportunityStatusesToExcel()
        {
            return ToExcel(ApplyQuery(context.OpportunityStatuses, Request.Query));
        }

        [HttpGet("/export/Crm/tasks/csv")]
        public FileStreamResult ExportTasksToCSV()
        {
            return ToCSV(ApplyQuery(context.Tasks, Request.Query));
        }

        [HttpGet("/export/Crm/tasks/excel")]
        public FileStreamResult ExportTasksToExcel()
        {
            return ToExcel(ApplyQuery(context.Tasks, Request.Query));
        }

        [HttpGet("/export/Crm/taskstatuses/csv")]
        public FileStreamResult ExportTaskStatusesToCSV()
        {
            return ToCSV(ApplyQuery(context.TaskStatuses, Request.Query));
        }

        [HttpGet("/export/Crm/taskstatuses/excel")]
        public FileStreamResult ExportTaskStatusesToExcel()
        {
            return ToExcel(ApplyQuery(context.TaskStatuses, Request.Query));
        }

        [HttpGet("/export/Crm/tasktypes/csv")]
        public FileStreamResult ExportTaskTypesToCSV()
        {
            return ToCSV(ApplyQuery(context.TaskTypes, Request.Query));
        }

        [HttpGet("/export/Crm/tasktypes/excel")]
        public FileStreamResult ExportTaskTypesToExcel()
        {
            return ToExcel(ApplyQuery(context.TaskTypes, Request.Query));
        }
    }
}
