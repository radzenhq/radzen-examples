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

namespace GetCurrentUser.Controllers.Northwind
{
    using Models;
    using Data;
    using Models.Northwind;
    using Microsoft.AspNetCore.Authorization;
    using System.Security.Claims;

    [Authorize(AuthenticationSchemes="Bearer")]
    public partial class OrdersController
    {
        partial void OnOrdersRead(ref IQueryable<Models.Northwind.Order> items)
        {
            var userName = this.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
        }

        partial void OnOrderDeleted(Models.Northwind.Order item)
        {

        }

        partial void OnOrderUpdated(Models.Northwind.Order item)
        {

        }

        partial void OnOrderCreated(Models.Northwind.Order item)
        {

        }
    }
}
