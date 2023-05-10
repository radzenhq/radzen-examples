using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CRMBlazorWasmRBS.Server.Controllers.RadzenCRM
{
    [Route("odata/RadzenCRM/Tasks")]
    public partial class TasksController : ODataController
    {
        private CRMBlazorWasmRBS.Server.Data.RadzenCRMContext context;

        public TasksController(CRMBlazorWasmRBS.Server.Data.RadzenCRMContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task> GetTasks()
        {
            var items = this.context.Tasks.AsQueryable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task>();
            this.OnTasksRead(ref items);

            return items;
        }

        partial void OnTasksRead(ref IQueryable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task> items);

        partial void OnTaskGet(ref SingleResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/RadzenCRM/Tasks(Id={Id})")]
        public SingleResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task> GetTask(int key)
        {
            var items = this.context.Tasks.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnTaskGet(ref result);

            return result;
        }
        partial void OnTaskDeleted(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task item);
        partial void OnAfterTaskDeleted(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task item);

        [HttpDelete("/odata/RadzenCRM/Tasks(Id={Id})")]
        public IActionResult DeleteTask(int key)
        {
            try
            {
                if (!ModelState.IsValid)
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
                this.OnAfterTaskDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTaskUpdated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task item);
        partial void OnAfterTaskUpdated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task item);

        [HttpPut("/odata/RadzenCRM/Tasks(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTask(int key, [FromBody]CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null || (item.Id != key))
                {
                    return BadRequest();
                }
                this.OnTaskUpdated(item);
                this.context.Tasks.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Tasks.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Opportunity,TaskStatus,TaskType");
                this.OnAfterTaskUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/RadzenCRM/Tasks(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTask(int key, [FromBody]Delta<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Tasks.Where(i => i.Id == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnTaskUpdated(item);
                this.context.Tasks.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Tasks.Where(i => i.Id == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Opportunity,TaskStatus,TaskType");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTaskCreated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task item);
        partial void OnAfterTaskCreated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CRMBlazorWasmRBS.Server.Models.RadzenCRM.Task item)
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

                var itemToReturn = this.context.Tasks.Where(i => i.Id == item.Id);

                Request.QueryString = Request.QueryString.Add("$expand", "Opportunity,TaskStatus,TaskType");

                this.OnAfterTaskCreated(item);

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
