using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using RadzenCrm.Data;

namespace RadzenCrm
{
    public partial class CrmService
    {

      public CrmService(CrmContext context)
      {
        this.context = context;
      }

      public CrmContext context { get; set; }

        


      partial void OnContactsRead(ref IQueryable<Models.Crm.Contact> items);

      public async Task<IQueryable<Models.Crm.Contact>> GetContacts(string filter = null, string orderby = null)
      {
        var items = context.Contacts.AsQueryable();

        if(!string.IsNullOrEmpty(filter))
        {
          items = items.Where(filter);
        }

        if(!string.IsNullOrEmpty(orderby))
        {
          items = items.OrderBy(orderby);
        }

        OnContactsRead(ref items);

        return await Task.FromResult(items);
      }
    
      partial void OnContactCreated(Models.Crm.Contact item);



      public async Task<Models.Crm.Contact> CreateContact(Models.Crm.Contact contact)
      {
        try
        {
            OnContactCreated(contact);
            context.Contacts.Add(contact);
            context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }
        return contact;
      }
            


      partial void OnOpportunitiesRead(ref IQueryable<Models.Crm.Opportunity> items);

      public async Task<IQueryable<Models.Crm.Opportunity>> GetOpportunities(string filter = null, string orderby = null)
      {
        var items = context.Opportunities.AsQueryable();

        items = items.Include(i => i.Contact);

        items = items.Include(i => i.OpportunityStatus);

        if(!string.IsNullOrEmpty(filter))
        {
          items = items.Where(filter);
        }

        if(!string.IsNullOrEmpty(orderby))
        {
          items = items.OrderBy(orderby);
        }

        OnOpportunitiesRead(ref items);

        return await Task.FromResult(items);
      }
    
      partial void OnOpportunityCreated(Models.Crm.Opportunity item);



      public async Task<Models.Crm.Opportunity> CreateOpportunity(Models.Crm.Opportunity opportunity)
      {
        try
        {
            OnOpportunityCreated(opportunity);
            context.Opportunities.Add(opportunity);
            context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }
        return opportunity;
      }
            


      partial void OnOpportunityStatusesRead(ref IQueryable<Models.Crm.OpportunityStatus> items);

      public async Task<IQueryable<Models.Crm.OpportunityStatus>> GetOpportunityStatuses(string filter = null, string orderby = null)
      {
        var items = context.OpportunityStatuses.AsQueryable();

        if(!string.IsNullOrEmpty(filter))
        {
          items = items.Where(filter);
        }

        if(!string.IsNullOrEmpty(orderby))
        {
          items = items.OrderBy(orderby);
        }

        OnOpportunityStatusesRead(ref items);

        return await Task.FromResult(items);
      }
    
      partial void OnOpportunityStatusCreated(Models.Crm.OpportunityStatus item);



      public async Task<Models.Crm.OpportunityStatus> CreateOpportunityStatus(Models.Crm.OpportunityStatus opportunityStatus)
      {
        try
        {
            OnOpportunityStatusCreated(opportunityStatus);
            context.OpportunityStatuses.Add(opportunityStatus);
            context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }
        return opportunityStatus;
      }
            


      partial void OnTasksRead(ref IQueryable<Models.Crm.Task> items);

      public async Task<IQueryable<Models.Crm.Task>> GetTasks(string filter = null, string orderby = null)
      {
        var items = context.Tasks.AsQueryable();

        items = items.Include(i => i.Opportunity);

        items = items.Include(i => i.TaskType);

        items = items.Include(i => i.TaskStatus);

        if(!string.IsNullOrEmpty(filter))
        {
          items = items.Where(filter);
        }

        if(!string.IsNullOrEmpty(orderby))
        {
          items = items.OrderBy(orderby);
        }

        OnTasksRead(ref items);

        return await Task.FromResult(items);
      }
    
      partial void OnTaskCreated(Models.Crm.Task item);



      public async Task<Models.Crm.Task> CreateTask(Models.Crm.Task task)
      {
        try
        {
            OnTaskCreated(task);
            context.Tasks.Add(task);
            context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }
        return task;
      }
            


      partial void OnTaskStatusesRead(ref IQueryable<Models.Crm.TaskStatus> items);

