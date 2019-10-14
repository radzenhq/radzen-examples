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

        public async Task<IQueryable<Models.Crm.Contact>> GetContacts(Query query = null)
        {
            var items = context.Contacts.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnContactsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnContactCreated(Models.Crm.Contact item);

        public async Task<Models.Crm.Contact> CreateContact(Models.Crm.Contact contact)
        {
            OnContactCreated(contact);

            context.Contacts.Add(contact);
            context.SaveChanges();

            return contact;
        }
    
        partial void OnOpportunitiesRead(ref IQueryable<Models.Crm.Opportunity> items);

        public async Task<IQueryable<Models.Crm.Opportunity>> GetOpportunities(Query query = null)
        {
            var items = context.Opportunities.AsQueryable();

            items = items.Include(i => i.Contact);

            items = items.Include(i => i.OpportunityStatus);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnOpportunitiesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOpportunityCreated(Models.Crm.Opportunity item);

        public async Task<Models.Crm.Opportunity> CreateOpportunity(Models.Crm.Opportunity opportunity)
        {
            OnOpportunityCreated(opportunity);

            context.Opportunities.Add(opportunity);
            context.SaveChanges();

            return opportunity;
        }
    
        partial void OnOpportunityStatusesRead(ref IQueryable<Models.Crm.OpportunityStatus> items);

        public async Task<IQueryable<Models.Crm.OpportunityStatus>> GetOpportunityStatuses(Query query = null)
        {
            var items = context.OpportunityStatuses.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnOpportunityStatusesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOpportunityStatusCreated(Models.Crm.OpportunityStatus item);

        public async Task<Models.Crm.OpportunityStatus> CreateOpportunityStatus(Models.Crm.OpportunityStatus opportunityStatus)
        {
            OnOpportunityStatusCreated(opportunityStatus);

            context.OpportunityStatuses.Add(opportunityStatus);
            context.SaveChanges();

            return opportunityStatus;
        }
    
        partial void OnTasksRead(ref IQueryable<Models.Crm.Task> items);

        public async Task<IQueryable<Models.Crm.Task>> GetTasks(Query query = null)
        {
            var items = context.Tasks.AsQueryable();

            items = items.Include(i => i.Opportunity);

            items = items.Include(i => i.TaskType);

            items = items.Include(i => i.TaskStatus);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnTasksRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTaskCreated(Models.Crm.Task item);

        public async Task<Models.Crm.Task> CreateTask(Models.Crm.Task task)
        {
            OnTaskCreated(task);

            context.Tasks.Add(task);
            context.SaveChanges();

            return task;
        }
    
        partial void OnTaskStatusesRead(ref IQueryable<Models.Crm.TaskStatus> items);

        public async Task<IQueryable<Models.Crm.TaskStatus>> GetTaskStatuses(Query query = null)
        {
            var items = context.TaskStatuses.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnTaskStatusesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTaskStatusCreated(Models.Crm.TaskStatus item);

        public async Task<Models.Crm.TaskStatus> CreateTaskStatus(Models.Crm.TaskStatus taskStatus)
        {
            OnTaskStatusCreated(taskStatus);

            context.TaskStatuses.Add(taskStatus);
            context.SaveChanges();

            return taskStatus;
        }
    
        partial void OnTaskTypesRead(ref IQueryable<Models.Crm.TaskType> items);

        public async Task<IQueryable<Models.Crm.TaskType>> GetTaskTypes(Query query = null)
        {
            var items = context.TaskTypes.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    items = items.Where(query.Filter);
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnTaskTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTaskTypeCreated(Models.Crm.TaskType item);

        public async Task<Models.Crm.TaskType> CreateTaskType(Models.Crm.TaskType taskType)
        {
            OnTaskTypeCreated(taskType);

            context.TaskTypes.Add(taskType);
            context.SaveChanges();

            return taskType;
        }
    
        partial void OnContactDeleted(Models.Crm.Contact item);

        public async Task<Models.Crm.Contact> DeleteContact(int? id)
        {
            var item = context.Contacts
                              .Where(i => i.Id == id)
                              .Include(i => i.Opportunities)
                              .FirstOrDefault();

            OnContactDeleted(item);

            context.Contacts.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnContactGet(Models.Crm.Contact item);

        public async Task<Models.Crm.Contact> GetContactById(int? id)
        {
            var item = context.Contacts
                              .AsNoTracking()
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            OnContactGet(item);

            return await Task.FromResult(item);
        }

        partial void OnContactUpdated(Models.Crm.Contact item);

        public async Task<Models.Crm.Contact> UpdateContact(int? id, Models.Crm.Contact contact)
        {
            OnContactUpdated(contact);

            var item = context.Contacts
                              .Where(i => i.Id == id)
                              .First();
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(contact);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return contact;
        }
    
        partial void OnOpportunityDeleted(Models.Crm.Opportunity item);

        public async Task<Models.Crm.Opportunity> DeleteOpportunity(int? id)
        {
            var item = context.Opportunities
                              .Where(i => i.Id == id)
                              .Include(i => i.Tasks)
                              .FirstOrDefault();

            OnOpportunityDeleted(item);

            context.Opportunities.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnOpportunityGet(Models.Crm.Opportunity item);

        public async Task<Models.Crm.Opportunity> GetOpportunityById(int? id)
        {
            var item = context.Opportunities
                              .AsNoTracking()
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            OnOpportunityGet(item);

            return await Task.FromResult(item);
        }

        partial void OnOpportunityUpdated(Models.Crm.Opportunity item);

        public async Task<Models.Crm.Opportunity> UpdateOpportunity(int? id, Models.Crm.Opportunity opportunity)
        {
            OnOpportunityUpdated(opportunity);

            var item = context.Opportunities
                              .Where(i => i.Id == id)
                              .First();
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(opportunity);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return opportunity;
        }
    
        partial void OnOpportunityStatusDeleted(Models.Crm.OpportunityStatus item);

        public async Task<Models.Crm.OpportunityStatus> DeleteOpportunityStatus(int? id)
        {
            var item = context.OpportunityStatuses
                              .Where(i => i.Id == id)
                              .Include(i => i.Opportunities)
                              .FirstOrDefault();

            OnOpportunityStatusDeleted(item);

            context.OpportunityStatuses.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnOpportunityStatusGet(Models.Crm.OpportunityStatus item);

        public async Task<Models.Crm.OpportunityStatus> GetOpportunityStatusById(int? id)
        {
            var item = context.OpportunityStatuses
                              .AsNoTracking()
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            OnOpportunityStatusGet(item);

            return await Task.FromResult(item);
        }

        partial void OnOpportunityStatusUpdated(Models.Crm.OpportunityStatus item);

        public async Task<Models.Crm.OpportunityStatus> UpdateOpportunityStatus(int? id, Models.Crm.OpportunityStatus opportunityStatus)
        {
            OnOpportunityStatusUpdated(opportunityStatus);

            var item = context.OpportunityStatuses
                              .Where(i => i.Id == id)
                              .First();
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(opportunityStatus);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return opportunityStatus;
        }
    
        partial void OnTaskDeleted(Models.Crm.Task item);

        public async Task<Models.Crm.Task> DeleteTask(int? id)
        {
            var item = context.Tasks
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            OnTaskDeleted(item);

            context.Tasks.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnTaskGet(Models.Crm.Task item);

        public async Task<Models.Crm.Task> GetTaskById(int? id)
        {
            var item = context.Tasks
                              .AsNoTracking()
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            OnTaskGet(item);

            return await Task.FromResult(item);
        }

        partial void OnTaskUpdated(Models.Crm.Task item);

        public async Task<Models.Crm.Task> UpdateTask(int? id, Models.Crm.Task task)
        {
            OnTaskUpdated(task);

            var item = context.Tasks
                              .Where(i => i.Id == id)
                              .First();
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(task);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return task;
        }
    
        partial void OnTaskStatusDeleted(Models.Crm.TaskStatus item);

        public async Task<Models.Crm.TaskStatus> DeleteTaskStatus(int? id)
        {
            var item = context.TaskStatuses
                              .Where(i => i.Id == id)
                              .Include(i => i.Tasks)
                              .FirstOrDefault();

            OnTaskStatusDeleted(item);

            context.TaskStatuses.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnTaskStatusGet(Models.Crm.TaskStatus item);

        public async Task<Models.Crm.TaskStatus> GetTaskStatusById(int? id)
        {
            var item = context.TaskStatuses
                              .AsNoTracking()
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            OnTaskStatusGet(item);

            return await Task.FromResult(item);
        }

        partial void OnTaskStatusUpdated(Models.Crm.TaskStatus item);

        public async Task<Models.Crm.TaskStatus> UpdateTaskStatus(int? id, Models.Crm.TaskStatus taskStatus)
        {
            OnTaskStatusUpdated(taskStatus);

            var item = context.TaskStatuses
                              .Where(i => i.Id == id)
                              .First();
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(taskStatus);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return taskStatus;
        }
    
        partial void OnTaskTypeDeleted(Models.Crm.TaskType item);

        public async Task<Models.Crm.TaskType> DeleteTaskType(int? id)
        {
            var item = context.TaskTypes
                              .Where(i => i.Id == id)
                              .Include(i => i.Tasks)
                              .FirstOrDefault();

            OnTaskTypeDeleted(item);

            context.TaskTypes.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnTaskTypeGet(Models.Crm.TaskType item);

        public async Task<Models.Crm.TaskType> GetTaskTypeById(int? id)
        {
            var item = context.TaskTypes
                              .AsNoTracking()
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            OnTaskTypeGet(item);

            return await Task.FromResult(item);
        }

        partial void OnTaskTypeUpdated(Models.Crm.TaskType item);

        public async Task<Models.Crm.TaskType> UpdateTaskType(int? id, Models.Crm.TaskType taskType)
        {
            OnTaskTypeUpdated(taskType);

            var item = context.TaskTypes
                              .Where(i => i.Id == id)
                              .First();
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(taskType);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return taskType;
        }
    }
}
