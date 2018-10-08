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

  [ODataRoutePrefix("odata/Northwind/CategorySalesFor1997s")]
  [Route("mvc/odata/Northwind/CategorySalesFor1997s")]
  public partial class CategorySalesFor1997sController : ODataController
  {
    private Data.NorthwindContext context;

    public CategorySalesFor1997sController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/CategorySalesFor1997s
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.CategorySalesFor1997> GetCategorySalesFor1997s()
    {
      var items = this.context.CategorySalesFor1997s.AsQueryable<Models.Northwind.CategorySalesFor1997>();

      this.OnCategorySalesFor1997sRead(ref items);

      return items;
    }

    partial void OnCategorySalesFor1997sRead(ref IQueryable<Models.Northwind.CategorySalesFor1997> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{CategoryName}")]
    public SingleResult<CategorySalesFor1997> GetCategorySalesFor1997(string key)
    {
        var items = this.context.CategorySalesFor1997s.Where(i=>i.CategoryName == key);

        return SingleResult.Create(items);
    }
  }
}
