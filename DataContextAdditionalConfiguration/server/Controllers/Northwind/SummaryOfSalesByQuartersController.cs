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

  [ODataRoutePrefix("odata/Northwind/SummaryOfSalesByQuarters")]
  [Route("mvc/odata/Northwind/SummaryOfSalesByQuarters")]
  public partial class SummaryOfSalesByQuartersController : ODataController
  {
    private Data.NorthwindContext context;

    public SummaryOfSalesByQuartersController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/SummaryOfSalesByQuarters
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.SummaryOfSalesByQuarter> GetSummaryOfSalesByQuarters()
    {
      var items = this.context.SummaryOfSalesByQuarters.AsQueryable<Models.Northwind.SummaryOfSalesByQuarter>();

      this.OnSummaryOfSalesByQuartersRead(ref items);

      return items;
    }

    partial void OnSummaryOfSalesByQuartersRead(ref IQueryable<Models.Northwind.SummaryOfSalesByQuarter> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{OrderID}")]
    public SingleResult<SummaryOfSalesByQuarter> GetSummaryOfSalesByQuarter(int key)
    {
        var items = this.context.SummaryOfSalesByQuarters.Where(i=>i.OrderID == key);

        return SingleResult.Create(items);
    }
  }
}
