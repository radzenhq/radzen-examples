using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomSecurity.Data;

namespace CustomSecurity
{
    public partial class ExportCustomSecurityDbController : ExportController
    {
        private readonly CustomSecurityDbContext context;

        public ExportCustomSecurityDbController(CustomSecurityDbContext context)
        {
            this.context = context;
        }
        [HttpGet("/export/CustomSecurityDb/roles/csv")]
        [HttpGet("/export/CustomSecurityDb/roles/csv(fileName='{fileName}')")]
        public FileStreamResult ExportRolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Roles, Request.Query), fileName);
        }

        [HttpGet("/export/CustomSecurityDb/roles/excel")]
        [HttpGet("/export/CustomSecurityDb/roles/excel(fileName='{fileName}')")]
        public FileStreamResult ExportRolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Roles, Request.Query), fileName);
        }
        [HttpGet("/export/CustomSecurityDb/users/csv")]
        [HttpGet("/export/CustomSecurityDb/users/csv(fileName='{fileName}')")]
        public FileStreamResult ExportUsersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Users, Request.Query), fileName);
        }

        [HttpGet("/export/CustomSecurityDb/users/excel")]
        [HttpGet("/export/CustomSecurityDb/users/excel(fileName='{fileName}')")]
        public FileStreamResult ExportUsersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Users, Request.Query), fileName);
        }
        [HttpGet("/export/CustomSecurityDb/userroles/csv")]
        [HttpGet("/export/CustomSecurityDb/userroles/csv(fileName='{fileName}')")]
        public FileStreamResult ExportUserRolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.UserRoles, Request.Query), fileName);
        }

        [HttpGet("/export/CustomSecurityDb/userroles/excel")]
        [HttpGet("/export/CustomSecurityDb/userroles/excel(fileName='{fileName}')")]
        public FileStreamResult ExportUserRolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.UserRoles, Request.Query), fileName);
        }
    }
}
