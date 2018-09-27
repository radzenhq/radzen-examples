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



namespace MasterDetailTwoDataGrid.Controllers.Northwind
{
  using Models;
  using Data;
  using Models.Northwind;

  [ODataRoutePrefix("odata/Northwind/Customers")]
  [Route("mvc/odata/Northwind/Customers")]
  public partial class CustomersController : ODataController
  {
    private Data.NorthwindContext context;

    public CustomersController(Data.NorthwindContext context)
    {
      this.context = context;
    }
    // GET /odata/Northwind/Customers
    [EnableQuery(MaxExpansionDepth=10)]
    [HttpGet]
    public IEnumerable<Models.Northwind.Customer> GetCustomers()
    {
      var items = this.context.Customers.AsQueryable<Models.Northwind.Customer>();

      this.OnCustomersRead(ref items);

      return items;
    }

    partial void OnCustomersRead(ref IQueryable<Models.Northwind.Customer> items);

    [EnableQuery(MaxExpansionDepth=10)]
    [HttpGet("{CustomerID}")]
    public SingleResult<Customer> GetCustomer(string key)
    {
        var items = this.context.Customers.Where(i=>i.CustomerID == key);

        return SingleResult.Create(items);
    }
    partial void OnCustomerDeleted(Models.Northwind.Customer item);

    [HttpDelete("{CustomerID}")]
    public IActionResult DeleteCustomer(string key) 
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Customers
                .Where(i => i.CustomerID == key)
                .Include(i => i.Orders)
                .Include(i => i.CustomerCustomerDemos)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }                

            this.OnCustomerDeleted(item);
            this.context.Customers.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnCustomerUpdated(Models.Northwind.Customer item);

    [HttpPut("{CustomerID}")]
    public IActionResult PutCustomer(string key, [FromBody]Models.Northwind.Customer newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.CustomerID != key))
            {
                return BadRequest();
            }

            this.OnCustomerUpdated(newItem);
            this.context.Customers.Update(newItem);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{CustomerID}")]
    public IActionResult PatchCustomer(string key, [FromBody]Delta<Models.Northwind.Customer> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Customers.Where(i=>i.CustomerID == key).FirstOrDefault();
            
            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnCustomerUpdated(item);
            this.context.Customers.Update(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnCustomerCreated(Models.Northwind.Customer item);

    [HttpPost]
    public IActionResult Post([FromBody] Models.Northwind.Customer item)
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

            this.OnCustomerCreated(item);
            this.context.Customers.Add(item);
            this.context.SaveChanges();

            return Created($"odata/Northwind/Customers/{item.CustomerID}", item);
        }
        catch(Exception ex) 
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
