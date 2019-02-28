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

  [ODataRoutePrefix("odata/CustomSecurity/UserRoles")]
  [Route("mvc/odata/CustomSecurity/UserRoles")]
  public partial class UserRolesController : ODataController
  {
    private Data.CustomSecurityContext context;

    public UserRolesController(Data.CustomSecurityContext context)
    {
      this.context = context;
    }
    // GET /odata/CustomSecurity/UserRoles
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.CustomSecurity.UserRole> GetUserRoles()
    {
      var items = this.context.UserRoles.AsQueryable<Models.CustomSecurity.UserRole>();
      this.OnUserRolesRead(ref items);

      return items;
    }

    partial void OnUserRolesRead(ref IQueryable<Models.CustomSecurity.UserRole> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{UserId},{RoleId}")]
    public SingleResult<UserRole> GetUserRole([FromODataUri] int keyUserId,[FromODataUri] int keyRoleId)
    {
        var items = this.context.UserRoles.Where(i=>i.UserId == keyUserId && i.RoleId == keyRoleId);
        return SingleResult.Create(items);
    }
    partial void OnUserRoleDeleted(Models.CustomSecurity.UserRole item);

    [HttpDelete("{UserId},{RoleId}")]
    public IActionResult DeleteUserRole([FromODataUri] int keyUserId,[FromODataUri] int keyRoleId)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.UserRoles
                .Where(i => i.UserId == keyUserId && i.RoleId == keyRoleId)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnUserRoleDeleted(item);
            this.context.UserRoles.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnUserRoleUpdated(Models.CustomSecurity.UserRole item);

    [HttpPut("{UserId},{RoleId}")]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutUserRole([FromODataUri] int keyUserId,[FromODataUri] int keyRoleId, [FromBody]Models.CustomSecurity.UserRole newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (newItem == null || (newItem.UserId != keyUserId && newItem.RoleId != keyRoleId))
            {
                return BadRequest();
            }

            this.OnUserRoleUpdated(newItem);
            this.context.UserRoles.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.UserRoles.Where(i => i.UserId == keyUserId && i.RoleId == keyRoleId);

            Request.QueryString = Request.QueryString.Add("$expand", "User,Role");

            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("{UserId},{RoleId}")]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchUserRole([FromODataUri] int keyUserId,[FromODataUri] int keyRoleId, [FromBody]Delta<Models.CustomSecurity.UserRole> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.UserRoles.Where(i => i.UserId == keyUserId && i.RoleId == keyRoleId).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnUserRoleUpdated(item);
            this.context.UserRoles.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.UserRoles.Where(i => i.UserId == keyUserId && i.RoleId == keyRoleId);

            Request.QueryString = Request.QueryString.Add("$expand", "User,Role");

            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnUserRoleCreated(Models.CustomSecurity.UserRole item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.CustomSecurity.UserRole item)
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

            this.OnUserRoleCreated(item);
            this.context.UserRoles.Add(item);
            this.context.SaveChanges();

            var keyUserId = item.UserId;
            var keyRoleId = item.RoleId;

            var itemToReturn = this.context.UserRoles.Where(i => i.UserId == keyUserId && i.RoleId == keyRoleId);

            Request.QueryString = Request.QueryString.Add("$expand", "User,Role");

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
