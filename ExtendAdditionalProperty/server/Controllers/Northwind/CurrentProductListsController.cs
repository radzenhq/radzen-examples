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

  [ODataRoutePrefix("odata/Northwind/CurrentProductLists")]
  [Route("mvc/odata/Northwind/CurrentProductLists")]
  public partial class CurrentProductListsController : ODataController
  {
    private Data.NorthwindContext context;

    public CurrentProductListsController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/CurrentProductLists
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.CurrentProductList> GetCurrentProductLists()
    {
      var items = this.context.CurrentProductLists.AsQueryable<Models.Northwind.CurrentProductList>();

      this.OnCurrentProductListsRead(ref items);

      return items;
    }

    partial void OnCurrentProductListsRead(ref IQueryable<Models.Northwind.CurrentProductList> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{ProductID}")]
    public SingleResult<CurrentProductList> GetCurrentProductList(int key)
    {
        var items = this.context.CurrentProductLists.Where(i=>i.ProductID == key);

        return SingleResult.Create(items);
    }
  }
}
