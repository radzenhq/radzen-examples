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



namespace MasterDetailTwoDataGrid.Controllers.Northwind
{
  using Models;
  using Data;
  using Models.Northwind;

  [ODataRoutePrefix("odata/Northwind/Regions")]
  [Route("mvc/odata/Northwind/Regions")]
  public partial class RegionsController : ODataController
  {
    private Data.NorthwindContext context;

    public RegionsController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/Regions
    [EnableQuery(MaxExpansionDepth=10)]
    [HttpGet]
    public IEnumerable<Models.Northwind.Region> GetRegions()
    {
      var items = this.context.Regions.AsQueryable<Models.Northwind.Region>();

      this.OnRegionsRead(ref items);

      return items;
    }

    partial void OnRegionsRead(ref IQueryable<Models.Northwind.Region> items);

    [EnableQuery(MaxExpansionDepth=10)]
    [HttpGet("{RegionID}")]
    public SingleResult<Region> GetRegion(int key)
    {
        var items = this.context.Regions.Where(i=>i.RegionID == key);

        return SingleResult.Create(items);
    }
    partial void OnRegionDeleted(Models.Northwind.Region item);

    [HttpDelete("{RegionID}")]
    public IActionResult DeleteRegion(int key) 
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Regions
                .Where(i => i.RegionID == key)
                .Include(i => i.Territories)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }                

            this.OnRegionDeleted(item);
            this.context.Regions.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnRegionUpdated(Models.Northwind.Region item);

    [HttpPut("{RegionID}")]
    public IActionResult PutRegion(int key, [FromBody]Models.Northwind.Region newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.RegionID != key))
            {
                return BadRequest();
            }

            this.OnRegionUpdated(newItem);
            this.context.Regions.Update(newItem);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{RegionID}")]
    public IActionResult PatchRegion(int key, [FromBody]Delta<Models.Northwind.Region> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Regions.Where(i=>i.RegionID == key).FirstOrDefault();
            
            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnRegionUpdated(item);
            this.context.Regions.Update(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnRegionCreated(Models.Northwind.Region item);

    [HttpPost]
    public IActionResult Post([FromBody] Models.Northwind.Region item)
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

            this.OnRegionCreated(item);
            this.context.Regions.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Northwind/Regions/{item.RegionID}", item);
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
