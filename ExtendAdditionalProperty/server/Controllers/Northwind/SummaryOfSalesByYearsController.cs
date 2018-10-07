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

  [ODataRoutePrefix("odata/Northwind/SummaryOfSalesByYears")]
  [Route("mvc/odata/Northwind/SummaryOfSalesByYears")]
  public partial class SummaryOfSalesByYearsController : ODataController
  {
    private Data.NorthwindContext context;

    public SummaryOfSalesByYearsController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/SummaryOfSalesByYears
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.SummaryOfSalesByYear> GetSummaryOfSalesByYears()
    {
      var items = this.context.SummaryOfSalesByYears.AsQueryable<Models.Northwind.SummaryOfSalesByYear>();

      this.OnSummaryOfSalesByYearsRead(ref items);

      return items;
    }

    partial void OnSummaryOfSalesByYearsRead(ref IQueryable<Models.Northwind.SummaryOfSalesByYear> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{OrderID}")]
    public SingleResult<SummaryOfSalesByYear> GetSummaryOfSalesByYear(int key)
    {
        var items = this.context.SummaryOfSalesByYears.Where(i=>i.OrderID == key);

        return SingleResult.Create(items);
    }
  }
}
