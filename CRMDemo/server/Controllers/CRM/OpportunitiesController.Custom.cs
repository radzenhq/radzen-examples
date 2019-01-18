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
using Microsoft.AspNetCore.Authorization;

namespace Crm.Controllers.Crm
{
    using Models;
    using Data;
    using Models.Crm;
    using System.Security.Claims;

    [Authorize(AuthenticationSchemes = "Bearer")]
    public partial class OpportunitiesController
    {
        partial void OnOpportunityCreated(Opportunity item)
        {
            item.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        partial void OnOpportunitiesRead(ref IQueryable<Opportunity> items)
        {
            var role = User.FindFirst(ClaimTypes.Role).Value;

            if (role != "Sales Manager")
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                items = items.Where(item => item.UserId == userId);
            }
        }
    }
}