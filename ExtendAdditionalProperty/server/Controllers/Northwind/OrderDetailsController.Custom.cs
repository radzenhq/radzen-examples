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

namespace ExtendAdditionalProperty.Controllers.Northwind
{
    using Models;
    using Data;
    using Models.Northwind;

    public partial class OrderDetailsController
    {
        partial void OnOrderDetailsRead(ref IQueryable<Models.Northwind.OrderDetail> items)
        {
            // Populate additional property
            foreach (var item in items)
            {
                var orderDetailExtended = this.context.OrderDetailsExtendeds
                  .Where(ode => ode.OrderID == item.OrderID && ode.ProductID == item.ProductID)
                  .FirstOrDefault();

                if (orderDetailExtended != null)
                {
                    item.ExtendedPrice = orderDetailExtended.ExtendedPrice;
                }
            }
        }
    }
}
