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
    [Route("odata/RadzenCRM/OpportunityStatuses")]
    public partial class OpportunityStatusesController : ODataController
    {
        private CRMBlazorWasmRBS.Server.Data.RadzenCRMContext context;

        public OpportunityStatusesController(CRMBlazorWasmRBS.Server.Data.RadzenCRMContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus> GetOpportunityStatuses()
        {
            var items = this.context.OpportunityStatuses.AsQueryable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus>();
            this.OnOpportunityStatusesRead(ref items);

            return items;
        }

        partial void OnOpportunityStatusesRead(ref IQueryable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus> items);

        partial void OnOpportunityStatusGet(ref SingleResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/RadzenCRM/OpportunityStatuses(Id={Id})")]
        public SingleResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus> GetOpportunityStatus(int key)
        {
            var items = this.context.OpportunityStatuses.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnOpportunityStatusGet(ref result);

            return result;
        }
        partial void OnOpportunityStatusDeleted(CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus item);
        partial void OnAfterOpportunityStatusDeleted(CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus item);

        [HttpDelete("/odata/RadzenCRM/OpportunityStatuses(Id={Id})")]
        public IActionResult DeleteOpportunityStatus(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.OpportunityStatuses
                    .Where(i => i.Id == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnOpportunityStatusDeleted(item);
                this.context.OpportunityStatuses.Remove(item);
                this.context.SaveChanges();
                this.OnAfterOpportunityStatusDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnOpportunityStatusUpdated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus item);
        partial void OnAfterOpportunityStatusUpdated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus item);

        [HttpPut("/odata/RadzenCRM/OpportunityStatuses(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutOpportunityStatus(int key, [FromBody]CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus item)
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
                this.OnOpportunityStatusUpdated(item);
                this.context.OpportunityStatuses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.OpportunityStatuses.Where(i => i.Id == key);
                ;
                this.OnAfterOpportunityStatusUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/RadzenCRM/OpportunityStatuses(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchOpportunityStatus(int key, [FromBody]Delta<CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.OpportunityStatuses.Where(i => i.Id == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnOpportunityStatusUpdated(item);
                this.context.OpportunityStatuses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.OpportunityStatuses.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnOpportunityStatusCreated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus item);
        partial void OnAfterOpportunityStatusCreated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CRMBlazorWasmRBS.Server.Models.RadzenCRM.OpportunityStatus item)
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

                this.OnOpportunityStatusCreated(item);
                this.context.OpportunityStatuses.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.OpportunityStatuses.Where(i => i.Id == item.Id);

                ;

                this.OnAfterOpportunityStatusCreated(item);

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
