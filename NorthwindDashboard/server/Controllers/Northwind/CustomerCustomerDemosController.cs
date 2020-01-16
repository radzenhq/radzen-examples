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



namespace Northwind.Controllers.Northwind
{
  using Models;
  using Data;
  using Models.Northwind;

  [ODataRoutePrefix("odata/Northwind/CustomerCustomerDemos")]
  [Route("mvc/odata/Northwind/CustomerCustomerDemos")]
  public partial class CustomerCustomerDemosController : ODataController
  {
    private Data.NorthwindContext context;

    public CustomerCustomerDemosController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/CustomerCustomerDemos
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.CustomerCustomerDemo> GetCustomerCustomerDemos()
    {
      var items = this.context.CustomerCustomerDemos.AsQueryable<Models.Northwind.CustomerCustomerDemo>();
      this.OnCustomerCustomerDemosRead(ref items);

      return items;
    }

    partial void OnCustomerCustomerDemosRead(ref IQueryable<Models.Northwind.CustomerCustomerDemo> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{CustomerID},{CustomerTypeID}")]
    public SingleResult<CustomerCustomerDemo> GetCustomerCustomerDemo([FromODataUri] string keyCustomerID,[FromODataUri] string keyCustomerTypeID)
    {
        var items = this.context.CustomerCustomerDemos.Where(i=>i.CustomerID == keyCustomerID && i.CustomerTypeID == keyCustomerTypeID);
        this.OnCustomerCustomerDemosGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnCustomerCustomerDemosGet(ref IQueryable<Models.Northwind.CustomerCustomerDemo> items);

    partial void OnCustomerCustomerDemoDeleted(Models.Northwind.CustomerCustomerDemo item);

    [HttpDelete("{CustomerID},{CustomerTypeID}")]
    public IActionResult DeleteCustomerCustomerDemo([FromODataUri] string keyCustomerID,[FromODataUri] string keyCustomerTypeID)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.CustomerCustomerDemos
                .Where(i => i.CustomerID == keyCustomerID && i.CustomerTypeID == keyCustomerTypeID)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnCustomerCustomerDemoDeleted(item);
            this.context.CustomerCustomerDemos.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnCustomerCustomerDemoUpdated(Models.Northwind.CustomerCustomerDemo item);

    [HttpPut("{CustomerID},{CustomerTypeID}")]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutCustomerCustomerDemo([FromODataUri] string keyCustomerID,[FromODataUri] string keyCustomerTypeID, [FromBody]Models.Northwind.CustomerCustomerDemo newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.CustomerID != keyCustomerID && newItem.CustomerTypeID != keyCustomerTypeID))
            {
                return BadRequest();
            }

            this.OnCustomerCustomerDemoUpdated(newItem);
            this.context.CustomerCustomerDemos.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.CustomerCustomerDemos.Where(i => i.CustomerID == keyCustomerID && i.CustomerTypeID == keyCustomerTypeID);
            Request.QueryString = Request.QueryString.Add("$expand", "Customer,CustomerDemographic");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{CustomerID},{CustomerTypeID}")]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchCustomerCustomerDemo([FromODataUri] string keyCustomerID,[FromODataUri] string keyCustomerTypeID, [FromBody]Delta<Models.Northwind.CustomerCustomerDemo> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.CustomerCustomerDemos.Where(i => i.CustomerID == keyCustomerID && i.CustomerTypeID == keyCustomerTypeID).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnCustomerCustomerDemoUpdated(item);
            this.context.CustomerCustomerDemos.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.CustomerCustomerDemos.Where(i => i.CustomerID == keyCustomerID && i.CustomerTypeID == keyCustomerTypeID);
            Request.QueryString = Request.QueryString.Add("$expand", "Customer,CustomerDemographic");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnCustomerCustomerDemoCreated(Models.Northwind.CustomerCustomerDemo item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Northwind.CustomerCustomerDemo item)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (item == null)
            {
                return BadRequest();
            }

            this.OnCustomerCustomerDemoCreated(item);
            this.context.CustomerCustomerDemos.Add(item);
            this.context.SaveChanges();

            var keyCustomerID = item.CustomerID;
            var keyCustomerTypeID = item.CustomerTypeID;

            var itemToReturn = this.context.CustomerCustomerDemos.Where(i => i.CustomerID == keyCustomerID && i.CustomerTypeID == keyCustomerTypeID);

            Request.QueryString = Request.QueryString.Add("$expand", "Customer,CustomerDemographic");

            return new ObjectResult(SingleResult.Create(itemToReturn))
            {
                StatusCode = 201
            };
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