      public async Task<IQueryable<Models.Crm.TaskStatus>> GetTaskStatuses(string filter = null, string orderby = null)
      {
        var items = context.TaskStatuses.AsQueryable();

        if(!string.IsNullOrEmpty(filter))
        {
          items = items.Where(filter);
        }

        if(!string.IsNullOrEmpty(orderby))
        {
          items = items.OrderBy(orderby);
        }

        OnTaskStatusesRead(ref items);

        return await Task.FromResult(items);
      }
    
      partial void OnTaskStatusCreated(Models.Crm.TaskStatus item);



      public async Task<Models.Crm.TaskStatus> CreateTaskStatus(Models.Crm.TaskStatus taskStatus)
      {
        try
        {
            OnTaskStatusCreated(taskStatus);
            context.TaskStatuses.Add(taskStatus);
            context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }
        return taskStatus;
      }
            


      partial void OnTaskTypesRead(ref IQueryable<Models.Crm.TaskType> items);

      public async Task<IQueryable<Models.Crm.TaskType>> GetTaskTypes(string filter = null, string orderby = null)
      {
        var items = context.TaskTypes.AsQueryable();

        if(!string.IsNullOrEmpty(filter))
        {
          items = items.Where(filter);
        }

        if(!string.IsNullOrEmpty(orderby))
        {
          items = items.OrderBy(orderby);
        }

        OnTaskTypesRead(ref items);

        return await Task.FromResult(items);
      }
    
      partial void OnTaskTypeCreated(Models.Crm.TaskType item);



      public async Task<Models.Crm.TaskType> CreateTaskType(Models.Crm.TaskType taskType)
      {
        try
        {
            OnTaskTypeCreated(taskType);
            context.TaskTypes.Add(taskType);
            context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }
        return taskType;
      }
            



      partial void OnContactDeleted(Models.Crm.Contact item);

      public async Task<Models.Crm.Contact> DeleteContact(int? id)
      {
        var item = context.Contacts
          .Where(i => i.Id == id)
          .Include(i => i.Opportunities)
          .FirstOrDefault();

        try
        {
            OnContactDeleted(item);
            context.Contacts.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }

        return item;
      }
    

      partial void OnContactGet(Models.Crm.Contact item);


      public async Task<Models.Crm.Contact> GetContactById(int? id)
      {
        var item = context.Contacts.Find(id);
        OnContactGet(item);
        return await Task.FromResult(item);
      }
    



      partial void OnContactUpdated(Models.Crm.Contact item);

      public async Task<Models.Crm.Contact> UpdateContact(int? id, Models.Crm.Contact contact)
      {
        try
        {
          OnContactUpdated(contact);
          context.Contacts.Update(contact);
          context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }

        return contact;
      }
            



      partial void OnOpportunityDeleted(Models.Crm.Opportunity item);

      public async Task<Models.Crm.Opportunity> DeleteOpportunity(int? id)
      {
        var item = context.Opportunities
          .Where(i => i.Id == id)
          .Include(i => i.Tasks)
          .FirstOrDefault();

        try
        {
            OnOpportunityDeleted(item);
            context.Opportunities.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }

        return item;
      }
    

      partial void OnOpportunityGet(Models.Crm.Opportunity item);


      public async Task<Models.Crm.Opportunity> GetOpportunityById(int? id)
      {
        var item = context.Opportunities.Find(id);
        OnOpportunityGet(item);
        return await Task.FromResult(item);
      }
    



      partial void OnOpportunityUpdated(Models.Crm.Opportunity item);

      public async Task<Models.Crm.Opportunity> UpdateOpportunity(int? id, Models.Crm.Opportunity opportunity)
      {
        try
        {
          OnOpportunityUpdated(opportunity);
          context.Opportunities.Update(opportunity);
          context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }

        return opportunity;
      }
            



      partial void OnOpportunityStatusDeleted(Models.Crm.OpportunityStatus item);

      public async Task<Models.Crm.OpportunityStatus> DeleteOpportunityStatus(int? id)
      {
        var item = context.OpportunityStatuses
          .Where(i => i.Id == id)
          .Include(i => i.Opportunities)
          .FirstOrDefault();

        try
        {
            OnOpportunityStatusDeleted(item);
            context.OpportunityStatuses.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }

        return item;
      }
    

