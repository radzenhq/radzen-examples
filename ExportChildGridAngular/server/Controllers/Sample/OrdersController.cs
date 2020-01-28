using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNet.OData.Query;



namespace Sample.Controllers.Sample
{
  using Models;
  using Data;
  using Models.Sample;

  [ODataRoutePrefix("odata/Sample/Orders")]
  [Route("mvc/odata/Sample/Orders")]
  public partial class OrdersController : ODataController
  {
    private Data.SampleContext context;

    public OrdersController(Data.SampleContext context)
    {
      this.context = context;
    }
    // GET /odata/Sample/Orders
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Sample.Order> GetOrders()
    {
      var items = this.context.Orders.AsQueryable<Models.Sample.Order>();
      this.OnOrdersRead(ref items);

      return items;
    }

    partial void OnOrdersRead(ref IQueryable<Models.Sample.Order> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<Order> GetOrder(int key)
    {
        var items = this.context.Orders.Where(i=>i.Id == key);
        this.OnOrdersGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnOrdersGet(ref IQueryable<Models.Sample.Order> items);

    partial void OnOrderDeleted(Models.Sample.Order item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteOrder(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Orders
                .Where(i => i.Id == key)
                .Include(i => i.OrderDetails)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnOrderDeleted(item);
            this.context.Orders.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOrderUpdated(Models.Sample.Order item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutOrder(int key, [FromBody]Models.Sample.Order newItem)
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

            this.OnOrderUpdated(newItem);
            this.context.Orders.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Orders.Where(i => i.Id == key);
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
    public IActionResult PatchOrder(int key, [FromBody]Delta<Models.Sample.Order> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Orders.Where(i => i.Id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnOrderUpdated(item);
            this.context.Orders.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Orders.Where(i => i.Id == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOrderCreated(Models.Sample.Order item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Sample.Order item)
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

            this.OnOrderCreated(item);
            this.context.Orders.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Sample/Orders/{item.Id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
