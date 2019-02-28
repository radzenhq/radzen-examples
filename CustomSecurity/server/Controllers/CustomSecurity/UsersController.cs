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



namespace CustomSecurity.Controllers.CustomSecurity
{
  using Models;
  using Data;
  using Models.CustomSecurity;

  [ODataRoutePrefix("odata/CustomSecurity/Users")]
  [Route("mvc/odata/CustomSecurity/Users")]
  public partial class UsersController : ODataController
  {
    private Data.CustomSecurityContext context;

    public UsersController(Data.CustomSecurityContext context)
    {
      this.context = context;
    }
    // GET /odata/CustomSecurity/Users
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.CustomSecurity.User> GetUsers()
    {
      var items = this.context.Users.AsQueryable<Models.CustomSecurity.User>();
      this.OnUsersRead(ref items);

      return items;
    }

    partial void OnUsersRead(ref IQueryable<Models.CustomSecurity.User> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<User> GetUser(int key)
    {
        var items = this.context.Users.Where(i=>i.Id == key);
        return SingleResult.Create(items);
    }
    partial void OnUserDeleted(Models.CustomSecurity.User item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteUser(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Users
                .Where(i => i.Id == key)
                .Include(i => i.UserRoles)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnUserDeleted(item);
            this.context.Users.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnUserUpdated(Models.CustomSecurity.User item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutUser(int key, [FromBody]Models.CustomSecurity.User newItem)
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

            this.OnUserUpdated(newItem);
            this.context.Users.Update(newItem);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchUser(int key, [FromBody]Delta<Models.CustomSecurity.User> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Users.Where(i => i.Id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnUserUpdated(item);
            this.context.Users.Update(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnUserCreated(Models.CustomSecurity.User item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.CustomSecurity.User item)
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

            this.OnUserCreated(item);
            this.context.Users.Add(item);
            this.context.SaveChanges();

            return Created($"odata/CustomSecurity/Users/{item.Id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
