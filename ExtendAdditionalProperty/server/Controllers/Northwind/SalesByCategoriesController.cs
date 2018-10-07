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

  [ODataRoutePrefix("odata/Northwind/SalesByCategories")]
  [Route("mvc/odata/Northwind/SalesByCategories")]
  public partial class SalesByCategoriesController : ODataController
  {
    private Data.NorthwindContext context;

    public SalesByCategoriesController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/SalesByCategories
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.SalesByCategory> GetSalesByCategories()
    {
      var items = this.context.SalesByCategories.AsQueryable<Models.Northwind.SalesByCategory>();

      this.OnSalesByCategoriesRead(ref items);

      return items;
    }

    partial void OnSalesByCategoriesRead(ref IQueryable<Models.Northwind.SalesByCategory> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{CategoryID}")]
    public SingleResult<SalesByCategory> GetSalesByCategory(int key)
    {
        var items = this.context.SalesByCategories.Where(i=>i.CategoryID == key);

        return SingleResult.Create(items);
    }
  }
}
