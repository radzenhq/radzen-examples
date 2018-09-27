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



namespace HierarchyWithTwoDataGrid.Controllers.Northwind
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
    [EnableQuery(MaxExpansionDepth=10)]
    [HttpGet]
    public IEnumerable<Models.Northwind.CustomerCustomerDemo> GetCustomerCustomerDemos()
    {
      var items = this.context.CustomerCustomerDemos.AsQueryable<Models.Northwind.CustomerCustomerDemo>();

      this.OnCustomerCustomerDemosRead(ref items);

      return items;
    }

    partial void OnCustomerCustomerDemosRead(ref IQueryable<Models.Northwind.CustomerCustomerDemo> items);

    [EnableQuery(MaxExpansionDepth=10)]
    [HttpGet("{CustomerID},{CustomerTypeID}")]
    public SingleResult<CustomerCustomerDemo> GetCustomerCustomerDemo([FromODataUri] string keyCustomerID,[FromODataUri] string keyCustomerTypeID)
    {
        var items = this.context.CustomerCustomerDemos.Where(i=>i.CustomerID == keyCustomerID && i.CustomerTypeID == keyCustomerTypeID);

        return SingleResult.Create(items);
    }
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

            var itemToReturn = this.context.CustomerCustomerDemos
                .Where(i => i.CustomerID == keyCustomerID && i.CustomerTypeID == keyCustomerTypeID)
                .Include(i => i.Customer)
                .Include(i => i.CustomerDemographic)
                .FirstOrDefault();

            return new JsonResult(itemToReturn, new Newtonsoft.Json.JsonSerializerSettings
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc
            })
            {
                StatusCode = 200
            };
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{CustomerID},{CustomerTypeID}")]
    public IActionResult PatchCustomerCustomerDemo([FromODataUri] string keyCustomerID,[FromODataUri] string keyCustomerTypeID, [FromBody]Delta<Models.Northwind.CustomerCustomerDemo> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.CustomerCustomerDemos.Where(i=>i.CustomerID == keyCustomerID && i.CustomerTypeID == keyCustomerTypeID).FirstOrDefault();
            
            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnCustomerCustomerDemoUpdated(item);
            this.context.CustomerCustomerDemos.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.CustomerCustomerDemos
                .Where(i => i.CustomerID == keyCustomerID && i.CustomerTypeID == keyCustomerTypeID)
                .Include(i => i.Customer)
                .Include(i => i.CustomerDemographic)
                .FirstOrDefault();

            return new JsonResult(itemToReturn, new Newtonsoft.Json.JsonSerializerSettings
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc
            })
            {
                StatusCode = 200
            };
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnCustomerCustomerDemoCreated(Models.Northwind.CustomerCustomerDemo item);

    [HttpPost]
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
            var itemToReturn = this.context.CustomerCustomerDemos
                .Where(i => i.CustomerID == keyCustomerID && i.CustomerTypeID == keyCustomerTypeID)
                .Include(i => i.Customer)
                .Include(i => i.CustomerDemographic)
                .FirstOrDefault();

            return new JsonResult(itemToReturn, new Newtonsoft.Json.JsonSerializerSettings
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc
            })
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
