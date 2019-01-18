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

  [ODataRoutePrefix("odata/CRM/Tasks")]
  [Route("mvc/odata/CRM/Tasks")]
  public partial class TasksController : ODataController
  {
    private Data.CrmContext context;

    public TasksController(Data.CrmContext context)
    {
      this.context = context;
    }
    // GET /odata/Crm/Tasks
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Crm.Task> GetTasks()
    {
      var items = this.context.Tasks.AsQueryable<Models.Crm.Task>();
      this.OnTasksRead(ref items);

      return items;
    }

    partial void OnTasksRead(ref IQueryable<Models.Crm.Task> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<Task> GetTask(int key)
    {
        var items = this.context.Tasks.Where(i=>i.Id == key);
        return SingleResult.Create(items);
    }
    partial void OnTaskDeleted(Models.Crm.Task item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteTask(int key) 
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Tasks
                .Where(i => i.Id == key)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }                

            this.OnTaskDeleted(item);
            this.context.Tasks.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnTaskUpdated(Models.Crm.Task item);

    [HttpPut("{Id}")]
    public IActionResult PutTask(int key, [FromBody]Models.Crm.Task newItem)
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

            this.OnTaskUpdated(newItem);
            this.context.Tasks.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Tasks
                .Where(i => i.Id == key)
                .Include(i => i.Opportunity)
                .Include(i => i.TaskType)
                .Include(i => i.TaskStatus)
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

    [HttpPatch("{Id}")]
    public IActionResult PatchTask(int key, [FromBody]Delta<Models.Crm.Task> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Tasks.Where(i=>i.Id == key).FirstOrDefault();
            
            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnTaskUpdated(item);
            this.context.Tasks.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Tasks
                .Where(i => i.Id == key)
                .Include(i => i.Opportunity)
                .Include(i => i.TaskType)
                .Include(i => i.TaskStatus)
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

    partial void OnTaskCreated(Models.Crm.Task item);

    [HttpPost]
    public IActionResult Post([FromBody] Models.Crm.Task item)
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

            this.OnTaskCreated(item);
            this.context.Tasks.Add(item);
            this.context.SaveChanges();

            var key = item.Id;
            var itemToReturn = this.context.Tasks
                .Where(i => i.Id == key)
                .Include(i => i.Opportunity)
                .Include(i => i.TaskType)
                .Include(i => i.TaskStatus)
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
