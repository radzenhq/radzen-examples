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

  [ODataRoutePrefix("odata/Northwind/OrdersQries")]
  [Route("mvc/odata/Northwind/OrdersQries")]
  public partial class OrdersQriesController : ODataController
  {
    private Data.NorthwindContext context;

    public OrdersQriesController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/OrdersQries
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.OrdersQry> GetOrdersQries()
    {
      var items = this.context.OrdersQries.AsQueryable<Models.Northwind.OrdersQry>();

      this.OnOrdersQriesRead(ref items);

      return items;
    }

    partial void OnOrdersQriesRead(ref IQueryable<Models.Northwind.OrdersQry> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{OrderID}")]
    public SingleResult<OrdersQry> GetOrdersQry(int key)
    {
        var items = this.context.OrdersQries.Where(i=>i.OrderID == key);

        return SingleResult.Create(items);
    }
  }
}
