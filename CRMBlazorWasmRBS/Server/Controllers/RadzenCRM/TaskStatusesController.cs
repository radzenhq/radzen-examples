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
    [Route("odata/RadzenCRM/TaskStatuses")]
    public partial class TaskStatusesController : ODataController
    {
        private CRMBlazorWasmRBS.Server.Data.RadzenCRMContext context;

        public TaskStatusesController(CRMBlazorWasmRBS.Server.Data.RadzenCRMContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus> GetTaskStatuses()
        {
            var items = this.context.TaskStatuses.AsQueryable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus>();
            this.OnTaskStatusesRead(ref items);

            return items;
        }

        partial void OnTaskStatusesRead(ref IQueryable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus> items);

        partial void OnTaskStatusGet(ref SingleResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/RadzenCRM/TaskStatuses(Id={Id})")]
        public SingleResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus> GetTaskStatus(int key)
        {
            var items = this.context.TaskStatuses.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnTaskStatusGet(ref result);

            return result;
        }
        partial void OnTaskStatusDeleted(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus item);
        partial void OnAfterTaskStatusDeleted(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus item);

        [HttpDelete("/odata/RadzenCRM/TaskStatuses(Id={Id})")]
        public IActionResult DeleteTaskStatus(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.TaskStatuses
                    .Where(i => i.Id == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnTaskStatusDeleted(item);
                this.context.TaskStatuses.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTaskStatusDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTaskStatusUpdated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus item);
        partial void OnAfterTaskStatusUpdated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus item);

        [HttpPut("/odata/RadzenCRM/TaskStatuses(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTaskStatus(int key, [FromBody]CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus item)
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
                this.OnTaskStatusUpdated(item);
                this.context.TaskStatuses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TaskStatuses.Where(i => i.Id == key);
                ;
                this.OnAfterTaskStatusUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/RadzenCRM/TaskStatuses(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTaskStatus(int key, [FromBody]Delta<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.TaskStatuses.Where(i => i.Id == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnTaskStatusUpdated(item);
                this.context.TaskStatuses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TaskStatuses.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTaskStatusCreated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus item);
        partial void OnAfterTaskStatusCreated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskStatus item)
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

                var itemToReturn = this.context.TaskStatuses.Where(i => i.Id == item.Id);

                ;

                this.OnAfterTaskStatusCreated(item);

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
