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

  [ODataRoutePrefix("odata/CRM/TaskTypes")]
  [Route("mvc/odata/CRM/TaskTypes")]
  public partial class TaskTypesController : ODataController
  {
    private Data.CrmContext context;

    public TaskTypesController(Data.CrmContext context)
    {
      this.context = context;
    }
    // GET /odata/Crm/TaskTypes
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Crm.TaskType> GetTaskTypes()
    {
      var items = this.context.TaskTypes.AsQueryable<Models.Crm.TaskType>();
      this.OnTaskTypesRead(ref items);

      return items;
    }

    partial void OnTaskTypesRead(ref IQueryable<Models.Crm.TaskType> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<TaskType> GetTaskType(int key)
    {
        var items = this.context.TaskTypes.Where(i=>i.Id == key);
        return SingleResult.Create(items);
    }
    partial void OnTaskTypeDeleted(Models.Crm.TaskType item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteTaskType(int key) 
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.TaskTypes
                .Where(i => i.Id == key)
                .Include(i => i.Tasks)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }                

            this.OnTaskTypeDeleted(item);
            this.context.TaskTypes.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnTaskTypeUpdated(Models.Crm.TaskType item);

    [HttpPut("{Id}")]
    public IActionResult PutTaskType(int key, [FromBody]Models.Crm.TaskType newItem)
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

            this.OnTaskTypeUpdated(newItem);
            this.context.TaskTypes.Update(newItem);
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
    public IActionResult PatchTaskType(int key, [FromBody]Delta<Models.Crm.TaskType> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.TaskTypes.Where(i=>i.Id == key).FirstOrDefault();
            
            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnTaskTypeUpdated(item);
            this.context.TaskTypes.Update(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnTaskTypeCreated(Models.Crm.TaskType item);

    [HttpPost]
    public IActionResult Post([FromBody] Models.Crm.TaskType item)
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

            this.OnTaskTypeCreated(item);
            this.context.TaskTypes.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Crm/TaskTypes/{item.Id}", item);
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
