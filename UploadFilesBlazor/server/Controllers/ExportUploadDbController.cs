using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UploadFilesBlazor.Data;

namespace UploadFilesBlazor
{
    public partial class ExportUploadDbController : ExportController
    {
        private readonly UploadDbContext context;

        public ExportUploadDbController(UploadDbContext context)
        {
            this.context = context;
        }
        [HttpGet("/export/UploadDb/files/csv")]
        [HttpGet("/export/UploadDb/files/csv(fileName='{fileName}')")]
        public FileStreamResult ExportFilesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Files, Request.Query), fileName);
        }

        [HttpGet("/export/UploadDb/files/excel")]
        [HttpGet("/export/UploadDb/files/excel(fileName='{fileName}')")]
        public FileStreamResult ExportFilesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Files, Request.Query), fileName);
        }
    }
}
