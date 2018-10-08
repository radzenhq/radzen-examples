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

  [ODataRoutePrefix("odata/Northwind/ProductsAboveAveragePrices")]
  [Route("mvc/odata/Northwind/ProductsAboveAveragePrices")]
  public partial class ProductsAboveAveragePricesController : ODataController
  {
    private Data.NorthwindContext context;

    public ProductsAboveAveragePricesController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/ProductsAboveAveragePrices
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.ProductsAboveAveragePrice> GetProductsAboveAveragePrices()
    {
      var items = this.context.ProductsAboveAveragePrices.AsQueryable<Models.Northwind.ProductsAboveAveragePrice>();

      this.OnProductsAboveAveragePricesRead(ref items);

      return items;
    }

    partial void OnProductsAboveAveragePricesRead(ref IQueryable<Models.Northwind.ProductsAboveAveragePrice> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{ProductName}")]
    public SingleResult<ProductsAboveAveragePrice> GetProductsAboveAveragePrice(string key)
    {
        var items = this.context.ProductsAboveAveragePrices.Where(i=>i.ProductName == key);

        return SingleResult.Create(items);
    }
  }
}
