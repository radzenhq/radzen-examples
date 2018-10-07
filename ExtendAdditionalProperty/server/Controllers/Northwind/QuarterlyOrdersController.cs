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

  [ODataRoutePrefix("odata/Northwind/QuarterlyOrders")]
  [Route("mvc/odata/Northwind/QuarterlyOrders")]
  public partial class QuarterlyOrdersController : ODataController
  {
    private Data.NorthwindContext context;

    public QuarterlyOrdersController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/QuarterlyOrders
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.QuarterlyOrder> GetQuarterlyOrders()
    {
      var items = this.context.QuarterlyOrders.AsQueryable<Models.Northwind.QuarterlyOrder>();

      this.OnQuarterlyOrdersRead(ref items);

      return items;
    }

    partial void OnQuarterlyOrdersRead(ref IQueryable<Models.Northwind.QuarterlyOrder> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{CustomerID}")]
    public SingleResult<QuarterlyOrder> GetQuarterlyOrder(string key)
    {
        var items = this.context.QuarterlyOrders.Where(i=>i.CustomerID == key);

        return SingleResult.Create(items);
    }
  }
}
