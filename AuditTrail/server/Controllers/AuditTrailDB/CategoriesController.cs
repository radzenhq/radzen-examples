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



namespace AuditTrail.Controllers.AuditTrailDb
{
  using Models;
  using Data;
  using Models.AuditTrailDb;

  [ODataRoutePrefix("odata/AuditTrailDB/Categories")]
  [Route("mvc/odata/AuditTrailDB/Categories")]
  public partial class CategoriesController : ODataController
  {
    private Data.AuditTrailDbContext context;

    public CategoriesController(Data.AuditTrailDbContext context)
    {
      this.context = context;
    }
    // GET /odata/AuditTrailDb/Categories
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.AuditTrailDb.Category> GetCategories()
    {
      var items = this.context.Categories.AsQueryable<Models.AuditTrailDb.Category>();
      this.OnCategoriesRead(ref items);

      return items;
    }

    partial void OnCategoriesRead(ref IQueryable<Models.AuditTrailDb.Category> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<Category> GetCategory(int? key)
    {
        var items = this.context.Categories.Where(i=>i.Id == key);
        return SingleResult.Create(items);
    }
    partial void OnCategoryDeleted(Models.AuditTrailDb.Category item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteCategory(int? key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Categories
                .Where(i => i.Id == key)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnCategoryDeleted(item);
            this.context.Categories.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnCategoryUpdated(Models.AuditTrailDb.Category item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutCategory(int? key, [FromBody]Models.AuditTrailDb.Category newItem)
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

            this.OnCategoryUpdated(newItem);
            this.context.Categories.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Categories.Where(i => i.Id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchCategory(int? key, [FromBody]Delta<Models.AuditTrailDb.Category> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Categories.Where(i => i.Id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnCategoryUpdated(item);
            this.context.Categories.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Categories.Where(i => i.Id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnCategoryCreated(Models.AuditTrailDb.Category item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.AuditTrailDb.Category item)
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

            this.OnCategoryCreated(item);
            this.context.Categories.Add(item);
            this.context.SaveChanges();

            return Created($"odata/AuditTrailDb/Categories/{item.Id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
