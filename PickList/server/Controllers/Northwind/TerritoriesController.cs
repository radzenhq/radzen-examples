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



namespace PickList.Controllers.Northwind
{
  using Models;
  using Data;
  using Models.Northwind;

  [ODataRoutePrefix("odata/Northwind/Territories")]
  [Route("mvc/odata/Northwind/Territories")]
  public partial class TerritoriesController : ODataController
  {
    private Data.NorthwindContext context;

    public TerritoriesController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/Territories
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.Territory> GetTerritories()
    {
      var items = this.context.Territories.AsQueryable<Models.Northwind.Territory>();

      this.OnTerritoriesRead(ref items);

      return items;
    }

    partial void OnTerritoriesRead(ref IQueryable<Models.Northwind.Territory> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{TerritoryID}")]
    public SingleResult<Territory> GetTerritory(string key)
    {
        var items = this.context.Territories.Where(i=>i.TerritoryID == key);

        return SingleResult.Create(items);
    }
    partial void OnTerritoryDeleted(Models.Northwind.Territory item);

    [HttpDelete("{TerritoryID}")]
    public IActionResult DeleteTerritory(string key) 
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Territories
                .Where(i => i.TerritoryID == key)
                .Include(i => i.EmployeeTerritories)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }                

            this.OnTerritoryDeleted(item);
            this.context.Territories.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnTerritoryUpdated(Models.Northwind.Territory item);

    [HttpPut("{TerritoryID}")]
    public IActionResult PutTerritory(string key, [FromBody]Models.Northwind.Territory newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.TerritoryID != key))
            {
                return BadRequest();
            }

            this.OnTerritoryUpdated(newItem);
            this.context.Territories.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Territories
                .Where(i => i.TerritoryID == key)
                .Include(i => i.Region)
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

    [HttpPatch("{TerritoryID}")]
    public IActionResult PatchTerritory(string key, [FromBody]Delta<Models.Northwind.Territory> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Territories.Where(i=>i.TerritoryID == key).FirstOrDefault();
            
            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnTerritoryUpdated(item);
            this.context.Territories.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Territories
                .Where(i => i.TerritoryID == key)
                .Include(i => i.Region)
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

    partial void OnTerritoryCreated(Models.Northwind.Territory item);

    [HttpPost]
    public IActionResult Post([FromBody] Models.Northwind.Territory item)
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

            this.OnTerritoryCreated(item);
            this.context.Territories.Add(item);
            this.context.SaveChanges();

            var key = item.TerritoryID;
            var itemToReturn = this.context.Territories
                .Where(i => i.TerritoryID == key)
                .Include(i => i.Region)
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
