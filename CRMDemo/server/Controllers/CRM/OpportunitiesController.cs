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



namespace Crm.Controllers.Crm
{
  using Models;
  using Data;
  using Models.Crm;

  [ODataRoutePrefix("odata/CRM/Opportunities")]
  [Route("mvc/odata/CRM/Opportunities")]
  public partial class OpportunitiesController : ODataController
  {
    private Data.CrmContext context;

    public OpportunitiesController(Data.CrmContext context)
    {
      this.context = context;
    }
    // GET /odata/Crm/Opportunities
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Crm.Opportunity> GetOpportunities()
    {
      var items = this.context.Opportunities.AsQueryable<Models.Crm.Opportunity>();
      this.OnOpportunitiesRead(ref items);

      return items;
    }

    partial void OnOpportunitiesRead(ref IQueryable<Models.Crm.Opportunity> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<Opportunity> GetOpportunity(int? key)
    {
        var items = this.context.Opportunities.Where(i=>i.Id == key);
        this.OnOpportunitiesGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnOpportunitiesGet(ref IQueryable<Models.Crm.Opportunity> items);

    partial void OnOpportunityDeleted(Models.Crm.Opportunity item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteOpportunity(int? key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Opportunities
                .Where(i => i.Id == key)
                .Include(i => i.Tasks)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnOpportunityDeleted(item);
            this.context.Opportunities.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOpportunityUpdated(Models.Crm.Opportunity item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutOpportunity(int? key, [FromBody]Models.Crm.Opportunity newItem)
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

            this.OnOpportunityUpdated(newItem);
            this.context.Opportunities.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Opportunities.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Contact,OpportunityStatus");
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
    public IActionResult PatchOpportunity(int? key, [FromBody]Delta<Models.Crm.Opportunity> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Opportunities.Where(i => i.Id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnOpportunityUpdated(item);
            this.context.Opportunities.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Opportunities.Where(i => i.Id == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Contact,OpportunityStatus");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOpportunityCreated(Models.Crm.Opportunity item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Crm.Opportunity item)
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

            this.OnOpportunityCreated(item);
            this.context.Opportunities.Add(item);
            this.context.SaveChanges();

            var key = item.Id;

            var itemToReturn = this.context.Opportunities.Where(i => i.Id == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Contact,OpportunityStatus");

            return new ObjectResult(SingleResult.Create(itemToReturn))
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
