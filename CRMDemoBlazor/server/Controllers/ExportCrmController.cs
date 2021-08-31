using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet("/export/Crm/contacts/csv(fileName='{fileName}')")]
        public FileStreamResult ExportContactsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Contacts, Request.Query), fileName);
        }

        [HttpGet("/export/Crm/contacts/excel")]
        [HttpGet("/export/Crm/contacts/excel(fileName='{fileName}')")]
        public FileStreamResult ExportContactsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Contacts, Request.Query), fileName);
        }
        [HttpGet("/export/Crm/opportunities/csv")]
        [HttpGet("/export/Crm/opportunities/csv(fileName='{fileName}')")]
        public FileStreamResult ExportOpportunitiesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Opportunities, Request.Query), fileName);
        }

        [HttpGet("/export/Crm/opportunities/excel")]
        [HttpGet("/export/Crm/opportunities/excel(fileName='{fileName}')")]
        public FileStreamResult ExportOpportunitiesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Opportunities, Request.Query), fileName);
        }
        [HttpGet("/export/Crm/opportunitystatuses/csv")]
        [HttpGet("/export/Crm/opportunitystatuses/csv(fileName='{fileName}')")]
        public FileStreamResult ExportOpportunityStatusesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.OpportunityStatuses, Request.Query), fileName);
        }

        [HttpGet("/export/Crm/opportunitystatuses/excel")]
        [HttpGet("/export/Crm/opportunitystatuses/excel(fileName='{fileName}')")]
        public FileStreamResult ExportOpportunityStatusesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.OpportunityStatuses, Request.Query), fileName);
        }
        [HttpGet("/export/Crm/tasks/csv")]
        [HttpGet("/export/Crm/tasks/csv(fileName='{fileName}')")]
        public FileStreamResult ExportTasksToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Tasks, Request.Query), fileName);
        }

        [HttpGet("/export/Crm/tasks/excel")]
        [HttpGet("/export/Crm/tasks/excel(fileName='{fileName}')")]
        public FileStreamResult ExportTasksToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Tasks, Request.Query), fileName);
        }
        [HttpGet("/export/Crm/taskstatuses/csv")]
        [HttpGet("/export/Crm/taskstatuses/csv(fileName='{fileName}')")]
        public FileStreamResult ExportTaskStatusesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.TaskStatuses, Request.Query), fileName);
        }

        [HttpGet("/export/Crm/taskstatuses/excel")]
        [HttpGet("/export/Crm/taskstatuses/excel(fileName='{fileName}')")]
        public FileStreamResult ExportTaskStatusesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.TaskStatuses, Request.Query), fileName);
        }
        [HttpGet("/export/Crm/tasktypes/csv")]
        [HttpGet("/export/Crm/tasktypes/csv(fileName='{fileName}')")]
        public FileStreamResult ExportTaskTypesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.TaskTypes, Request.Query), fileName);
        }

        [HttpGet("/export/Crm/tasktypes/excel")]
        [HttpGet("/export/Crm/tasktypes/excel(fileName='{fileName}')")]
        public FileStreamResult ExportTaskTypesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.TaskTypes, Request.Query), fileName);
        }
    }
}
