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

  [ODataRoutePrefix("odata/Northwind/Invoices")]
  [Route("mvc/odata/Northwind/Invoices")]
  public partial class InvoicesController : ODataController
  {
    private Data.NorthwindContext context;

    public InvoicesController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/Invoices
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.Invoice> GetInvoices()
    {
      var items = this.context.Invoices.AsQueryable<Models.Northwind.Invoice>();

      this.OnInvoicesRead(ref items);

      return items;
    }

    partial void OnInvoicesRead(ref IQueryable<Models.Northwind.Invoice> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{CustomerName}")]
    public SingleResult<Invoice> GetInvoice(string key)
    {
        var items = this.context.Invoices.Where(i=>i.CustomerName == key);

        return SingleResult.Create(items);
    }
  }
}
