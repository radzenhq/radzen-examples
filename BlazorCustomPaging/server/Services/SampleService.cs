using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using BlazorCustomPaging.Data;

namespace BlazorCustomPaging
{
    public partial class SampleService
    {
        private readonly SampleContext context;
        private readonly NavigationManager navigationManager;

        public SampleService(SampleContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public async Task ExportOrdersToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/sample/orders/excel") : "export/sample/orders/excel", true);
        }

        public async Task ExportOrdersToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/sample/orders/csv") : "export/sample/orders/csv", true);
        }

        partial void OnOrdersRead(ref IQueryable<Models.Sample.Order> items);

        public async Task<IQueryable<Models.Sample.Order>> GetOrders(Query query = null)
        {
            var items = context.Orders.AsQueryable();

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

            OnOrdersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOrderCreated(Models.Sample.Order item);

        public async Task<Models.Sample.Order> CreateOrder(Models.Sample.Order order)
        {
            OnOrderCreated(order);

            context.Orders.Add(order);
            context.SaveChanges();

            return order;
        }
        public async Task ExportOrderDetailsToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/sample/orderdetails/excel") : "export/sample/orderdetails/excel", true);
        }

        public async Task ExportOrderDetailsToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/sample/orderdetails/csv") : "export/sample/orderdetails/csv", true);
        }

        partial void OnOrderDetailsRead(ref IQueryable<Models.Sample.OrderDetail> items);

        public async Task<IQueryable<Models.Sample.OrderDetail>> GetOrderDetails(Query query = null)
        {
            var items = context.OrderDetails.AsQueryable();

            items = items.Include(i => i.Order);

            items = items.Include(i => i.Product);

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

            OnOrderDetailsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOrderDetailCreated(Models.Sample.OrderDetail item);

        public async Task<Models.Sample.OrderDetail> CreateOrderDetail(Models.Sample.OrderDetail orderDetail)
        {
            OnOrderDetailCreated(orderDetail);

            context.OrderDetails.Add(orderDetail);
            context.SaveChanges();

            return orderDetail;
        }
        public async Task ExportProductsToExcel(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/sample/products/excel") : "export/sample/products/excel", true);
        }

        public async Task ExportProductsToCSV(Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl("export/sample/products/csv") : "export/sample/products/csv", true);
        }

        partial void OnProductsRead(ref IQueryable<Models.Sample.Product> items);

        public async Task<IQueryable<Models.Sample.Product>> GetProducts(Query query = null)
        {
            var items = context.Products.AsQueryable();

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

            OnProductsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProductCreated(Models.Sample.Product item);

        public async Task<Models.Sample.Product> CreateProduct(Models.Sample.Product product)
        {
            OnProductCreated(product);

            context.Products.Add(product);
            context.SaveChanges();

            return product;
        }

        partial void OnOrderDeleted(Models.Sample.Order item);

        public async Task<Models.Sample.Order> DeleteOrder(int? id)
        {
            var item = context.Orders
                              .Where(i => i.Id == id)
                              .Include(i => i.OrderDetails)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOrderDeleted(item);

            context.Orders.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnOrderGet(Models.Sample.Order item);

        public async Task<Models.Sample.Order> GetOrderById(int? id)
        {
            var items = context.Orders
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var item = items.FirstOrDefault();

            OnOrderGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.Sample.Order> CancelOrderChanges(Models.Sample.Order item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnOrderUpdated(Models.Sample.Order item);

        public async Task<Models.Sample.Order> UpdateOrder(int? id, Models.Sample.Order order)
        {
            OnOrderUpdated(order);

            var item = context.Orders
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(order);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return order;
        }

        partial void OnOrderDetailDeleted(Models.Sample.OrderDetail item);

        public async Task<Models.Sample.OrderDetail> DeleteOrderDetail(int? id)
        {
            var item = context.OrderDetails
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOrderDetailDeleted(item);

            context.OrderDetails.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnOrderDetailGet(Models.Sample.OrderDetail item);

        public async Task<Models.Sample.OrderDetail> GetOrderDetailById(int? id)
        {
            var items = context.OrderDetails
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Order);

            items = items.Include(i => i.Product);

            var item = items.FirstOrDefault();

            OnOrderDetailGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.Sample.OrderDetail> CancelOrderDetailChanges(Models.Sample.OrderDetail item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnOrderDetailUpdated(Models.Sample.OrderDetail item);

        public async Task<Models.Sample.OrderDetail> UpdateOrderDetail(int? id, Models.Sample.OrderDetail orderDetail)
        {
            OnOrderDetailUpdated(orderDetail);

            var item = context.OrderDetails
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(orderDetail);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return orderDetail;
        }

        partial void OnProductDeleted(Models.Sample.Product item);

        public async Task<Models.Sample.Product> DeleteProduct(int? id)
        {
            var item = context.Products
                              .Where(i => i.Id == id)
                              .Include(i => i.OrderDetails)
                              .FirstOrDefault();

            if (item == null)
            {
               throw new Exception("Item no longer available");
            }

            OnProductDeleted(item);

            context.Products.Remove(item);
            context.SaveChanges();

            return item;
        }

        partial void OnProductGet(Models.Sample.Product item);

        public async Task<Models.Sample.Product> GetProductById(int? id)
        {
            var items = context.Products
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var item = items.FirstOrDefault();

            OnProductGet(item);

            return await Task.FromResult(item);
        }

        public async Task<Models.Sample.Product> CancelProductChanges(Models.Sample.Product item)
        {
            var entity = context.Entry(item);
            entity.CurrentValues.SetValues(entity.OriginalValues);
            entity.State = EntityState.Unchanged;

            return item;
        }

        partial void OnProductUpdated(Models.Sample.Product item);

        public async Task<Models.Sample.Product> UpdateProduct(int? id, Models.Sample.Product product)
        {
            OnProductUpdated(product);

            var item = context.Products
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (item == null)
            {
               throw new Exception("Item no longer available");
            }
            var entry = context.Entry(item);
            entry.CurrentValues.SetValues(product);
            entry.State = EntityState.Modified;
            context.SaveChanges();

            return product;
        }
    }
}
