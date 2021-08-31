using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Text.Encodings.Web;
using Microsoft.Data.SqlClient;
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
        CrmContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly CrmContext context;
        private readonly NavigationManager navigationManager;

        public CrmService(CrmContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public async Task ExportContactsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/contacts/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportContactsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/contacts/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnContactsRead(ref IQueryable<Models.Crm.Contact> items);

        public async Task<IQueryable<Models.Crm.Contact>> GetContacts(Query query = null)
        {
            var items = Context.Contacts.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
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
        partial void OnAfterContactCreated(Models.Crm.Contact item);

        public async Task<Models.Crm.Contact> CreateContact(Models.Crm.Contact contact)
        {
            OnContactCreated(contact);

            Context.Contacts.Add(contact);
            Context.SaveChanges();

            OnAfterContactCreated(contact);

            return contact;
        }
        public async Task ExportOpportunitiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/opportunities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/opportunities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportOpportunitiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/opportunities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/opportunities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnOpportunitiesRead(ref IQueryable<Models.Crm.Opportunity> items);

        public async Task<IQueryable<Models.Crm.Opportunity>> GetOpportunities(Query query = null)
        {
            var items = Context.Opportunities.AsQueryable();

            items = items.Include(i => i.Contact);

            items = items.Include(i => i.OpportunityStatus);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
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
        partial void OnAfterOpportunityCreated(Models.Crm.Opportunity item);

        public async Task<Models.Crm.Opportunity> CreateOpportunity(Models.Crm.Opportunity opportunity)
        {
            OnOpportunityCreated(opportunity);

            Context.Opportunities.Add(opportunity);
            Context.SaveChanges();

            OnAfterOpportunityCreated(opportunity);

            return opportunity;
        }
        public async Task ExportOpportunityStatusesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/opportunitystatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/opportunitystatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportOpportunityStatusesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/opportunitystatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/opportunitystatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnOpportunityStatusesRead(ref IQueryable<Models.Crm.OpportunityStatus> items);

        public async Task<IQueryable<Models.Crm.OpportunityStatus>> GetOpportunityStatuses(Query query = null)
        {
            var items = Context.OpportunityStatuses.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
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
        partial void OnAfterOpportunityStatusCreated(Models.Crm.OpportunityStatus item);

        public async Task<Models.Crm.OpportunityStatus> CreateOpportunityStatus(Models.Crm.OpportunityStatus opportunityStatus)
        {
            OnOpportunityStatusCreated(opportunityStatus);

            Context.OpportunityStatuses.Add(opportunityStatus);
            Context.SaveChanges();

            OnAfterOpportunityStatusCreated(opportunityStatus);

            return opportunityStatus;
        }
        public async Task ExportTasksToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/tasks/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/tasks/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTasksToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/tasks/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/tasks/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTasksRead(ref IQueryable<Models.Crm.Task> items);

        public async Task<IQueryable<Models.Crm.Task>> GetTasks(Query query = null)
        {
            var items = Context.Tasks.AsQueryable();

            items = items.Include(i => i.Opportunity);

            items = items.Include(i => i.TaskType);

            items = items.Include(i => i.TaskStatus);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
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
        partial void OnAfterTaskCreated(Models.Crm.Task item);

        public async Task<Models.Crm.Task> CreateTask(Models.Crm.Task task)
        {
            OnTaskCreated(task);

            Context.Tasks.Add(task);
            Context.SaveChanges();

            OnAfterTaskCreated(task);

            return task;
        }
        public async Task ExportTaskStatusesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/taskstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/taskstatuses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTaskStatusesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/taskstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/taskstatuses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTaskStatusesRead(ref IQueryable<Models.Crm.TaskStatus> items);

        public async Task<IQueryable<Models.Crm.TaskStatus>> GetTaskStatuses(Query query = null)
        {
            var items = Context.TaskStatuses.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
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
        partial void OnAfterTaskStatusCreated(Models.Crm.TaskStatus item);

        public async Task<Models.Crm.TaskStatus> CreateTaskStatus(Models.Crm.TaskStatus taskStatus)
        {
            OnTaskStatusCreated(taskStatus);

            Context.TaskStatuses.Add(taskStatus);
            Context.SaveChanges();

            OnAfterTaskStatusCreated(taskStatus);

            return taskStatus;
        }
        public async Task ExportTaskTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/tasktypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/tasktypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTaskTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/crm/tasktypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/crm/tasktypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTaskTypesRead(ref IQueryable<Models.Crm.TaskType> items);

        public async Task<IQueryable<Models.Crm.TaskType>> GetTaskTypes(Query query = null)
        {
            var items = Context.TaskTypes.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
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
        partial void OnAfterTaskTypeCreated(Models.Crm.TaskType item);

        public async Task<Models.Crm.TaskType> CreateTaskType(Models.Crm.TaskType taskType)
        {
            OnTaskTypeCreated(taskType);

            Context.TaskTypes.Add(taskType);
            Context.SaveChanges();

            OnAfterTaskTypeCreated(taskType);

            return taskType;
        }

        partial void OnContactDeleted(Models.Crm.Contact item);
        partial void OnAfterContactDeleted(Models.Crm.Contact item);

        public async Task<Models.Crm.Contact> DeleteContact(int? id)
        {
            var itemToDelete = Context.Contacts
                              .Where(i => i.Id == id)
                              .Include(i => i.Opportunities)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnContactDeleted(itemToDelete);

            Context.Contacts.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterContactDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnContactGet(Models.Crm.Contact item);

        public async Task<Models.Crm.Contact> GetContactById(int? id)
        {
            var items = Context.Contacts
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnContactGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Crm.Contact> CancelContactChanges(Models.Crm.Contact item)
        {
            var entityToCancel = Context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnContactUpdated(Models.Crm.Contact item);
        partial void OnAfterContactUpdated(Models.Crm.Contact item);

        public async Task<Models.Crm.Contact> UpdateContact(int? id, Models.Crm.Contact contact)
        {
            OnContactUpdated(contact);

            var itemToUpdate = Context.Contacts
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(contact);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterContactUpdated(contact);

            return contact;
        }

        partial void OnOpportunityDeleted(Models.Crm.Opportunity item);
        partial void OnAfterOpportunityDeleted(Models.Crm.Opportunity item);

        public async Task<Models.Crm.Opportunity> DeleteOpportunity(int? id)
        {
            var itemToDelete = Context.Opportunities
                              .Where(i => i.Id == id)
                              .Include(i => i.Tasks)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOpportunityDeleted(itemToDelete);

            Context.Opportunities.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterOpportunityDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnOpportunityGet(Models.Crm.Opportunity item);

        public async Task<Models.Crm.Opportunity> GetOpportunityById(int? id)
        {
            var items = Context.Opportunities
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Contact);

            items = items.Include(i => i.OpportunityStatus);

            var itemToReturn = items.FirstOrDefault();

            OnOpportunityGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Crm.Opportunity> CancelOpportunityChanges(Models.Crm.Opportunity item)
        {
            var entityToCancel = Context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnOpportunityUpdated(Models.Crm.Opportunity item);
        partial void OnAfterOpportunityUpdated(Models.Crm.Opportunity item);

        public async Task<Models.Crm.Opportunity> UpdateOpportunity(int? id, Models.Crm.Opportunity opportunity)
        {
            OnOpportunityUpdated(opportunity);

            var itemToUpdate = Context.Opportunities
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(opportunity);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterOpportunityUpdated(opportunity);

            return opportunity;
        }

        partial void OnOpportunityStatusDeleted(Models.Crm.OpportunityStatus item);
        partial void OnAfterOpportunityStatusDeleted(Models.Crm.OpportunityStatus item);

        public async Task<Models.Crm.OpportunityStatus> DeleteOpportunityStatus(int? id)
        {
            var itemToDelete = Context.OpportunityStatuses
                              .Where(i => i.Id == id)
                              .Include(i => i.Opportunities)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOpportunityStatusDeleted(itemToDelete);

            Context.OpportunityStatuses.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterOpportunityStatusDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnOpportunityStatusGet(Models.Crm.OpportunityStatus item);

        public async Task<Models.Crm.OpportunityStatus> GetOpportunityStatusById(int? id)
        {
            var items = Context.OpportunityStatuses
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnOpportunityStatusGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Crm.OpportunityStatus> CancelOpportunityStatusChanges(Models.Crm.OpportunityStatus item)
        {
            var entityToCancel = Context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnOpportunityStatusUpdated(Models.Crm.OpportunityStatus item);
        partial void OnAfterOpportunityStatusUpdated(Models.Crm.OpportunityStatus item);

        public async Task<Models.Crm.OpportunityStatus> UpdateOpportunityStatus(int? id, Models.Crm.OpportunityStatus opportunityStatus)
        {
            OnOpportunityStatusUpdated(opportunityStatus);

            var itemToUpdate = Context.OpportunityStatuses
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(opportunityStatus);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterOpportunityStatusUpdated(opportunityStatus);

            return opportunityStatus;
        }

        partial void OnTaskDeleted(Models.Crm.Task item);
        partial void OnAfterTaskDeleted(Models.Crm.Task item);

        public async Task<Models.Crm.Task> DeleteTask(int? id)
        {
            var itemToDelete = Context.Tasks
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTaskDeleted(itemToDelete);

            Context.Tasks.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTaskDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnTaskGet(Models.Crm.Task item);

        public async Task<Models.Crm.Task> GetTaskById(int? id)
        {
            var items = Context.Tasks
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Opportunity);

            items = items.Include(i => i.TaskType);

            items = items.Include(i => i.TaskStatus);

            var itemToReturn = items.FirstOrDefault();

            OnTaskGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Crm.Task> CancelTaskChanges(Models.Crm.Task item)
        {
            var entityToCancel = Context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnTaskUpdated(Models.Crm.Task item);
        partial void OnAfterTaskUpdated(Models.Crm.Task item);

        public async Task<Models.Crm.Task> UpdateTask(int? id, Models.Crm.Task task)
        {
            OnTaskUpdated(task);

            var itemToUpdate = Context.Tasks
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(task);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterTaskUpdated(task);

            return task;
        }

        partial void OnTaskStatusDeleted(Models.Crm.TaskStatus item);
        partial void OnAfterTaskStatusDeleted(Models.Crm.TaskStatus item);

        public async Task<Models.Crm.TaskStatus> DeleteTaskStatus(int? id)
        {
            var itemToDelete = Context.TaskStatuses
                              .Where(i => i.Id == id)
                              .Include(i => i.Tasks)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTaskStatusDeleted(itemToDelete);

            Context.TaskStatuses.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTaskStatusDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnTaskStatusGet(Models.Crm.TaskStatus item);

        public async Task<Models.Crm.TaskStatus> GetTaskStatusById(int? id)
        {
            var items = Context.TaskStatuses
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnTaskStatusGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Crm.TaskStatus> CancelTaskStatusChanges(Models.Crm.TaskStatus item)
        {
            var entityToCancel = Context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnTaskStatusUpdated(Models.Crm.TaskStatus item);
        partial void OnAfterTaskStatusUpdated(Models.Crm.TaskStatus item);

        public async Task<Models.Crm.TaskStatus> UpdateTaskStatus(int? id, Models.Crm.TaskStatus taskStatus)
        {
            OnTaskStatusUpdated(taskStatus);

            var itemToUpdate = Context.TaskStatuses
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(taskStatus);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterTaskStatusUpdated(taskStatus);

            return taskStatus;
        }

        partial void OnTaskTypeDeleted(Models.Crm.TaskType item);
        partial void OnAfterTaskTypeDeleted(Models.Crm.TaskType item);

        public async Task<Models.Crm.TaskType> DeleteTaskType(int? id)
        {
            var itemToDelete = Context.TaskTypes
                              .Where(i => i.Id == id)
                              .Include(i => i.Tasks)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTaskTypeDeleted(itemToDelete);

            Context.TaskTypes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTaskTypeDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnTaskTypeGet(Models.Crm.TaskType item);

        public async Task<Models.Crm.TaskType> GetTaskTypeById(int? id)
        {
            var items = Context.TaskTypes
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnTaskTypeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Crm.TaskType> CancelTaskTypeChanges(Models.Crm.TaskType item)
        {
            var entityToCancel = Context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnTaskTypeUpdated(Models.Crm.TaskType item);
        partial void OnAfterTaskTypeUpdated(Models.Crm.TaskType item);

        public async Task<Models.Crm.TaskType> UpdateTaskType(int? id, Models.Crm.TaskType taskType)
        {
            OnTaskTypeUpdated(taskType);

            var itemToUpdate = Context.TaskTypes
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(taskType);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterTaskTypeUpdated(taskType);

            return taskType;
        }
    }
}
