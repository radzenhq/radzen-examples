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



namespace SpDefaultParameterValue.Controllers.Northwind
{
  using Models;
  using Data;
  using Models.Northwind;

  public partial class TenMostExpensiveProductsController : ODataController
  {
    private Data.NorthwindContext context;

    public TenMostExpensiveProductsController(Data.NorthwindContext context)
    {
      this.context = context;
    }

    [HttpGet]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [ODataRoute("TenMostExpensiveProductsFunc()")]
    public IActionResult TenMostExpensiveProducts()
    {
        var items = this.context.TenMostExpensiveProducts.AsNoTracking().FromSql("EXEC [dbo].[Ten Most Expensive Products]");

        this.OnTenMostExpensiveProductsInvoke(ref items);

        return Ok(items);
    }

    partial void OnTenMostExpensiveProductsInvoke(ref IQueryable<Models.Northwind.TenMostExpensiveProduct> items);
  }
}
