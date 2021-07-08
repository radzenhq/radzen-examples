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
using CustomSecurity.Data;

namespace CustomSecurity
{
    public partial class CustomSecurityDbService
    {
        CustomSecurityDbContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly CustomSecurityDbContext context;
        private readonly NavigationManager navigationManager;

        public CustomSecurityDbService(CustomSecurityDbContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public async Task ExportRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/customsecuritydb/roles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/customsecuritydb/roles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/customsecuritydb/roles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/customsecuritydb/roles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRolesRead(ref IQueryable<Models.CustomSecurityDb.Role> items);

        public async Task<IQueryable<Models.CustomSecurityDb.Role>> GetRoles(Query query = null)
        {
            var items = Context.Roles.AsQueryable();

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

            OnRolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRoleCreated(Models.CustomSecurityDb.Role item);
        partial void OnAfterRoleCreated(Models.CustomSecurityDb.Role item);

        public async Task<Models.CustomSecurityDb.Role> CreateRole(Models.CustomSecurityDb.Role role)
        {
            OnRoleCreated(role);

            Context.Roles.Add(role);
            Context.SaveChanges();

            OnAfterRoleCreated(role);

            return role;
        }
        public async Task ExportUsersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/customsecuritydb/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/customsecuritydb/users/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUsersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/customsecuritydb/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/customsecuritydb/users/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUsersRead(ref IQueryable<Models.CustomSecurityDb.User> items);

        public async Task<IQueryable<Models.CustomSecurityDb.User>> GetUsers(Query query = null)
        {
            var items = Context.Users.AsQueryable();

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

            OnUsersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUserCreated(Models.CustomSecurityDb.User item);
        partial void OnAfterUserCreated(Models.CustomSecurityDb.User item);

        public async Task<Models.CustomSecurityDb.User> CreateUser(Models.CustomSecurityDb.User user)
        {
            OnUserCreated(user);

            Context.Users.Add(user);
            Context.SaveChanges();

            OnAfterUserCreated(user);

            return user;
        }
        public async Task ExportUserRolesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/customsecuritydb/userroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/customsecuritydb/userroles/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUserRolesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/customsecuritydb/userroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/customsecuritydb/userroles/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUserRolesRead(ref IQueryable<Models.CustomSecurityDb.UserRole> items);

        public async Task<IQueryable<Models.CustomSecurityDb.UserRole>> GetUserRoles(Query query = null)
        {
            var items = Context.UserRoles.AsQueryable();

            items = items.Include(i => i.User);

            items = items.Include(i => i.Role);

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

            OnUserRolesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUserRoleCreated(Models.CustomSecurityDb.UserRole item);
        partial void OnAfterUserRoleCreated(Models.CustomSecurityDb.UserRole item);

        public async Task<Models.CustomSecurityDb.UserRole> CreateUserRole(Models.CustomSecurityDb.UserRole userRole)
        {
            OnUserRoleCreated(userRole);

            Context.UserRoles.Add(userRole);
            Context.SaveChanges();

            OnAfterUserRoleCreated(userRole);

            return userRole;
        }

        partial void OnRoleDeleted(Models.CustomSecurityDb.Role item);
        partial void OnAfterRoleDeleted(Models.CustomSecurityDb.Role item);

        public async Task<Models.CustomSecurityDb.Role> DeleteRole(int? id)
        {
            var itemToDelete = Context.Roles
                              .Where(i => i.Id == id)
                              .Include(i => i.UserRoles)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRoleDeleted(itemToDelete);

            Context.Roles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterRoleDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnRoleGet(Models.CustomSecurityDb.Role item);

        public async Task<Models.CustomSecurityDb.Role> GetRoleById(int? id)
        {
            var items = Context.Roles
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnRoleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.CustomSecurityDb.Role> CancelRoleChanges(Models.CustomSecurityDb.Role item)
        {
            var entityToCancel = Context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnRoleUpdated(Models.CustomSecurityDb.Role item);
        partial void OnAfterRoleUpdated(Models.CustomSecurityDb.Role item);

        public async Task<Models.CustomSecurityDb.Role> UpdateRole(int? id, Models.CustomSecurityDb.Role role)
        {
            OnRoleUpdated(role);

            var itemToUpdate = Context.Roles
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(role);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();

            OnAfterRoleUpdated(role);

            return role;
        }

        partial void OnUserDeleted(Models.CustomSecurityDb.User item);
        partial void OnAfterUserDeleted(Models.CustomSecurityDb.User item);

        public async Task<Models.CustomSecurityDb.User> DeleteUser(int? id)
        {
            var itemToDelete = Context.Users
                              .Where(i => i.Id == id)
                              .Include(i => i.UserRoles)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUserDeleted(itemToDelete);

            Context.Users.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUserDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnUserGet(Models.CustomSecurityDb.User item);

        public async Task<Models.CustomSecurityDb.User> GetUserById(int? id)
        {
            var items = Context.Users
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnUserGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.CustomSecurityDb.User> CancelUserChanges(Models.CustomSecurityDb.User item)
        {
            var entityToCancel = Context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnUserUpdated(Models.CustomSecurityDb.User item);
        partial void OnAfterUserUpdated(Models.CustomSecurityDb.User item);

        public async Task<Models.CustomSecurityDb.User> UpdateUser(int? id, Models.CustomSecurityDb.User user)
        {
            OnUserUpdated(user);

            var itemToUpdate = Context.Users
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(user);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();

            OnAfterUserUpdated(user);

            return user;
        }

        partial void OnUserRoleDeleted(Models.CustomSecurityDb.UserRole item);
        partial void OnAfterUserRoleDeleted(Models.CustomSecurityDb.UserRole item);

        public async Task<Models.CustomSecurityDb.UserRole> DeleteUserRole(int? userId, int? roleId)
        {
            var itemToDelete = Context.UserRoles
                              .Where(i => i.UserId == userId && i.RoleId == roleId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUserRoleDeleted(itemToDelete);

            Context.UserRoles.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUserRoleDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnUserRoleGet(Models.CustomSecurityDb.UserRole item);

        public async Task<Models.CustomSecurityDb.UserRole> GetUserRoleByUserIdAndRoleId(int? userId, int? roleId)
        {
            var items = Context.UserRoles
                              .AsNoTracking()
                              .Where(i => i.UserId == userId && i.RoleId == roleId);

            items = items.Include(i => i.User);

            items = items.Include(i => i.Role);

            var itemToReturn = items.FirstOrDefault();

            OnUserRoleGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.CustomSecurityDb.UserRole> CancelUserRoleChanges(Models.CustomSecurityDb.UserRole item)
        {
            var entityToCancel = Context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnUserRoleUpdated(Models.CustomSecurityDb.UserRole item);
        partial void OnAfterUserRoleUpdated(Models.CustomSecurityDb.UserRole item);

        public async Task<Models.CustomSecurityDb.UserRole> UpdateUserRole(int? userId, int? roleId, Models.CustomSecurityDb.UserRole userRole)
        {
            OnUserRoleUpdated(userRole);

            var itemToUpdate = Context.UserRoles
                              .Where(i => i.UserId == userId && i.RoleId == roleId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(userRole);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();

            OnAfterUserRoleUpdated(userRole);

            return userRole;
        }
    }
}
