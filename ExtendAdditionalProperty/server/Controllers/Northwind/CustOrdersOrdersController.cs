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

  public partial class CustOrdersOrdersController : ODataController
  {
    private Data.NorthwindContext context;

    public CustOrdersOrdersController(Data.NorthwindContext context)
    {
      this.context = context;
    }

    [HttpGet]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [ODataRoute("CustOrdersOrdersFunc(CustomerID={CustomerID})")]
    public IActionResult CustOrdersOrdersFunc([FromODataUri] string CustomerID)
    {
        this.OnCustOrdersOrdersDefaultParams(ref CustomerID);

        var items = this.context.CustOrdersOrders.AsNoTracking().FromSql("EXEC [dbo].[CustOrdersOrders] {0}", CustomerID);

        this.OnCustOrdersOrdersInvoke(ref items);

        return Ok(items);
    }

    partial void OnCustOrdersOrdersDefaultParams(ref string CustomerID);

    partial void OnCustOrdersOrdersInvoke(ref IQueryable<Models.Northwind.CustOrdersOrder> items);
  }
}
