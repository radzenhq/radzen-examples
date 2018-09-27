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

  [ODataRoutePrefix("odata/Northwind/CustomerDemographics")]
  [Route("mvc/odata/Northwind/CustomerDemographics")]
  public partial class CustomerDemographicsController : ODataController
  {
    private Data.NorthwindContext context;

    public CustomerDemographicsController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/CustomerDemographics
    [EnableQuery(MaxExpansionDepth=10)]
    [HttpGet]
    public IEnumerable<Models.Northwind.CustomerDemographic> GetCustomerDemographics()
    {
      var items = this.context.CustomerDemographics.AsQueryable<Models.Northwind.CustomerDemographic>();

      this.OnCustomerDemographicsRead(ref items);

      return items;
    }

    partial void OnCustomerDemographicsRead(ref IQueryable<Models.Northwind.CustomerDemographic> items);

    [EnableQuery(MaxExpansionDepth=10)]
    [HttpGet("{CustomerTypeID}")]
    public SingleResult<CustomerDemographic> GetCustomerDemographic(string key)
    {
        var items = this.context.CustomerDemographics.Where(i=>i.CustomerTypeID == key);

        return SingleResult.Create(items);
    }
    partial void OnCustomerDemographicDeleted(Models.Northwind.CustomerDemographic item);

    [HttpDelete("{CustomerTypeID}")]
    public IActionResult DeleteCustomerDemographic(string key) 
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.CustomerDemographics
                .Where(i => i.CustomerTypeID == key)
                .Include(i => i.CustomerCustomerDemos)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }                

            this.OnCustomerDemographicDeleted(item);
            this.context.CustomerDemographics.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnCustomerDemographicUpdated(Models.Northwind.CustomerDemographic item);

    [HttpPut("{CustomerTypeID}")]
    public IActionResult PutCustomerDemographic(string key, [FromBody]Models.Northwind.CustomerDemographic newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.CustomerTypeID != key))
            {
                return BadRequest();
            }

            this.OnCustomerDemographicUpdated(newItem);
            this.context.CustomerDemographics.Update(newItem);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{CustomerTypeID}")]
    public IActionResult PatchCustomerDemographic(string key, [FromBody]Delta<Models.Northwind.CustomerDemographic> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.CustomerDemographics.Where(i=>i.CustomerTypeID == key).FirstOrDefault();
            
            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnCustomerDemographicUpdated(item);
            this.context.CustomerDemographics.Update(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnCustomerDemographicCreated(Models.Northwind.CustomerDemographic item);

    [HttpPost]
    public IActionResult Post([FromBody] Models.Northwind.CustomerDemographic item)
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

            this.OnCustomerDemographicCreated(item);
            this.context.CustomerDemographics.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Northwind/CustomerDemographics/{item.CustomerTypeID}", item);
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
