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



namespace InvokeCustomServerMethod.Controllers.Sample
{
  using Models;
  using Data;
  using Models.Sample;

  [ODataRoutePrefix("odata/Sample/OrderDetails")]
  [Route("mvc/odata/Sample/OrderDetails")]
  public partial class OrderDetailsController : ODataController
  {
    private Data.SampleContext context;

    public OrderDetailsController(Data.SampleContext context)
    {
      this.context = context;
    }
    // GET /odata/Sample/OrderDetails
    [EnableQuery(MaxExpansionDepth=10)]
    [HttpGet]
    public IEnumerable<Models.Sample.OrderDetail> GetOrderDetails()
    {
      var items = this.context.OrderDetails.AsQueryable<Models.Sample.OrderDetail>();

      this.OnOrderDetailsRead(ref items);

      return items;
    }

    partial void OnOrderDetailsRead(ref IQueryable<Models.Sample.OrderDetail> items);

    [EnableQuery(MaxExpansionDepth=10)]
    [HttpGet("{Id}")]
    public SingleResult<OrderDetail> GetOrderDetail(int key)
    {
        var items = this.context.OrderDetails.Where(i=>i.Id == key);

        return SingleResult.Create(items);
    }
    partial void OnOrderDetailDeleted(Models.Sample.OrderDetail item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteOrderDetail(int key) 
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.OrderDetails
                .Where(i => i.Id == key)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }                

            this.OnOrderDetailDeleted(item);
            this.context.OrderDetails.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOrderDetailUpdated(Models.Sample.OrderDetail item);

    [HttpPut("{Id}")]
    public IActionResult PutOrderDetail(int key, [FromBody]Models.Sample.OrderDetail newItem)
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

            this.OnOrderDetailUpdated(newItem);
            this.context.OrderDetails.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.OrderDetails
                .Where(i => i.Id == key)
                .Include(i => i.Order)
                .Include(i => i.Product)
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
    public IActionResult PatchOrderDetail(int key, [FromBody]Delta<Models.Sample.OrderDetail> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.OrderDetails.Where(i=>i.Id == key).FirstOrDefault();
            
            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnOrderDetailUpdated(item);
            this.context.OrderDetails.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.OrderDetails
                .Where(i => i.Id == key)
                .Include(i => i.Order)
                .Include(i => i.Product)
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

    partial void OnOrderDetailCreated(Models.Sample.OrderDetail item);

    [HttpPost]
    public IActionResult Post([FromBody] Models.Sample.OrderDetail item)
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

            this.OnOrderDetailCreated(item);
            this.context.OrderDetails.Add(item);
            this.context.SaveChanges();

            var key = item.Id;
            var itemToReturn = this.context.OrderDetails
                .Where(i => i.Id == key)
                .Include(i => i.Order)
                .Include(i => i.Product)
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
