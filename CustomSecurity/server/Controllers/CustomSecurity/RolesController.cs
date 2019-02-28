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

  [ODataRoutePrefix("odata/CustomSecurity/Roles")]
  [Route("mvc/odata/CustomSecurity/Roles")]
  public partial class RolesController : ODataController
  {
    private Data.CustomSecurityContext context;

    public RolesController(Data.CustomSecurityContext context)
    {
      this.context = context;
    }
    // GET /odata/CustomSecurity/Roles
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.CustomSecurity.Role> GetRoles()
    {
      var items = this.context.Roles.AsQueryable<Models.CustomSecurity.Role>();
      this.OnRolesRead(ref items);

      return items;
    }

    partial void OnRolesRead(ref IQueryable<Models.CustomSecurity.Role> items);

    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    [HttpGet("{Id}")]
    public SingleResult<Role> GetRole(int key)
    {
        var items = this.context.Roles.Where(i=>i.Id == key);
        return SingleResult.Create(items);
    }
    partial void OnRoleDeleted(Models.CustomSecurity.Role item);

    [HttpDelete("{Id}")]
    public IActionResult DeleteRole(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var item = this.context.Roles
                .Where(i => i.Id == key)
                .Include(i => i.UserRoles)
                .FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            this.OnRoleDeleted(item);
            this.context.Roles.Remove(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnRoleUpdated(Models.CustomSecurity.Role item);

    [HttpPut("{Id}")]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutRole(int key, [FromBody]Models.CustomSecurity.Role newItem)
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

            this.OnRoleUpdated(newItem);
            this.context.Roles.Update(newItem);
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
    public IActionResult PatchRole(int key, [FromBody]Delta<Models.CustomSecurity.Role> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.context.Roles.Where(i => i.Id == key).FirstOrDefault();

            if (item == null)
            {
                return BadRequest();
            }

            patch.Patch(item);

            this.OnRoleUpdated(item);
            this.context.Roles.Update(item);
            this.context.SaveChanges();

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnRoleCreated(Models.CustomSecurity.Role item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.CustomSecurity.Role item)
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

            this.OnRoleCreated(item);
            this.context.Roles.Add(item);
            this.context.SaveChanges();

            return Created($"odata/CustomSecurity/Roles/{item.Id}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
