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

  public partial class CustOrderHistsController : ODataController
  {
    private Data.NorthwindContext context;

    public CustOrderHistsController(Data.NorthwindContext context)
    {
      this.context = context;
    }

    [HttpGet]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [ODataRoute("CustOrderHistsFunc(CustomerID={CustomerID})")]
    public IActionResult CustOrderHistsFunc([FromODataUri] string CustomerID)
    {
        this.OnCustOrderHistsDefaultParams(ref CustomerID);

        var items = this.context.CustOrderHists.AsNoTracking().FromSql("EXEC [dbo].[CustOrderHist] {0}", CustomerID);

        this.OnCustOrderHistsInvoke(ref items);

        return Ok(items);
    }

    partial void OnCustOrderHistsDefaultParams(ref string CustomerID);

    partial void OnCustOrderHistsInvoke(ref IQueryable<Models.Northwind.CustOrderHist> items);
  }
}
