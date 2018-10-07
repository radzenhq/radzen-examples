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

  [ODataRoutePrefix("odata/Northwind/ProductSalesFor1997s")]
  [Route("mvc/odata/Northwind/ProductSalesFor1997s")]
  public partial class ProductSalesFor1997sController : ODataController
  {
    private Data.NorthwindContext context;

    public ProductSalesFor1997sController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/ProductSalesFor1997s
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.ProductSalesFor1997> GetProductSalesFor1997s()
    {
      var items = this.context.ProductSalesFor1997s.AsQueryable<Models.Northwind.ProductSalesFor1997>();

      this.OnProductSalesFor1997sRead(ref items);

      return items;
    }

    partial void OnProductSalesFor1997sRead(ref IQueryable<Models.Northwind.ProductSalesFor1997> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{CategoryName}")]
    public SingleResult<ProductSalesFor1997> GetProductSalesFor1997(string key)
    {
        var items = this.context.ProductSalesFor1997s.Where(i=>i.CategoryName == key);

        return SingleResult.Create(items);
    }
  }
}
