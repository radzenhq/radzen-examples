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

  [ODataRoutePrefix("odata/CRM/TaskStatuses")]
  [Route("mvc/odata/CRM/TaskStatuses")]
  public partial class TaskStatusesController : ODataController
  {
    private Data.CrmContext context;

    public TaskStatusesController(Data.CrmContext context)
    {
      this.context = context;
    }
    // GET /odata/Crm/TaskStatuses
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Crm.TaskStatus> GetTaskStatuses()
    {
      var items = this.context.TaskStatuses.AsQueryable<Models.Crm.TaskStatus>();
      this.OnTaskStatusesRead(ref items);

      return items;
    }

    partial void OnTaskStatusesRead(ref IQueryable<Models.Crm.TaskStatus> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<TaskStatus> GetTaskStatus(int key)
    {
        var items = this.context.TaskStatuses.Where(i=>i.Id == key);
        return SingleResult.Create(items);
    }
    partial void OnTaskStatusDeleted(Models.Crm.TaskStatus item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteTaskStatus(int key) 
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.TaskStatuses
                .Where(i => i.Id == key)
                .Include(i => i.Tasks)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }                

            this.OnTaskStatusDeleted(item);
            this.context.TaskStatuses.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnTaskStatusUpdated(Models.Crm.TaskStatus item);

    [HttpPut("{Id}")]
    public IActionResult PutTaskStatus(int key, [FromBody]Models.Crm.TaskStatus newItem)
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

            this.OnTaskStatusUpdated(newItem);
            this.context.TaskStatuses.Update(newItem);
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
    public IActionResult PatchTaskStatus(int key, [FromBody]Delta<Models.Crm.TaskStatus> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.TaskStatuses.Where(i=>i.Id == key).FirstOrDefault();
            
            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnTaskStatusUpdated(item);
            this.context.TaskStatuses.Update(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnTaskStatusCreated(Models.Crm.TaskStatus item);

    [HttpPost]
    public IActionResult Post([FromBody] Models.Crm.TaskStatus item)
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

            this.OnTaskStatusCreated(item);
            this.context.TaskStatuses.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Crm/TaskStatuses/{item.Id}", item);
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
