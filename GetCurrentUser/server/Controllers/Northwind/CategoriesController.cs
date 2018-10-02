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



namespace GetCurrentUser.Controllers.Northwind
{
  using Models;
  using Data;
  using Models.Northwind;

  [ODataRoutePrefix("odata/Northwind/Categories")]
  [Route("mvc/odata/Northwind/Categories")]
  public partial class CategoriesController : ODataController
  {
    private Data.NorthwindContext context;

    public CategoriesController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/Categories
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.Category> GetCategories()
    {
      var items = this.context.Categories.AsQueryable<Models.Northwind.Category>();

      this.OnCategoriesRead(ref items);

      return items;
    }

    partial void OnCategoriesRead(ref IQueryable<Models.Northwind.Category> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{CategoryID}")]
    public SingleResult<Category> GetCategory(int key)
    {
        var items = this.context.Categories.Where(i=>i.CategoryID == key);

        return SingleResult.Create(items);
    }
    partial void OnCategoryDeleted(Models.Northwind.Category item);

    [HttpDelete("{CategoryID}")]
    public IActionResult DeleteCategory(int key) 
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Categories
                .Where(i => i.CategoryID == key)
                .Include(i => i.Products)
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

    partial void OnCategoryUpdated(Models.Northwind.Category item);

    [HttpPut("{CategoryID}")]
    public IActionResult PutCategory(int key, [FromBody]Models.Northwind.Category newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.CategoryID != key))
            {
                return BadRequest();
            }

            this.OnCategoryUpdated(newItem);
            this.context.Categories.Update(newItem);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{CategoryID}")]
    public IActionResult PatchCategory(int key, [FromBody]Delta<Models.Northwind.Category> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Categories.Where(i=>i.CategoryID == key).FirstOrDefault();
            
            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnCategoryUpdated(item);
            this.context.Categories.Update(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnCategoryCreated(Models.Northwind.Category item);

    [HttpPost]
    public IActionResult Post([FromBody] Models.Northwind.Category item)
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

            return Created($"odata/Northwind/Categories/{item.CategoryID}", item);
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
