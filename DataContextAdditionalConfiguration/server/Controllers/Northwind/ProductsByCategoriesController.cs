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

  [ODataRoutePrefix("odata/Northwind/ProductsByCategories")]
  [Route("mvc/odata/Northwind/ProductsByCategories")]
  public partial class ProductsByCategoriesController : ODataController
  {
    private Data.NorthwindContext context;

    public ProductsByCategoriesController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/ProductsByCategories
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.ProductsByCategory> GetProductsByCategories()
    {
      var items = this.context.ProductsByCategories.AsQueryable<Models.Northwind.ProductsByCategory>();

      this.OnProductsByCategoriesRead(ref items);

      return items;
    }

    partial void OnProductsByCategoriesRead(ref IQueryable<Models.Northwind.ProductsByCategory> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{CategoryName}")]
    public SingleResult<ProductsByCategory> GetProductsByCategory(string key)
    {
        var items = this.context.ProductsByCategories.Where(i=>i.CategoryName == key);

        return SingleResult.Create(items);
    }
  }
}
