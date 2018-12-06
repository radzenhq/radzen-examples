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



namespace MultiTenantSample.Controllers.Sample
{
  using Models;
  using Data;
  using Models.Sample;

  [ODataRoutePrefix("odata/Sample/Products")]
  [Route("mvc/odata/Sample/Products")]
  public partial class ProductsController : ODataController
  {
    private Data.SampleContext context;

    public ProductsController(Data.SampleContext context)
    {
      this.context = context;
    }
    // GET /odata/Sample/Products
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Sample.Product> GetProducts()
    {
      var items = this.context.Products.AsQueryable<Models.Sample.Product>();
      this.OnProductsRead(ref items);

      return items;
    }

    partial void OnProductsRead(ref IQueryable<Models.Sample.Product> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<Product> GetProduct(int key)
    {
        var items = this.context.Products.Where(i=>i.Id == key);
        return SingleResult.Create(items);
    }
    partial void OnProductDeleted(Models.Sample.Product item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteProduct(int key) 
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Products
                .Where(i => i.Id == key)
                .Include(i => i.OrderDetails)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }                

            this.OnProductDeleted(item);
            this.context.Products.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnProductUpdated(Models.Sample.Product item);

    [HttpPut("{Id}")]
    public IActionResult PutProduct(int key, [FromBody]Models.Sample.Product newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.Id != key))
            {
                return BadRequest();
            }

            this.OnProductUpdated(newItem);
            this.context.Products.Update(newItem);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{Id}")]
    public IActionResult PatchProduct(int key, [FromBody]Delta<Models.Sample.Product> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Products.Where(i=>i.Id == key).FirstOrDefault();
            
            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnProductUpdated(item);
            this.context.Products.Update(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnProductCreated(Models.Sample.Product item);

    [HttpPost]
    public IActionResult Post([FromBody] Models.Sample.Product item)
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

            this.OnProductCreated(item);
            this.context.Products.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Sample/Products/{item.Id}", item);
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
