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



namespace DataContextAdditionalConfiguration.Controllers.Northwind
{
  using Models;
  using Data;
  using Models.Northwind;

  [ODataRoutePrefix("odata/Northwind/OrderSubtotals")]
  [Route("mvc/odata/Northwind/OrderSubtotals")]
  public partial class OrderSubtotalsController : ODataController
  {
    private Data.NorthwindContext context;

    public OrderSubtotalsController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/OrderSubtotals
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.OrderSubtotal> GetOrderSubtotals()
    {
      var items = this.context.OrderSubtotals.AsQueryable<Models.Northwind.OrderSubtotal>();

      this.OnOrderSubtotalsRead(ref items);

      return items;
    }

    partial void OnOrderSubtotalsRead(ref IQueryable<Models.Northwind.OrderSubtotal> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{OrderID}")]
    public SingleResult<OrderSubtotal> GetOrderSubtotal(int key)
    {
        var items = this.context.OrderSubtotals.Where(i=>i.OrderID == key);

        return SingleResult.Create(items);
    }
  }
}
