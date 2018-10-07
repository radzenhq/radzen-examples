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

  [ODataRoutePrefix("odata/Northwind/CustomerAndSuppliersByCities")]
  [Route("mvc/odata/Northwind/CustomerAndSuppliersByCities")]
  public partial class CustomerAndSuppliersByCitiesController : ODataController
  {
    private Data.NorthwindContext context;

    public CustomerAndSuppliersByCitiesController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/CustomerAndSuppliersByCities
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.CustomerAndSuppliersByCity> GetCustomerAndSuppliersByCities()
    {
      var items = this.context.CustomerAndSuppliersByCities.AsQueryable<Models.Northwind.CustomerAndSuppliersByCity>();

      this.OnCustomerAndSuppliersByCitiesRead(ref items);

      return items;
    }

    partial void OnCustomerAndSuppliersByCitiesRead(ref IQueryable<Models.Northwind.CustomerAndSuppliersByCity> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{CompanyName}")]
    public SingleResult<CustomerAndSuppliersByCity> GetCustomerAndSuppliersByCity(string key)
    {
        var items = this.context.CustomerAndSuppliersByCities.Where(i=>i.CompanyName == key);

        return SingleResult.Create(items);
    }
  }
}
