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

  [ODataRoutePrefix("odata/Northwind/Suppliers")]
  [Route("mvc/odata/Northwind/Suppliers")]
  public partial class SuppliersController : ODataController
  {
    private Data.NorthwindContext context;

    public SuppliersController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/Suppliers
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.Supplier> GetSuppliers()
    {
      var items = this.context.Suppliers.AsQueryable<Models.Northwind.Supplier>();

      this.OnSuppliersRead(ref items);

      return items;
    }

    partial void OnSuppliersRead(ref IQueryable<Models.Northwind.Supplier> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{SupplierID}")]
    public SingleResult<Supplier> GetSupplier(int key)
    {
        var items = this.context.Suppliers.Where(i=>i.SupplierID == key);

        return SingleResult.Create(items);
    }
    partial void OnSupplierDeleted(Models.Northwind.Supplier item);

    [HttpDelete("{SupplierID}")]
    public IActionResult DeleteSupplier(int key) 
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Suppliers
                .Where(i => i.SupplierID == key)
                .Include(i => i.Products)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }                

            this.OnSupplierDeleted(item);
            this.context.Suppliers.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSupplierUpdated(Models.Northwind.Supplier item);

    [HttpPut("{SupplierID}")]
    public IActionResult PutSupplier(int key, [FromBody]Models.Northwind.Supplier newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.SupplierID != key))
            {
                return BadRequest();
            }

            this.OnSupplierUpdated(newItem);
            this.context.Suppliers.Update(newItem);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{SupplierID}")]
    public IActionResult PatchSupplier(int key, [FromBody]Delta<Models.Northwind.Supplier> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Suppliers.Where(i=>i.SupplierID == key).FirstOrDefault();
            
            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnSupplierUpdated(item);
            this.context.Suppliers.Update(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnSupplierCreated(Models.Northwind.Supplier item);

    [HttpPost]
    public IActionResult Post([FromBody] Models.Northwind.Supplier item)
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

            this.OnSupplierCreated(item);
            this.context.Suppliers.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Northwind/Suppliers/{item.SupplierID}", item);
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
