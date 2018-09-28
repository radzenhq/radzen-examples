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



namespace DefaultColumnFilter.Controllers.Northwind
{
  using Models;
  using Data;
  using Models.Northwind;

  [ODataRoutePrefix("odata/Northwind/Shippers")]
  [Route("mvc/odata/Northwind/Shippers")]
  public partial class ShippersController : ODataController
  {
    private Data.NorthwindContext context;

    public ShippersController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/Shippers
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.Shipper> GetShippers()
    {
      var items = this.context.Shippers.AsQueryable<Models.Northwind.Shipper>();

      this.OnShippersRead(ref items);

      return items;
    }

    partial void OnShippersRead(ref IQueryable<Models.Northwind.Shipper> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{ShipperID}")]
    public SingleResult<Shipper> GetShipper(int key)
    {
        var items = this.context.Shippers.Where(i=>i.ShipperID == key);

        return SingleResult.Create(items);
    }
    partial void OnShipperDeleted(Models.Northwind.Shipper item);

    [HttpDelete("{ShipperID}")]
    public IActionResult DeleteShipper(int key) 
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Shippers
                .Where(i => i.ShipperID == key)
                .Include(i => i.Orders)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }                

            this.OnShipperDeleted(item);
            this.context.Shippers.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnShipperUpdated(Models.Northwind.Shipper item);

    [HttpPut("{ShipperID}")]
    public IActionResult PutShipper(int key, [FromBody]Models.Northwind.Shipper newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.ShipperID != key))
            {
                return BadRequest();
            }

            this.OnShipperUpdated(newItem);
            this.context.Shippers.Update(newItem);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{ShipperID}")]
    public IActionResult PatchShipper(int key, [FromBody]Delta<Models.Northwind.Shipper> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Shippers.Where(i=>i.ShipperID == key).FirstOrDefault();
            
            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnShipperUpdated(item);
            this.context.Shippers.Update(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnShipperCreated(Models.Northwind.Shipper item);

    [HttpPost]
    public IActionResult Post([FromBody] Models.Northwind.Shipper item)
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

            this.OnShipperCreated(item);
            this.context.Shippers.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Northwind/Shippers/{item.ShipperID}", item);
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
