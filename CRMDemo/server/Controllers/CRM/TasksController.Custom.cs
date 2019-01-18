using System;
using System.Net;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNet.OData.Query;

namespace Crm.Controllers.Crm
{
    using Models;
    using Data;
    using Models.Crm;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;

    [Authorize(AuthenticationSchemes = "Bearer")]
    public partial class TasksController
    {
        partial void OnTasksRead(ref IQueryable<Task> items)
        {
            var role = User.FindFirst(ClaimTypes.Role).Value;

            if (role != "Sales Manager")
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                items = items.Include(item => item.Opportunity.User)
                             .Where(item => item.Opportunity.User.Id == userId);
            }
        }
    }
}
