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

  [ODataRoutePrefix("odata/Northwind/AlphabeticalListOfProducts")]
  [Route("mvc/odata/Northwind/AlphabeticalListOfProducts")]
  public partial class AlphabeticalListOfProductsController : ODataController
  {
    private Data.NorthwindContext context;

    public AlphabeticalListOfProductsController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/AlphabeticalListOfProducts
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.AlphabeticalListOfProduct> GetAlphabeticalListOfProducts()
    {
      var items = this.context.AlphabeticalListOfProducts.AsQueryable<Models.Northwind.AlphabeticalListOfProduct>();

      this.OnAlphabeticalListOfProductsRead(ref items);

      return items;
    }

    partial void OnAlphabeticalListOfProductsRead(ref IQueryable<Models.Northwind.AlphabeticalListOfProduct> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{ProductID}")]
    public SingleResult<AlphabeticalListOfProduct> GetAlphabeticalListOfProduct(int key)
    {
        var items = this.context.AlphabeticalListOfProducts.Where(i=>i.ProductID == key);

        return SingleResult.Create(items);
    }
  }
}
