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
using TreeDemos.Data;

namespace TreeDemos
{
    public partial class NorthwindService
    {
        private readonly NorthwindContext context;
        private readonly NavigationManager navigationManager;

        public NorthwindService(NorthwindContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public async Task ExportCategoriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/categories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/categories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCategoriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/categories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/categories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCategoriesRead(ref IQueryable<Models.Northwind.Category> items);

        public async Task<IQueryable<Models.Northwind.Category>> GetCategories(Query query = null)
        {
            var items = context.Categories.AsQueryable();

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

            OnCategoriesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCategoryCreated(Models.Northwind.Category item);
        partial void OnAfterCategoryCreated(Models.Northwind.Category item);

        public async Task<Models.Northwind.Category> CreateCategory(Models.Northwind.Category category)
        {
            OnCategoryCreated(category);

            context.Categories.Add(category);
            context.SaveChanges();

            OnAfterCategoryCreated(category);

            return category;
        }
        public async Task ExportCustomersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/customers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/customers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCustomersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/customers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/customers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCustomersRead(ref IQueryable<Models.Northwind.Customer> items);

        public async Task<IQueryable<Models.Northwind.Customer>> GetCustomers(Query query = null)
        {
            var items = context.Customers.AsQueryable();

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

            OnCustomersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCustomerCreated(Models.Northwind.Customer item);
        partial void OnAfterCustomerCreated(Models.Northwind.Customer item);

        public async Task<Models.Northwind.Customer> CreateCustomer(Models.Northwind.Customer customer)
        {
            OnCustomerCreated(customer);

            context.Customers.Add(customer);
            context.SaveChanges();

            OnAfterCustomerCreated(customer);

            return customer;
        }
        public async Task ExportCustomerCustomerDemosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/customercustomerdemos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/customercustomerdemos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCustomerCustomerDemosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/customercustomerdemos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/customercustomerdemos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCustomerCustomerDemosRead(ref IQueryable<Models.Northwind.CustomerCustomerDemo> items);

        public async Task<IQueryable<Models.Northwind.CustomerCustomerDemo>> GetCustomerCustomerDemos(Query query = null)
        {
            var items = context.CustomerCustomerDemos.AsQueryable();

            items = items.Include(i => i.Customer);

            items = items.Include(i => i.CustomerDemographic);

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

            OnCustomerCustomerDemosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCustomerCustomerDemoCreated(Models.Northwind.CustomerCustomerDemo item);
        partial void OnAfterCustomerCustomerDemoCreated(Models.Northwind.CustomerCustomerDemo item);

        public async Task<Models.Northwind.CustomerCustomerDemo> CreateCustomerCustomerDemo(Models.Northwind.CustomerCustomerDemo customerCustomerDemo)
        {
            OnCustomerCustomerDemoCreated(customerCustomerDemo);

            context.CustomerCustomerDemos.Add(customerCustomerDemo);
            context.SaveChanges();

            OnAfterCustomerCustomerDemoCreated(customerCustomerDemo);

            return customerCustomerDemo;
        }
        public async Task ExportCustomerDemographicsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/customerdemographics/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/customerdemographics/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCustomerDemographicsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/customerdemographics/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/customerdemographics/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCustomerDemographicsRead(ref IQueryable<Models.Northwind.CustomerDemographic> items);

        public async Task<IQueryable<Models.Northwind.CustomerDemographic>> GetCustomerDemographics(Query query = null)
        {
            var items = context.CustomerDemographics.AsQueryable();

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

            OnCustomerDemographicsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCustomerDemographicCreated(Models.Northwind.CustomerDemographic item);
        partial void OnAfterCustomerDemographicCreated(Models.Northwind.CustomerDemographic item);

        public async Task<Models.Northwind.CustomerDemographic> CreateCustomerDemographic(Models.Northwind.CustomerDemographic customerDemographic)
        {
            OnCustomerDemographicCreated(customerDemographic);

            context.CustomerDemographics.Add(customerDemographic);
            context.SaveChanges();

            OnAfterCustomerDemographicCreated(customerDemographic);

            return customerDemographic;
        }
        public async Task ExportEmployeesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/employees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/employees/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEmployeesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/employees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/employees/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEmployeesRead(ref IQueryable<Models.Northwind.Employee> items);

        public async Task<IQueryable<Models.Northwind.Employee>> GetEmployees(Query query = null)
        {
            var items = context.Employees.AsQueryable();

            items = items.Include(i => i.Employee1);

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

            OnEmployeesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEmployeeCreated(Models.Northwind.Employee item);
        partial void OnAfterEmployeeCreated(Models.Northwind.Employee item);

        public async Task<Models.Northwind.Employee> CreateEmployee(Models.Northwind.Employee employee)
        {
            OnEmployeeCreated(employee);

            context.Employees.Add(employee);
            context.SaveChanges();

            OnAfterEmployeeCreated(employee);

            return employee;
        }
        public async Task ExportEmployeeTerritoriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/employeeterritories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/employeeterritories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEmployeeTerritoriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/employeeterritories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/employeeterritories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEmployeeTerritoriesRead(ref IQueryable<Models.Northwind.EmployeeTerritory> items);

        public async Task<IQueryable<Models.Northwind.EmployeeTerritory>> GetEmployeeTerritories(Query query = null)
        {
            var items = context.EmployeeTerritories.AsQueryable();

            items = items.Include(i => i.Employee);

            items = items.Include(i => i.Territory);

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

            OnEmployeeTerritoriesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEmployeeTerritoryCreated(Models.Northwind.EmployeeTerritory item);
        partial void OnAfterEmployeeTerritoryCreated(Models.Northwind.EmployeeTerritory item);

        public async Task<Models.Northwind.EmployeeTerritory> CreateEmployeeTerritory(Models.Northwind.EmployeeTerritory employeeTerritory)
        {
            OnEmployeeTerritoryCreated(employeeTerritory);

            context.EmployeeTerritories.Add(employeeTerritory);
            context.SaveChanges();

            OnAfterEmployeeTerritoryCreated(employeeTerritory);

            return employeeTerritory;
        }
        public async Task ExportOrdersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/orders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/orders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportOrdersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/orders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/orders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnOrdersRead(ref IQueryable<Models.Northwind.Order> items);

        public async Task<IQueryable<Models.Northwind.Order>> GetOrders(Query query = null)
        {
            var items = context.Orders.AsQueryable();

            items = items.Include(i => i.Customer);

            items = items.Include(i => i.Employee);

            items = items.Include(i => i.Shipper);

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

            OnOrdersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOrderCreated(Models.Northwind.Order item);
        partial void OnAfterOrderCreated(Models.Northwind.Order item);

        public async Task<Models.Northwind.Order> CreateOrder(Models.Northwind.Order order)
        {
            OnOrderCreated(order);

            context.Orders.Add(order);
            context.SaveChanges();

            OnAfterOrderCreated(order);

            return order;
        }
        public async Task ExportOrderDetailsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/orderdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/orderdetails/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportOrderDetailsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/orderdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/orderdetails/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnOrderDetailsRead(ref IQueryable<Models.Northwind.OrderDetail> items);

        public async Task<IQueryable<Models.Northwind.OrderDetail>> GetOrderDetails(Query query = null)
        {
            var items = context.OrderDetails.AsQueryable();

            items = items.Include(i => i.Order);

            items = items.Include(i => i.Product);

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

            OnOrderDetailsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOrderDetailCreated(Models.Northwind.OrderDetail item);
        partial void OnAfterOrderDetailCreated(Models.Northwind.OrderDetail item);

        public async Task<Models.Northwind.OrderDetail> CreateOrderDetail(Models.Northwind.OrderDetail orderDetail)
        {
            OnOrderDetailCreated(orderDetail);

            context.OrderDetails.Add(orderDetail);
            context.SaveChanges();

            OnAfterOrderDetailCreated(orderDetail);

            return orderDetail;
        }
        public async Task ExportProductsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/products/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/products/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportProductsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/products/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/products/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnProductsRead(ref IQueryable<Models.Northwind.Product> items);

        public async Task<IQueryable<Models.Northwind.Product>> GetProducts(Query query = null)
        {
            var items = context.Products.AsQueryable();

            items = items.Include(i => i.Supplier);

            items = items.Include(i => i.Category);

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

            OnProductsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProductCreated(Models.Northwind.Product item);
        partial void OnAfterProductCreated(Models.Northwind.Product item);

        public async Task<Models.Northwind.Product> CreateProduct(Models.Northwind.Product product)
        {
            OnProductCreated(product);

            context.Products.Add(product);
            context.SaveChanges();

            OnAfterProductCreated(product);

            return product;
        }
        public async Task ExportRegionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/regions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/regions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportRegionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/regions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/regions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnRegionsRead(ref IQueryable<Models.Northwind.Region> items);

        public async Task<IQueryable<Models.Northwind.Region>> GetRegions(Query query = null)
        {
            var items = context.Regions.AsQueryable();

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

            OnRegionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnRegionCreated(Models.Northwind.Region item);
        partial void OnAfterRegionCreated(Models.Northwind.Region item);

        public async Task<Models.Northwind.Region> CreateRegion(Models.Northwind.Region region)
        {
            OnRegionCreated(region);

            context.Regions.Add(region);
            context.SaveChanges();

            OnAfterRegionCreated(region);

            return region;
        }
        public async Task ExportShippersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/shippers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/shippers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportShippersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/shippers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/shippers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnShippersRead(ref IQueryable<Models.Northwind.Shipper> items);

        public async Task<IQueryable<Models.Northwind.Shipper>> GetShippers(Query query = null)
        {
            var items = context.Shippers.AsQueryable();

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

            OnShippersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnShipperCreated(Models.Northwind.Shipper item);
        partial void OnAfterShipperCreated(Models.Northwind.Shipper item);

        public async Task<Models.Northwind.Shipper> CreateShipper(Models.Northwind.Shipper shipper)
        {
            OnShipperCreated(shipper);

            context.Shippers.Add(shipper);
            context.SaveChanges();

            OnAfterShipperCreated(shipper);

            return shipper;
        }
        public async Task ExportSuppliersToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/suppliers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/suppliers/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSuppliersToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/suppliers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/suppliers/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSuppliersRead(ref IQueryable<Models.Northwind.Supplier> items);

        public async Task<IQueryable<Models.Northwind.Supplier>> GetSuppliers(Query query = null)
        {
            var items = context.Suppliers.AsQueryable();

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

            OnSuppliersRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSupplierCreated(Models.Northwind.Supplier item);
        partial void OnAfterSupplierCreated(Models.Northwind.Supplier item);

        public async Task<Models.Northwind.Supplier> CreateSupplier(Models.Northwind.Supplier supplier)
        {
            OnSupplierCreated(supplier);

            context.Suppliers.Add(supplier);
            context.SaveChanges();

            OnAfterSupplierCreated(supplier);

            return supplier;
        }
        public async Task ExportTerritoriesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/territories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/territories/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTerritoriesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/northwind/territories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/northwind/territories/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTerritoriesRead(ref IQueryable<Models.Northwind.Territory> items);

        public async Task<IQueryable<Models.Northwind.Territory>> GetTerritories(Query query = null)
        {
            var items = context.Territories.AsQueryable();

            items = items.Include(i => i.Region);

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

            OnTerritoriesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTerritoryCreated(Models.Northwind.Territory item);
        partial void OnAfterTerritoryCreated(Models.Northwind.Territory item);

        public async Task<Models.Northwind.Territory> CreateTerritory(Models.Northwind.Territory territory)
        {
            OnTerritoryCreated(territory);

            context.Territories.Add(territory);
            context.SaveChanges();

            OnAfterTerritoryCreated(territory);

            return territory;
        }

        partial void OnCategoryDeleted(Models.Northwind.Category item);
        partial void OnAfterCategoryDeleted(Models.Northwind.Category item);

        public async Task<Models.Northwind.Category> DeleteCategory(int? categoryId)
        {
            var itemToDelete = context.Categories
                              .Where(i => i.CategoryID == categoryId)
                              .Include(i => i.Products)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCategoryDeleted(itemToDelete);

            context.Categories.Remove(itemToDelete);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw ex;
            }

            OnAfterCategoryDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnCategoryGet(Models.Northwind.Category item);

        public async Task<Models.Northwind.Category> GetCategoryByCategoryId(int? categoryId)
        {
            var items = context.Categories
                              .AsNoTracking()
                              .Where(i => i.CategoryID == categoryId);

            var itemToReturn = items.FirstOrDefault();

            OnCategoryGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Northwind.Category> CancelCategoryChanges(Models.Northwind.Category item)
        {
            var entityToCancel = context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnCategoryUpdated(Models.Northwind.Category item);
        partial void OnAfterCategoryUpdated(Models.Northwind.Category item);

        public async Task<Models.Northwind.Category> UpdateCategory(int? categoryId, Models.Northwind.Category category)
        {
            OnCategoryUpdated(category);

            var itemToUpdate = context.Categories
                              .Where(i => i.CategoryID == categoryId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
            var entryToUpdate = context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(category);
            entryToUpdate.State = EntityState.Modified;
            context.SaveChanges();

            OnAfterCategoryUpdated(category);

            return category;
        }

        partial void OnCustomerDeleted(Models.Northwind.Customer item);
        partial void OnAfterCustomerDeleted(Models.Northwind.Customer item);

        public async Task<Models.Northwind.Customer> DeleteCustomer(string customerId)
        {
            var itemToDelete = context.Customers
                              .Where(i => i.CustomerID == customerId)
                              .Include(i => i.Orders)
                              .Include(i => i.CustomerCustomerDemos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCustomerDeleted(itemToDelete);

            context.Customers.Remove(itemToDelete);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw ex;
            }

            OnAfterCustomerDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnCustomerGet(Models.Northwind.Customer item);

        public async Task<Models.Northwind.Customer> GetCustomerByCustomerId(string customerId)
        {
            var items = context.Customers
                              .AsNoTracking()
                              .Where(i => i.CustomerID == customerId);

            var itemToReturn = items.FirstOrDefault();

            OnCustomerGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Northwind.Customer> CancelCustomerChanges(Models.Northwind.Customer item)
        {
            var entityToCancel = context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnCustomerUpdated(Models.Northwind.Customer item);
        partial void OnAfterCustomerUpdated(Models.Northwind.Customer item);

        public async Task<Models.Northwind.Customer> UpdateCustomer(string customerId, Models.Northwind.Customer customer)
        {
            OnCustomerUpdated(customer);

            var itemToUpdate = context.Customers
                              .Where(i => i.CustomerID == customerId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
            var entryToUpdate = context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(customer);
            entryToUpdate.State = EntityState.Modified;
            context.SaveChanges();

            OnAfterCustomerUpdated(customer);

            return customer;
        }

        partial void OnCustomerCustomerDemoDeleted(Models.Northwind.CustomerCustomerDemo item);
        partial void OnAfterCustomerCustomerDemoDeleted(Models.Northwind.CustomerCustomerDemo item);

        public async Task<Models.Northwind.CustomerCustomerDemo> DeleteCustomerCustomerDemo(string customerId, string customerTypeId)
        {
            var itemToDelete = context.CustomerCustomerDemos
                              .Where(i => i.CustomerID == customerId && i.CustomerTypeID == customerTypeId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCustomerCustomerDemoDeleted(itemToDelete);

            context.CustomerCustomerDemos.Remove(itemToDelete);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw ex;
            }

            OnAfterCustomerCustomerDemoDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnCustomerCustomerDemoGet(Models.Northwind.CustomerCustomerDemo item);

        public async Task<Models.Northwind.CustomerCustomerDemo> GetCustomerCustomerDemoByCustomerIdAndCustomerTypeId(string customerId, string customerTypeId)
        {
            var items = context.CustomerCustomerDemos
                              .AsNoTracking()
                              .Where(i => i.CustomerID == customerId && i.CustomerTypeID == customerTypeId);

            items = items.Include(i => i.Customer);

            items = items.Include(i => i.CustomerDemographic);

            var itemToReturn = items.FirstOrDefault();

            OnCustomerCustomerDemoGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Northwind.CustomerCustomerDemo> CancelCustomerCustomerDemoChanges(Models.Northwind.CustomerCustomerDemo item)
        {
            var entityToCancel = context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnCustomerCustomerDemoUpdated(Models.Northwind.CustomerCustomerDemo item);
        partial void OnAfterCustomerCustomerDemoUpdated(Models.Northwind.CustomerCustomerDemo item);

        public async Task<Models.Northwind.CustomerCustomerDemo> UpdateCustomerCustomerDemo(string customerId, string customerTypeId, Models.Northwind.CustomerCustomerDemo customerCustomerDemo)
        {
            OnCustomerCustomerDemoUpdated(customerCustomerDemo);

            var itemToUpdate = context.CustomerCustomerDemos
                              .Where(i => i.CustomerID == customerId && i.CustomerTypeID == customerTypeId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
            var entryToUpdate = context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(customerCustomerDemo);
            entryToUpdate.State = EntityState.Modified;
            context.SaveChanges();

            OnAfterCustomerCustomerDemoUpdated(customerCustomerDemo);

            return customerCustomerDemo;
        }

        partial void OnCustomerDemographicDeleted(Models.Northwind.CustomerDemographic item);
        partial void OnAfterCustomerDemographicDeleted(Models.Northwind.CustomerDemographic item);

        public async Task<Models.Northwind.CustomerDemographic> DeleteCustomerDemographic(string customerTypeId)
        {
            var itemToDelete = context.CustomerDemographics
                              .Where(i => i.CustomerTypeID == customerTypeId)
                              .Include(i => i.CustomerCustomerDemos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCustomerDemographicDeleted(itemToDelete);

            context.CustomerDemographics.Remove(itemToDelete);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw ex;
            }

            OnAfterCustomerDemographicDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnCustomerDemographicGet(Models.Northwind.CustomerDemographic item);

        public async Task<Models.Northwind.CustomerDemographic> GetCustomerDemographicByCustomerTypeId(string customerTypeId)
        {
            var items = context.CustomerDemographics
                              .AsNoTracking()
                              .Where(i => i.CustomerTypeID == customerTypeId);

            var itemToReturn = items.FirstOrDefault();

            OnCustomerDemographicGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Northwind.CustomerDemographic> CancelCustomerDemographicChanges(Models.Northwind.CustomerDemographic item)
        {
            var entityToCancel = context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnCustomerDemographicUpdated(Models.Northwind.CustomerDemographic item);
        partial void OnAfterCustomerDemographicUpdated(Models.Northwind.CustomerDemographic item);

        public async Task<Models.Northwind.CustomerDemographic> UpdateCustomerDemographic(string customerTypeId, Models.Northwind.CustomerDemographic customerDemographic)
        {
            OnCustomerDemographicUpdated(customerDemographic);

            var itemToUpdate = context.CustomerDemographics
                              .Where(i => i.CustomerTypeID == customerTypeId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
            var entryToUpdate = context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(customerDemographic);
            entryToUpdate.State = EntityState.Modified;
            context.SaveChanges();

            OnAfterCustomerDemographicUpdated(customerDemographic);

            return customerDemographic;
        }

        partial void OnEmployeeDeleted(Models.Northwind.Employee item);
        partial void OnAfterEmployeeDeleted(Models.Northwind.Employee item);

        public async Task<Models.Northwind.Employee> DeleteEmployee(int? employeeId)
        {
            var itemToDelete = context.Employees
                              .Where(i => i.EmployeeID == employeeId)
                              .Include(i => i.Employees1)
                              .Include(i => i.Orders)
                              .Include(i => i.EmployeeTerritories)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEmployeeDeleted(itemToDelete);

            context.Employees.Remove(itemToDelete);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw ex;
            }

            OnAfterEmployeeDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnEmployeeGet(Models.Northwind.Employee item);

        public async Task<Models.Northwind.Employee> GetEmployeeByEmployeeId(int? employeeId)
        {
            var items = context.Employees
                              .AsNoTracking()
                              .Where(i => i.EmployeeID == employeeId);

            items = items.Include(i => i.Employee1);

            var itemToReturn = items.FirstOrDefault();

            OnEmployeeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Northwind.Employee> CancelEmployeeChanges(Models.Northwind.Employee item)
        {
            var entityToCancel = context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnEmployeeUpdated(Models.Northwind.Employee item);
        partial void OnAfterEmployeeUpdated(Models.Northwind.Employee item);

        public async Task<Models.Northwind.Employee> UpdateEmployee(int? employeeId, Models.Northwind.Employee employee)
        {
            OnEmployeeUpdated(employee);

            var itemToUpdate = context.Employees
                              .Where(i => i.EmployeeID == employeeId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
            var entryToUpdate = context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(employee);
            entryToUpdate.State = EntityState.Modified;
            context.SaveChanges();

            OnAfterEmployeeUpdated(employee);

            return employee;
        }

        partial void OnEmployeeTerritoryDeleted(Models.Northwind.EmployeeTerritory item);
        partial void OnAfterEmployeeTerritoryDeleted(Models.Northwind.EmployeeTerritory item);

        public async Task<Models.Northwind.EmployeeTerritory> DeleteEmployeeTerritory(int? employeeId, string territoryId)
        {
            var itemToDelete = context.EmployeeTerritories
                              .Where(i => i.EmployeeID == employeeId && i.TerritoryID == territoryId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEmployeeTerritoryDeleted(itemToDelete);

            context.EmployeeTerritories.Remove(itemToDelete);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw ex;
            }

            OnAfterEmployeeTerritoryDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnEmployeeTerritoryGet(Models.Northwind.EmployeeTerritory item);

        public async Task<Models.Northwind.EmployeeTerritory> GetEmployeeTerritoryByEmployeeIdAndTerritoryId(int? employeeId, string territoryId)
        {
            var items = context.EmployeeTerritories
                              .AsNoTracking()
                              .Where(i => i.EmployeeID == employeeId && i.TerritoryID == territoryId);

            items = items.Include(i => i.Employee);

            items = items.Include(i => i.Territory);

            var itemToReturn = items.FirstOrDefault();

            OnEmployeeTerritoryGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Northwind.EmployeeTerritory> CancelEmployeeTerritoryChanges(Models.Northwind.EmployeeTerritory item)
        {
            var entityToCancel = context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnEmployeeTerritoryUpdated(Models.Northwind.EmployeeTerritory item);
        partial void OnAfterEmployeeTerritoryUpdated(Models.Northwind.EmployeeTerritory item);

        public async Task<Models.Northwind.EmployeeTerritory> UpdateEmployeeTerritory(int? employeeId, string territoryId, Models.Northwind.EmployeeTerritory employeeTerritory)
        {
            OnEmployeeTerritoryUpdated(employeeTerritory);

            var itemToUpdate = context.EmployeeTerritories
                              .Where(i => i.EmployeeID == employeeId && i.TerritoryID == territoryId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
            var entryToUpdate = context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(employeeTerritory);
            entryToUpdate.State = EntityState.Modified;
            context.SaveChanges();

            OnAfterEmployeeTerritoryUpdated(employeeTerritory);

            return employeeTerritory;
        }

        partial void OnOrderDeleted(Models.Northwind.Order item);
        partial void OnAfterOrderDeleted(Models.Northwind.Order item);

        public async Task<Models.Northwind.Order> DeleteOrder(int? orderId)
        {
            var itemToDelete = context.Orders
                              .Where(i => i.OrderID == orderId)
                              .Include(i => i.OrderDetails)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOrderDeleted(itemToDelete);

            context.Orders.Remove(itemToDelete);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw ex;
            }

            OnAfterOrderDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnOrderGet(Models.Northwind.Order item);

        public async Task<Models.Northwind.Order> GetOrderByOrderId(int? orderId)
        {
            var items = context.Orders
                              .AsNoTracking()
                              .Where(i => i.OrderID == orderId);

            items = items.Include(i => i.Customer);

            items = items.Include(i => i.Employee);

            items = items.Include(i => i.Shipper);

            var itemToReturn = items.FirstOrDefault();

            OnOrderGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Northwind.Order> CancelOrderChanges(Models.Northwind.Order item)
        {
            var entityToCancel = context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnOrderUpdated(Models.Northwind.Order item);
        partial void OnAfterOrderUpdated(Models.Northwind.Order item);

        public async Task<Models.Northwind.Order> UpdateOrder(int? orderId, Models.Northwind.Order order)
        {
            OnOrderUpdated(order);

            var itemToUpdate = context.Orders
                              .Where(i => i.OrderID == orderId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
            var entryToUpdate = context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(order);
            entryToUpdate.State = EntityState.Modified;
            context.SaveChanges();

            OnAfterOrderUpdated(order);

            return order;
        }

        partial void OnOrderDetailDeleted(Models.Northwind.OrderDetail item);
        partial void OnAfterOrderDetailDeleted(Models.Northwind.OrderDetail item);

        public async Task<Models.Northwind.OrderDetail> DeleteOrderDetail(int? orderId, int? productId)
        {
            var itemToDelete = context.OrderDetails
                              .Where(i => i.OrderID == orderId && i.ProductID == productId)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOrderDetailDeleted(itemToDelete);

            context.OrderDetails.Remove(itemToDelete);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw ex;
            }

            OnAfterOrderDetailDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnOrderDetailGet(Models.Northwind.OrderDetail item);

        public async Task<Models.Northwind.OrderDetail> GetOrderDetailByOrderIdAndProductId(int? orderId, int? productId)
        {
            var items = context.OrderDetails
                              .AsNoTracking()
                              .Where(i => i.OrderID == orderId && i.ProductID == productId);

            items = items.Include(i => i.Order);

            items = items.Include(i => i.Product);

            var itemToReturn = items.FirstOrDefault();

            OnOrderDetailGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Northwind.OrderDetail> CancelOrderDetailChanges(Models.Northwind.OrderDetail item)
        {
            var entityToCancel = context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnOrderDetailUpdated(Models.Northwind.OrderDetail item);
        partial void OnAfterOrderDetailUpdated(Models.Northwind.OrderDetail item);

        public async Task<Models.Northwind.OrderDetail> UpdateOrderDetail(int? orderId, int? productId, Models.Northwind.OrderDetail orderDetail)
        {
            OnOrderDetailUpdated(orderDetail);

            var itemToUpdate = context.OrderDetails
                              .Where(i => i.OrderID == orderId && i.ProductID == productId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
            var entryToUpdate = context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(orderDetail);
            entryToUpdate.State = EntityState.Modified;
            context.SaveChanges();

            OnAfterOrderDetailUpdated(orderDetail);

            return orderDetail;
        }

        partial void OnProductDeleted(Models.Northwind.Product item);
        partial void OnAfterProductDeleted(Models.Northwind.Product item);

        public async Task<Models.Northwind.Product> DeleteProduct(int? productId)
        {
            var itemToDelete = context.Products
                              .Where(i => i.ProductID == productId)
                              .Include(i => i.OrderDetails)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnProductDeleted(itemToDelete);

            context.Products.Remove(itemToDelete);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw ex;
            }

            OnAfterProductDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnProductGet(Models.Northwind.Product item);

        public async Task<Models.Northwind.Product> GetProductByProductId(int? productId)
        {
            var items = context.Products
                              .AsNoTracking()
                              .Where(i => i.ProductID == productId);

            items = items.Include(i => i.Supplier);

            items = items.Include(i => i.Category);

            var itemToReturn = items.FirstOrDefault();

            OnProductGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Northwind.Product> CancelProductChanges(Models.Northwind.Product item)
        {
            var entityToCancel = context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnProductUpdated(Models.Northwind.Product item);
        partial void OnAfterProductUpdated(Models.Northwind.Product item);

        public async Task<Models.Northwind.Product> UpdateProduct(int? productId, Models.Northwind.Product product)
        {
            OnProductUpdated(product);

            var itemToUpdate = context.Products
                              .Where(i => i.ProductID == productId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
            var entryToUpdate = context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(product);
            entryToUpdate.State = EntityState.Modified;
            context.SaveChanges();

            OnAfterProductUpdated(product);

            return product;
        }

        partial void OnRegionDeleted(Models.Northwind.Region item);
        partial void OnAfterRegionDeleted(Models.Northwind.Region item);

        public async Task<Models.Northwind.Region> DeleteRegion(int? regionId)
        {
            var itemToDelete = context.Regions
                              .Where(i => i.RegionID == regionId)
                              .Include(i => i.Territories)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnRegionDeleted(itemToDelete);

            context.Regions.Remove(itemToDelete);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw ex;
            }

            OnAfterRegionDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnRegionGet(Models.Northwind.Region item);

        public async Task<Models.Northwind.Region> GetRegionByRegionId(int? regionId)
        {
            var items = context.Regions
                              .AsNoTracking()
                              .Where(i => i.RegionID == regionId);

            var itemToReturn = items.FirstOrDefault();

            OnRegionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Northwind.Region> CancelRegionChanges(Models.Northwind.Region item)
        {
            var entityToCancel = context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnRegionUpdated(Models.Northwind.Region item);
        partial void OnAfterRegionUpdated(Models.Northwind.Region item);

        public async Task<Models.Northwind.Region> UpdateRegion(int? regionId, Models.Northwind.Region region)
        {
            OnRegionUpdated(region);

            var itemToUpdate = context.Regions
                              .Where(i => i.RegionID == regionId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
            var entryToUpdate = context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(region);
            entryToUpdate.State = EntityState.Modified;
            context.SaveChanges();

            OnAfterRegionUpdated(region);

            return region;
        }

        partial void OnShipperDeleted(Models.Northwind.Shipper item);
        partial void OnAfterShipperDeleted(Models.Northwind.Shipper item);

        public async Task<Models.Northwind.Shipper> DeleteShipper(int? shipperId)
        {
            var itemToDelete = context.Shippers
                              .Where(i => i.ShipperID == shipperId)
                              .Include(i => i.Orders)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnShipperDeleted(itemToDelete);

            context.Shippers.Remove(itemToDelete);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw ex;
            }

            OnAfterShipperDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnShipperGet(Models.Northwind.Shipper item);

        public async Task<Models.Northwind.Shipper> GetShipperByShipperId(int? shipperId)
        {
            var items = context.Shippers
                              .AsNoTracking()
                              .Where(i => i.ShipperID == shipperId);

            var itemToReturn = items.FirstOrDefault();

            OnShipperGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Northwind.Shipper> CancelShipperChanges(Models.Northwind.Shipper item)
        {
            var entityToCancel = context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnShipperUpdated(Models.Northwind.Shipper item);
        partial void OnAfterShipperUpdated(Models.Northwind.Shipper item);

        public async Task<Models.Northwind.Shipper> UpdateShipper(int? shipperId, Models.Northwind.Shipper shipper)
        {
            OnShipperUpdated(shipper);

            var itemToUpdate = context.Shippers
                              .Where(i => i.ShipperID == shipperId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
            var entryToUpdate = context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(shipper);
            entryToUpdate.State = EntityState.Modified;
            context.SaveChanges();

            OnAfterShipperUpdated(shipper);

            return shipper;
        }

        partial void OnSupplierDeleted(Models.Northwind.Supplier item);
        partial void OnAfterSupplierDeleted(Models.Northwind.Supplier item);

        public async Task<Models.Northwind.Supplier> DeleteSupplier(int? supplierId)
        {
            var itemToDelete = context.Suppliers
                              .Where(i => i.SupplierID == supplierId)
                              .Include(i => i.Products)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSupplierDeleted(itemToDelete);

            context.Suppliers.Remove(itemToDelete);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw ex;
            }

            OnAfterSupplierDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnSupplierGet(Models.Northwind.Supplier item);

        public async Task<Models.Northwind.Supplier> GetSupplierBySupplierId(int? supplierId)
        {
            var items = context.Suppliers
                              .AsNoTracking()
                              .Where(i => i.SupplierID == supplierId);

            var itemToReturn = items.FirstOrDefault();

            OnSupplierGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Northwind.Supplier> CancelSupplierChanges(Models.Northwind.Supplier item)
        {
            var entityToCancel = context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnSupplierUpdated(Models.Northwind.Supplier item);
        partial void OnAfterSupplierUpdated(Models.Northwind.Supplier item);

        public async Task<Models.Northwind.Supplier> UpdateSupplier(int? supplierId, Models.Northwind.Supplier supplier)
        {
            OnSupplierUpdated(supplier);

            var itemToUpdate = context.Suppliers
                              .Where(i => i.SupplierID == supplierId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
            var entryToUpdate = context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(supplier);
            entryToUpdate.State = EntityState.Modified;
            context.SaveChanges();

            OnAfterSupplierUpdated(supplier);

            return supplier;
        }

        partial void OnTerritoryDeleted(Models.Northwind.Territory item);
        partial void OnAfterTerritoryDeleted(Models.Northwind.Territory item);

        public async Task<Models.Northwind.Territory> DeleteTerritory(string territoryId)
        {
            var itemToDelete = context.Territories
                              .Where(i => i.TerritoryID == territoryId)
                              .Include(i => i.EmployeeTerritories)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTerritoryDeleted(itemToDelete);

            context.Territories.Remove(itemToDelete);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw ex;
            }

            OnAfterTerritoryDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnTerritoryGet(Models.Northwind.Territory item);

        public async Task<Models.Northwind.Territory> GetTerritoryByTerritoryId(string territoryId)
        {
            var items = context.Territories
                              .AsNoTracking()
                              .Where(i => i.TerritoryID == territoryId);

            items = items.Include(i => i.Region);

            var itemToReturn = items.FirstOrDefault();

            OnTerritoryGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.Northwind.Territory> CancelTerritoryChanges(Models.Northwind.Territory item)
        {
            var entityToCancel = context.Entry(item);
            entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
            entityToCancel.State = EntityState.Unchanged;

            return item;
        }

        partial void OnTerritoryUpdated(Models.Northwind.Territory item);
        partial void OnAfterTerritoryUpdated(Models.Northwind.Territory item);

        public async Task<Models.Northwind.Territory> UpdateTerritory(string territoryId, Models.Northwind.Territory territory)
        {
            OnTerritoryUpdated(territory);

            var itemToUpdate = context.Territories
                              .Where(i => i.TerritoryID == territoryId)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
            var entryToUpdate = context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(territory);
            entryToUpdate.State = EntityState.Modified;
            context.SaveChanges();

            OnAfterTerritoryUpdated(territory);

            return territory;
        }
    }
}
