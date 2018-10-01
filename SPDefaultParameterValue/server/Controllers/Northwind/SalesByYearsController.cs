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



namespace SpDefaultParameterValue.Controllers.Northwind
{
  using Models;
  using Data;
  using Models.Northwind;

  public partial class SalesByYearsController : ODataController
  {
    private Data.NorthwindContext context;

    public SalesByYearsController(Data.NorthwindContext context)
    {
      this.context = context;
    }

    [HttpGet]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [ODataRoute("SalesByYearsFunc(Beginning_Date={Beginning_Date},Ending_Date={Ending_Date})")]
    public IActionResult SalesByYearsFunc([FromODataUri] string Beginning_Date, [FromODataUri] string Ending_Date)
    {
        this.OnSalesByYearsDefaultParams(ref Beginning_Date, ref Ending_Date);

        var items = this.context.SalesByYears.AsNoTracking().FromSql("EXEC [dbo].[Sales by Year] {0}, {1}", DateTime.Parse(Beginning_Date, null, System.Globalization.DateTimeStyles.RoundtripKind), DateTime.Parse(Ending_Date, null, System.Globalization.DateTimeStyles.RoundtripKind));

        this.OnSalesByYearsInvoke(ref items);

        return Ok(items);
    }

    partial void OnSalesByYearsDefaultParams(ref string Beginning_Date, ref string Ending_Date);

    partial void OnSalesByYearsInvoke(ref IQueryable<Models.Northwind.SalesByYear> items);
  }
}
