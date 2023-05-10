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
    [Route("odata/RadzenCRM/Contacts")]
    public partial class ContactsController : ODataController
    {
        private CRMBlazorWasmRBS.Server.Data.RadzenCRMContext context;

        public ContactsController(CRMBlazorWasmRBS.Server.Data.RadzenCRMContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact> GetContacts()
        {
            var items = this.context.Contacts.AsQueryable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact>();
            this.OnContactsRead(ref items);

            return items;
        }

        partial void OnContactsRead(ref IQueryable<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact> items);

        partial void OnContactGet(ref SingleResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/RadzenCRM/Contacts(Id={Id})")]
        public SingleResult<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact> GetContact(int key)
        {
            var items = this.context.Contacts.Where(i => i.Id == key);
            var result = SingleResult.Create(items);

            OnContactGet(ref result);

            return result;
        }
        partial void OnContactDeleted(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact item);
        partial void OnAfterContactDeleted(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact item);

        [HttpDelete("/odata/RadzenCRM/Contacts(Id={Id})")]
        public IActionResult DeleteContact(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var item = this.context.Contacts
                    .Where(i => i.Id == key)
                    .FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                this.OnContactDeleted(item);
                this.context.Contacts.Remove(item);
                this.context.SaveChanges();
                this.OnAfterContactDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContactUpdated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact item);
        partial void OnAfterContactUpdated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact item);

        [HttpPut("/odata/RadzenCRM/Contacts(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutContact(int key, [FromBody]CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact item)
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
                this.OnContactUpdated(item);
                this.context.Contacts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contacts.Where(i => i.Id == key);
                ;
                this.OnAfterContactUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/RadzenCRM/Contacts(Id={Id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchContact(int key, [FromBody]Delta<CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var item = this.context.Contacts.Where(i => i.Id == key).FirstOrDefault();

                if (item == null)
                {
                    return BadRequest();
                }
                patch.Patch(item);

                this.OnContactUpdated(item);
                this.context.Contacts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contacts.Where(i => i.Id == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContactCreated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact item);
        partial void OnAfterContactCreated(CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] CRMBlazorWasmRBS.Server.Models.RadzenCRM.Contact item)
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

                this.OnContactCreated(item);
                this.context.Contacts.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contacts.Where(i => i.Id == item.Id);

                ;

                this.OnAfterContactCreated(item);

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