      partial void OnOpportunityStatusGet(Models.Crm.OpportunityStatus item);


      public async Task<Models.Crm.OpportunityStatus> GetOpportunityStatusById(int? id)
      {
        var item = context.OpportunityStatuses.Find(id);
        OnOpportunityStatusGet(item);
        return await Task.FromResult(item);
      }
    



      partial void OnOpportunityStatusUpdated(Models.Crm.OpportunityStatus item);

      public async Task<Models.Crm.OpportunityStatus> UpdateOpportunityStatus(int? id, Models.Crm.OpportunityStatus opportunityStatus)
      {
        try
        {
          OnOpportunityStatusUpdated(opportunityStatus);
          context.OpportunityStatuses.Update(opportunityStatus);
          context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }

        return opportunityStatus;
      }
            



      partial void OnTaskDeleted(Models.Crm.Task item);

      public async Task<Models.Crm.Task> DeleteTask(int? id)
      {
        var item = context.Tasks
          .Where(i => i.Id == id)
          .FirstOrDefault();

        try
        {
            OnTaskDeleted(item);
            context.Tasks.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }

        return item;
      }
    

      partial void OnTaskGet(Models.Crm.Task item);


      public async Task<Models.Crm.Task> GetTaskById(int? id)
      {
        var item = context.Tasks.Find(id);
        OnTaskGet(item);
        return await Task.FromResult(item);
      }
    



      partial void OnTaskUpdated(Models.Crm.Task item);

      public async Task<Models.Crm.Task> UpdateTask(int? id, Models.Crm.Task task)
      {
        try
        {
          OnTaskUpdated(task);
          context.Tasks.Update(task);
          context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }

        return task;
      }
            



      partial void OnTaskStatusDeleted(Models.Crm.TaskStatus item);

      public async Task<Models.Crm.TaskStatus> DeleteTaskStatus(int? id)
      {
        var item = context.TaskStatuses
          .Where(i => i.Id == id)
          .Include(i => i.Tasks)
          .FirstOrDefault();

        try
        {
            OnTaskStatusDeleted(item);
            context.TaskStatuses.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }

        return item;
      }
    

      partial void OnTaskStatusGet(Models.Crm.TaskStatus item);


      public async Task<Models.Crm.TaskStatus> GetTaskStatusById(int? id)
      {
        var item = context.TaskStatuses.Find(id);
        OnTaskStatusGet(item);
        return await Task.FromResult(item);
      }
    



      partial void OnTaskStatusUpdated(Models.Crm.TaskStatus item);

      public async Task<Models.Crm.TaskStatus> UpdateTaskStatus(int? id, Models.Crm.TaskStatus taskStatus)
      {
        try
        {
          OnTaskStatusUpdated(taskStatus);
          context.TaskStatuses.Update(taskStatus);
          context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }

        return taskStatus;
      }
            



      partial void OnTaskTypeDeleted(Models.Crm.TaskType item);

      public async Task<Models.Crm.TaskType> DeleteTaskType(int? id)
      {
        var item = context.TaskTypes
          .Where(i => i.Id == id)
          .Include(i => i.Tasks)
          .FirstOrDefault();

        try
        {
            OnTaskTypeDeleted(item);
            context.TaskTypes.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }

        return item;
      }
    

      partial void OnTaskTypeGet(Models.Crm.TaskType item);


      public async Task<Models.Crm.TaskType> GetTaskTypeById(int? id)
      {
        var item = context.TaskTypes.Find(id);
        OnTaskTypeGet(item);
        return await Task.FromResult(item);
      }
    



      partial void OnTaskTypeUpdated(Models.Crm.TaskType item);

      public async Task<Models.Crm.TaskType> UpdateTaskType(int? id, Models.Crm.TaskType taskType)
      {
        try
        {
          OnTaskTypeUpdated(taskType);
          context.TaskTypes.Update(taskType);
          context.SaveChanges();
        }
        catch(Exception ex)
        {
            return null;
        }

        return taskType;
      }
        
  }
}
