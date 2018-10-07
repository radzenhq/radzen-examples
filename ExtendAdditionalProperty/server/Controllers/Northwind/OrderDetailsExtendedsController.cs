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

  [ODataRoutePrefix("odata/Northwind/OrderDetailsExtendeds")]
  [Route("mvc/odata/Northwind/OrderDetailsExtendeds")]
  public partial class OrderDetailsExtendedsController : ODataController
  {
    private Data.NorthwindContext context;

    public OrderDetailsExtendedsController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/OrderDetailsExtendeds
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.OrderDetailsExtended> GetOrderDetailsExtendeds()
    {
      var items = this.context.OrderDetailsExtendeds.AsQueryable<Models.Northwind.OrderDetailsExtended>();

      this.OnOrderDetailsExtendedsRead(ref items);

      return items;
    }

    partial void OnOrderDetailsExtendedsRead(ref IQueryable<Models.Northwind.OrderDetailsExtended> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{OrderID}")]
    public SingleResult<OrderDetailsExtended> GetOrderDetailsExtended(int key)
    {
        var items = this.context.OrderDetailsExtendeds.Where(i=>i.OrderID == key);

        return SingleResult.Create(items);
    }
  }
}
