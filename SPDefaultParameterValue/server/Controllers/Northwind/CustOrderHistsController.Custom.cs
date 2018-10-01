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

    public partial class CustOrderHistsController : ODataController
    {
        partial void OnCustOrderHistsDefaultParams(ref string CustomerID)
        {
            CustomerID = "AROUT";
        }

        partial void OnCustOrderHistsInvoke(ref IQueryable<CustOrderHist> items)
        {
            items = items.Where(i => i.Total > 25);
        }
    }
}
