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

  [ODataRoutePrefix("odata/Northwind/SalesTotalsByAmounts")]
  [Route("mvc/odata/Northwind/SalesTotalsByAmounts")]
  public partial class SalesTotalsByAmountsController : ODataController
  {
    private Data.NorthwindContext context;

    public SalesTotalsByAmountsController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/SalesTotalsByAmounts
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.SalesTotalsByAmount> GetSalesTotalsByAmounts()
    {
      var items = this.context.SalesTotalsByAmounts.AsQueryable<Models.Northwind.SalesTotalsByAmount>();

      this.OnSalesTotalsByAmountsRead(ref items);

      return items;
    }

    partial void OnSalesTotalsByAmountsRead(ref IQueryable<Models.Northwind.SalesTotalsByAmount> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{OrderID}")]
    public SingleResult<SalesTotalsByAmount> GetSalesTotalsByAmount(int key)
    {
        var items = this.context.SalesTotalsByAmounts.Where(i=>i.OrderID == key);

        return SingleResult.Create(items);
    }
  }
}
