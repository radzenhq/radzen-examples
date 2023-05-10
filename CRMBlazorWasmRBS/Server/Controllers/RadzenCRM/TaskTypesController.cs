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
    [Route("odata/RadzenCRM/TaskTypes")]
    public partial class TaskTypesController : ODataController
    {
        private CRMBlazorWasmRBS.Server.Data.RadzenCRMContext context;

        public TaskTypesController(CRMBlazorWasmRBS.Server.Data.RadzenCRMContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType> GetTaskTypes()
        {
            var items = this.context.TaskTypes.AsQueryable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType>();
            this.OnTaskTypesRead(ref items);

            return items;
        }

        partial void OnTaskTypesRead(ref IQueryable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType> items);

        partial void OnTaskTypeGet(ref SingleResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/RadzenCRM/TaskTypes(Id={Id})")]
        public SingleResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType> GetTaskType(int key)
        {
            var items = this.context.TaskTypes.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnTaskTypeGet(ref result);

            return result;
        }
        partial void OnTaskTypeDeleted(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType item);
        partial void OnAfterTaskTypeDeleted(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType item);

        [HttpDelete("/odata/RadzenCRM/TaskTypes(Id={Id})")]
        public IActionResult DeleteTaskType(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.TaskTypes
                    .Where(i => i.Id == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnTaskTypeDeleted(item);
                this.context.TaskTypes.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTaskTypeDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTaskTypeUpdated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType item);
        partial void OnAfterTaskTypeUpdated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType item);

        [HttpPut("/odata/RadzenCRM/TaskTypes(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTaskType(int key, [FromBody]CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType item)
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
                this.OnTaskTypeUpdated(item);
                this.context.TaskTypes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TaskTypes.Where(i => i.Id == key);
                ;
                this.OnAfterTaskTypeUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/RadzenCRM/TaskTypes(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTaskType(int key, [FromBody]Delta<CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.TaskTypes.Where(i => i.Id == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnTaskTypeUpdated(item);
                this.context.TaskTypes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.TaskTypes.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTaskTypeCreated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType item);
        partial void OnAfterTaskTypeCreated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CRMBlazorWasmRBS.Server.Models.RadzenCRM.TaskType item)
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

                var itemToReturn = this.context.TaskTypes.Where(i => i.Id == item.Id);

                ;

                this.OnAfterTaskTypeCreated(item);

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
