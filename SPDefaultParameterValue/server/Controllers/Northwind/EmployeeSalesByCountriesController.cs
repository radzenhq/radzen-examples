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

  public partial class EmployeeSalesByCountriesController : ODataController
  {
    private Data.NorthwindContext context;

    public EmployeeSalesByCountriesController(Data.NorthwindContext context)
    {
      this.context = context;
    }

    [HttpGet]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [ODataRoute("EmployeeSalesByCountriesFunc(Beginning_Date={Beginning_Date},Ending_Date={Ending_Date})")]
    public IActionResult EmployeeSalesByCountriesFunc([FromODataUri] string Beginning_Date, [FromODataUri] string Ending_Date)
    {
        this.OnEmployeeSalesByCountriesDefaultParams(ref Beginning_Date, ref Ending_Date);

        var items = this.context.EmployeeSalesByCountries.AsNoTracking().FromSql("EXEC [dbo].[Employee Sales by Country] {0}, {1}", DateTime.Parse(Beginning_Date, null, System.Globalization.DateTimeStyles.RoundtripKind), DateTime.Parse(Ending_Date, null, System.Globalization.DateTimeStyles.RoundtripKind));

        this.OnEmployeeSalesByCountriesInvoke(ref items);

        return Ok(items);
    }

    partial void OnEmployeeSalesByCountriesDefaultParams(ref string Beginning_Date, ref string Ending_Date);

    partial void OnEmployeeSalesByCountriesInvoke(ref IQueryable<Models.Northwind.EmployeeSalesByCountry> items);
  }
}
