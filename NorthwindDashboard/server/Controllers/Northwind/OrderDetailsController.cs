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

  [ODataRoutePrefix("odata/Northwind/OrderDetails")]
  [Route("mvc/odata/Northwind/OrderDetails")]
  public partial class OrderDetailsController : ODataController
  {
    private Data.NorthwindContext context;

    public OrderDetailsController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/OrderDetails
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.Northwind.OrderDetail> GetOrderDetails()
    {
      var items = this.context.OrderDetails.AsQueryable<Models.Northwind.OrderDetail>();
      this.OnOrderDetailsRead(ref items);

      return items;
    }

    partial void OnOrderDetailsRead(ref IQueryable<Models.Northwind.OrderDetail> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{OrderID},{ProductID}")]
    public SingleResult<OrderDetail> GetOrderDetail([FromODataUri] int keyOrderID,[FromODataUri] int keyProductID)
    {
        var items = this.context.OrderDetails.Where(i=>i.OrderID == keyOrderID && i.ProductID == keyProductID);
        this.OnOrderDetailsGet(ref items);

        return SingleResult.Create(items);
    }

    partial void OnOrderDetailsGet(ref IQueryable<Models.Northwind.OrderDetail> items);

    partial void OnOrderDetailDeleted(Models.Northwind.OrderDetail item);

    [HttpDelete("{OrderID},{ProductID}")]
    public IActionResult DeleteOrderDetail([FromODataUri] int keyOrderID,[FromODataUri] int keyProductID)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.OrderDetails
                .Where(i => i.OrderID == keyOrderID && i.ProductID == keyProductID)
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

    partial void OnOrderDetailUpdated(Models.Northwind.OrderDetail item);

    [HttpPut("{OrderID},{ProductID}")]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutOrderDetail([FromODataUri] int keyOrderID,[FromODataUri] int keyProductID, [FromBody]Models.Northwind.OrderDetail newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.OrderID != keyOrderID && newItem.ProductID != keyProductID))
            {
                return BadRequest();
            }

            this.OnOrderDetailUpdated(newItem);
            this.context.OrderDetails.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.OrderDetails.Where(i => i.OrderID == keyOrderID && i.ProductID == keyProductID);
            Request.QueryString = Request.QueryString.Add("$expand", "Order,Product");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{OrderID},{ProductID}")]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchOrderDetail([FromODataUri] int keyOrderID,[FromODataUri] int keyProductID, [FromBody]Delta<Models.Northwind.OrderDetail> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.OrderDetails.Where(i => i.OrderID == keyOrderID && i.ProductID == keyProductID).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnOrderDetailUpdated(item);
            this.context.OrderDetails.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.OrderDetails.Where(i => i.OrderID == keyOrderID && i.ProductID == keyProductID);
            Request.QueryString = Request.QueryString.Add("$expand", "Order,Product");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnOrderDetailCreated(Models.Northwind.OrderDetail item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.Northwind.OrderDetail item)
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

            var keyOrderID = item.OrderID;
            var keyProductID = item.ProductID;

            var itemToReturn = this.context.OrderDetails.Where(i => i.OrderID == keyOrderID && i.ProductID == keyProductID);

            Request.QueryString = Request.QueryString.Add("$expand", "Order,Product");

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
