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



namespace Northwind.Controllers.Northwind
{
  using Models;
  using Data;
  using Models.Northwind;

  [ODataRoutePrefix("odata/Northwind/Orders")]
  [Route("mvc/odata/Northwind/Orders")]
  public partial class OrdersController : ODataController
  {
    private Data.NorthwindContext context;

    public OrdersController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/Orders
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.Order> GetOrders()
    {
      var items = this.context.Orders.AsQueryable<Models.Northwind.Order>();
      this.OnOrdersRead(ref items);

      return items;
    }

    partial void OnOrdersRead(ref IQueryable<Models.Northwind.Order> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{OrderID}")]
    public SingleResult<Order> GetOrder(int key)
    {
        var items = this.context.Orders.Where(i=>i.OrderID == key);
        this.OnOrdersGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnOrdersGet(ref IQueryable<Models.Northwind.Order> items);

    partial void OnOrderDeleted(Models.Northwind.Order item);

    [HttpDelete("{OrderID}")]
    public IActionResult DeleteOrder(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Orders
                .Where(i => i.OrderID == key)
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

    partial void OnOrderUpdated(Models.Northwind.Order item);

    [HttpPut("{OrderID}")]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutOrder(int key, [FromBody]Models.Northwind.Order newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.OrderID != key))
            {
                return BadRequest();
            }

            this.OnOrderUpdated(newItem);
            this.context.Orders.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Orders.Where(i => i.OrderID == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Customer,Employee,Shipper");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{OrderID}")]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchOrder(int key, [FromBody]Delta<Models.Northwind.Order> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Orders.Where(i => i.OrderID == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnOrderUpdated(item);
            this.context.Orders.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Orders.Where(i => i.OrderID == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Customer,Employee,Shipper");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOrderCreated(Models.Northwind.Order item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Northwind.Order item)
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

            var key = item.OrderID;

            var itemToReturn = this.context.Orders.Where(i => i.OrderID == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Customer,Employee,Shipper");

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
