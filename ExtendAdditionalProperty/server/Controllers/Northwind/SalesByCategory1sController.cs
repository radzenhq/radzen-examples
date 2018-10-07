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

  public partial class SalesByCategory1sController : ODataController
  {
    private Data.NorthwindContext context;

    public SalesByCategory1sController(Data.NorthwindContext context)
    {
      this.context = context;
    }

    [HttpGet]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [ODataRoute("SalesByCategory1sFunc(CategoryName={CategoryName},OrdYear={OrdYear})")]
    public IActionResult SalesByCategory1sFunc([FromODataUri] string CategoryName, [FromODataUri] string OrdYear)
    {
        this.OnSalesByCategory1sDefaultParams(ref CategoryName, ref OrdYear);

        var items = this.context.SalesByCategory1s.AsNoTracking().FromSql("EXEC [dbo].[SalesByCategory] {0}, {1}", CategoryName, OrdYear);

        this.OnSalesByCategory1sInvoke(ref items);

        return Ok(items);
    }

    partial void OnSalesByCategory1sDefaultParams(ref string CategoryName, ref string OrdYear);

    partial void OnSalesByCategory1sInvoke(ref IQueryable<Models.Northwind.SalesByCategory1> items);
  }
}
