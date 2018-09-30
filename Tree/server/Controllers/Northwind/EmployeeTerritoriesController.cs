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



namespace Tree.Controllers.Northwind
{
  using Models;
  using Data;
  using Models.Northwind;

  [ODataRoutePrefix("odata/Northwind/EmployeeTerritories")]
  [Route("mvc/odata/Northwind/EmployeeTerritories")]
  public partial class EmployeeTerritoriesController : ODataController
  {
    private Data.NorthwindContext context;

    public EmployeeTerritoriesController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/EmployeeTerritories
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.EmployeeTerritory> GetEmployeeTerritories()
    {
      var items = this.context.EmployeeTerritories.AsQueryable<Models.Northwind.EmployeeTerritory>();

      this.OnEmployeeTerritoriesRead(ref items);

      return items;
    }

    partial void OnEmployeeTerritoriesRead(ref IQueryable<Models.Northwind.EmployeeTerritory> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{EmployeeID},{TerritoryID}")]
    public SingleResult<EmployeeTerritory> GetEmployeeTerritory([FromODataUri] int keyEmployeeID,[FromODataUri] string keyTerritoryID)
    {
        var items = this.context.EmployeeTerritories.Where(i=>i.EmployeeID == keyEmployeeID && i.TerritoryID == keyTerritoryID);

        return SingleResult.Create(items);
    }
    partial void OnEmployeeTerritoryDeleted(Models.Northwind.EmployeeTerritory item);

    [HttpDelete("{EmployeeID},{TerritoryID}")]
    public IActionResult DeleteEmployeeTerritory([FromODataUri] int keyEmployeeID,[FromODataUri] string keyTerritoryID) 
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.EmployeeTerritories
                .Where(i => i.EmployeeID == keyEmployeeID && i.TerritoryID == keyTerritoryID)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }                

            this.OnEmployeeTerritoryDeleted(item);
            this.context.EmployeeTerritories.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnEmployeeTerritoryUpdated(Models.Northwind.EmployeeTerritory item);

    [HttpPut("{EmployeeID},{TerritoryID}")]
    public IActionResult PutEmployeeTerritory([FromODataUri] int keyEmployeeID,[FromODataUri] string keyTerritoryID, [FromBody]Models.Northwind.EmployeeTerritory newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.EmployeeID != keyEmployeeID && newItem.TerritoryID != keyTerritoryID))
            {
                return BadRequest();
            }

            this.OnEmployeeTerritoryUpdated(newItem);
            this.context.EmployeeTerritories.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.EmployeeTerritories
                .Where(i => i.EmployeeID == keyEmployeeID && i.TerritoryID == keyTerritoryID)
                .Include(i => i.Employee)
                .Include(i => i.Territory)
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

    [HttpPatch("{EmployeeID},{TerritoryID}")]
    public IActionResult PatchEmployeeTerritory([FromODataUri] int keyEmployeeID,[FromODataUri] string keyTerritoryID, [FromBody]Delta<Models.Northwind.EmployeeTerritory> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.EmployeeTerritories.Where(i=>i.EmployeeID == keyEmployeeID && i.TerritoryID == keyTerritoryID).FirstOrDefault();
            
            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnEmployeeTerritoryUpdated(item);
            this.context.EmployeeTerritories.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.EmployeeTerritories
                .Where(i => i.EmployeeID == keyEmployeeID && i.TerritoryID == keyTerritoryID)
                .Include(i => i.Employee)
                .Include(i => i.Territory)
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

    partial void OnEmployeeTerritoryCreated(Models.Northwind.EmployeeTerritory item);

    [HttpPost]
    public IActionResult Post([FromBody] Models.Northwind.EmployeeTerritory item)
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

            this.OnEmployeeTerritoryCreated(item);
            this.context.EmployeeTerritories.Add(item);
            this.context.SaveChanges();

            var keyEmployeeID = item.EmployeeID;
            var keyTerritoryID = item.TerritoryID;
            var itemToReturn = this.context.EmployeeTerritories
                .Where(i => i.EmployeeID == keyEmployeeID && i.TerritoryID == keyTerritoryID)
                .Include(i => i.Employee)
                .Include(i => i.Territory)
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
