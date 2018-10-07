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

  public partial class CustOrdersDetailsController : ODataController
  {
    private Data.NorthwindContext context;

    public CustOrdersDetailsController(Data.NorthwindContext context)
    {
      this.context = context;
    }

    [HttpGet]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [ODataRoute("CustOrdersDetailsFunc(OrderID={OrderID})")]
    public IActionResult CustOrdersDetailsFunc([FromODataUri] int? OrderID)
    {
        this.OnCustOrdersDetailsDefaultParams(ref OrderID);

        var items = this.context.CustOrdersDetails.AsNoTracking().FromSql("EXEC [dbo].[CustOrdersDetail] {0}", OrderID);

        this.OnCustOrdersDetailsInvoke(ref items);

        return Ok(items);
    }

    partial void OnCustOrdersDetailsDefaultParams(ref int? OrderID);

    partial void OnCustOrdersDetailsInvoke(ref IQueryable<Models.Northwind.CustOrdersDetail> items);
  }
}
