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
using UploadFilesBlazor.Data;

namespace UploadFilesBlazor
{
    public partial class UploadDbService
    {
        UploadDbContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly UploadDbContext context;
        private readonly NavigationManager navigationManager;

        public UploadDbService(UploadDbContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public async Task ExportFilesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/uploaddb/files/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/uploaddb/files/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportFilesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/uploaddb/files/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/uploaddb/files/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnFilesRead(ref IQueryable<Models.UploadDb.File> items);

        public async Task<IQueryable<Models.UploadDb.File>> GetFiles(Query query = null)
        {
            var items = Context.Files.AsQueryable();

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

            OnFilesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnFileCreated(Models.UploadDb.File item);
        partial void OnAfterFileCreated(Models.UploadDb.File item);

        public async Task<Models.UploadDb.File> CreateFile(Models.UploadDb.File file)
        {
            OnFileCreated(file);

            Context.Files.Add(file);
            Context.SaveChanges();

            OnAfterFileCreated(file);

            return file;
        }

        partial void OnFileDeleted(Models.UploadDb.File item);
        partial void OnAfterFileDeleted(Models.UploadDb.File item);

        public async Task<Models.UploadDb.File> DeleteFile(int? id)
        {
            var itemToDelete = Context.Files
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnFileDeleted(itemToDelete);

            Context.Files.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterFileDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnFileGet(Models.UploadDb.File item);

        public async Task<Models.UploadDb.File> GetFileById(int? id)
        {
            var items = Context.Files
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnFileGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.UploadDb.File> CancelFileChanges(Models.UploadDb.File item)
        {
            var entityToCancel = Context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnFileUpdated(Models.UploadDb.File item);
        partial void OnAfterFileUpdated(Models.UploadDb.File item);

        public async Task<Models.UploadDb.File> UpdateFile(int? id, Models.UploadDb.File file)
        {
            OnFileUpdated(file);

            var itemToUpdate = Context.Files
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(file);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterFileUpdated(file);

            return file;
        }
    }
}
