using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using CRMBlazorWasmRBS.Server.Data;

namespace CRMBlazorWasmRBS.Server.Controllers
{
    public partial class ExportRadzenCRMController : ExportController
    {
        private readonly RadzenCRMContext context;
        private readonly RadzenCRMService service;

        public ExportRadzenCRMController(RadzenCRMContext context, RadzenCRMService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/RadzenCRM/contacts/csv")]
        [HttpGet("/export/RadzenCRM/contacts/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContactsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetContacts(), Request.Query), fileName);
        }

        [HttpGet("/export/RadzenCRM/contacts/excel")]
        [HttpGet("/export/RadzenCRM/contacts/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContactsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetContacts(), Request.Query), fileName);
        }

        [HttpGet("/export/RadzenCRM/opportunities/csv")]
        [HttpGet("/export/RadzenCRM/opportunities/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportOpportunitiesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetOpportunities(), Request.Query), fileName);
        }

        [HttpGet("/export/RadzenCRM/opportunities/excel")]
        [HttpGet("/export/RadzenCRM/opportunities/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportOpportunitiesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetOpportunities(), Request.Query), fileName);
        }

        [HttpGet("/export/RadzenCRM/opportunitystatuses/csv")]
        [HttpGet("/export/RadzenCRM/opportunitystatuses/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportOpportunityStatusesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetOpportunityStatuses(), Request.Query), fileName);
        }

        [HttpGet("/export/RadzenCRM/opportunitystatuses/excel")]
        [HttpGet("/export/RadzenCRM/opportunitystatuses/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportOpportunityStatusesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetOpportunityStatuses(), Request.Query), fileName);
        }

        [HttpGet("/export/RadzenCRM/tasks/csv")]
        [HttpGet("/export/RadzenCRM/tasks/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTasksToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTasks(), Request.Query), fileName);
        }

        [HttpGet("/export/RadzenCRM/tasks/excel")]
        [HttpGet("/export/RadzenCRM/tasks/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTasksToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTasks(), Request.Query), fileName);
        }

        [HttpGet("/export/RadzenCRM/taskstatuses/csv")]
        [HttpGet("/export/RadzenCRM/taskstatuses/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTaskStatusesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTaskStatuses(), Request.Query), fileName);
        }

        [HttpGet("/export/RadzenCRM/taskstatuses/excel")]
        [HttpGet("/export/RadzenCRM/taskstatuses/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTaskStatusesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTaskStatuses(), Request.Query), fileName);
        }

        [HttpGet("/export/RadzenCRM/tasktypes/csv")]
        [HttpGet("/export/RadzenCRM/tasktypes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTaskTypesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTaskTypes(), Request.Query), fileName);
        }

        [HttpGet("/export/RadzenCRM/tasktypes/excel")]
        [HttpGet("/export/RadzenCRM/tasktypes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTaskTypesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTaskTypes(), Request.Query), fileName);
        }
    }
}
