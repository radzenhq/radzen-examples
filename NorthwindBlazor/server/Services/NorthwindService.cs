using System;
using System.Web;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using NorthwindBlazor.Data;
using NorthwindBlazor.Models.Northwind;

namespace NorthwindBlazor
{
    public partial class NorthwindService 
    {

      public NorthwindService(NorthwindContext context)
      {
        this.context = context;
      }

      public NorthwindContext context { get; set; }

        
      partial void OnAlphabeticalListOfProductsRead(ref IQueryable<Models.Northwind.AlphabeticalListOfProduct> items);

      public async Task<IEnumerable<AlphabeticalListOfProduct>> GetAlphabeticalListOfProducts()
      {
        var items = context.AlphabeticalListOfProducts.AsNoTracking().AsQueryable<Models.Northwind.AlphabeticalListOfProduct>();

        OnAlphabeticalListOfProductsRead(ref items);

        return await Task.FromResult(
          items
        );
      }
            
      partial void OnCategoriesRead(ref IQueryable<Models.Northwind.Category> items);

      public async Task<IEnumerable<Category>> GetCategories()
      {
        var items = context.Categories.AsQueryable<Models.Northwind.Category>();

        OnCategoriesRead(ref items);

        return await Task.FromResult(
          items
        );
      }
    
      public async Task<Category> CreateCategory(Category category)
      {
        try
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }
        return category;
      }
    

      partial void OnCategoryDeleted(Models.Northwind.Category item);        

      public async Task<Category> DeleteCategory(int? categoryId)
      {
        var item = context.Categories
          .Where(i => i.CategoryID == categoryId)
          .Include(i => i.Products)
          .FirstOrDefault();

        try
        {
            OnCategoryDeleted(item);
            context.Categories.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return item;
      }
    
      public async Task<Category> GetCategoryByCategoryId(int? categoryId)
      {
        return await Task.FromResult(context.Categories.Find(categoryId));
      }
    
      
      partial void OnCategoryUpdated(Models.Northwind.Category item);

      public async Task<Category> UpdateCategory(int? categoryId, Category category)
      {
        try
        {
          OnCategoryUpdated(category);
          context.Categories.Update(category);
          context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return category;
      }
            
      partial void OnCategorySalesFor1997sRead(ref IQueryable<Models.Northwind.CategorySalesFor1997> items);

      public async Task<IEnumerable<CategorySalesFor1997>> GetCategorySalesFor1997S()
      {
        var items = context.CategorySalesFor1997s.AsNoTracking().AsQueryable<Models.Northwind.CategorySalesFor1997>();

        OnCategorySalesFor1997sRead(ref items);

        return await Task.FromResult(
          items
        );
      }
            
      partial void OnCurrentProductListsRead(ref IQueryable<Models.Northwind.CurrentProductList> items);

      public async Task<IEnumerable<CurrentProductList>> GetCurrentProductLists()
      {
        var items = context.CurrentProductLists.AsNoTracking().AsQueryable<Models.Northwind.CurrentProductList>();

        OnCurrentProductListsRead(ref items);

        return await Task.FromResult(
          items
        );
      }
            
      public async Task<IQueryable<Models.Northwind.CustOrderHist>> GetCustOrderHists(string CustomerID)
      {
          OnCustOrderHistsDefaultParams(ref CustomerID);
  
          var items = context.CustOrderHists.FromSqlRaw("EXEC [dbo].[CustOrderHist] @CustomerID={0}", CustomerID);
  
          OnCustOrderHistsInvoke(ref items);
  
          return await Task.FromResult(items);
      }
  
      partial void OnCustOrderHistsDefaultParams(ref string CustomerID);
  
      partial void OnCustOrderHistsInvoke(ref IQueryable<Models.Northwind.CustOrderHist> items);            
            
      public async Task<IQueryable<Models.Northwind.CustOrdersDetail>> GetCustOrdersDetails(int? OrderID)
      {
          OnCustOrdersDetailsDefaultParams(ref OrderID);
  
          var items = context.CustOrdersDetails.FromSqlRaw("EXEC [dbo].[CustOrdersDetail] @OrderID={0}", OrderID);
  
          OnCustOrdersDetailsInvoke(ref items);
  
          return await Task.FromResult(items);
      }
  
      partial void OnCustOrdersDetailsDefaultParams(ref int? OrderID);
  
      partial void OnCustOrdersDetailsInvoke(ref IQueryable<Models.Northwind.CustOrdersDetail> items);            
            
      public async Task<IQueryable<Models.Northwind.CustOrdersOrder>> GetCustOrdersOrders(string CustomerID)
      {
          OnCustOrdersOrdersDefaultParams(ref CustomerID);
  
          var items = context.CustOrdersOrders.FromSqlRaw("EXEC [dbo].[CustOrdersOrders] @CustomerID={0}", CustomerID);
  
          OnCustOrdersOrdersInvoke(ref items);
  
          return await Task.FromResult(items);
      }
  
      partial void OnCustOrdersOrdersDefaultParams(ref string CustomerID);
  
      partial void OnCustOrdersOrdersInvoke(ref IQueryable<Models.Northwind.CustOrdersOrder> items);            
            
      partial void OnCustomersRead(ref IQueryable<Models.Northwind.Customer> items);

      public async Task<IEnumerable<Customer>> GetCustomers()
      {
        var items = context.Customers.AsQueryable<Models.Northwind.Customer>();

        OnCustomersRead(ref items);

        return await Task.FromResult(
          items
        );
      }
    
      public async Task<Customer> CreateCustomer(Customer customer)
      {
        try
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }
        return customer;
      }
    

      partial void OnCustomerDeleted(Models.Northwind.Customer item);        

      public async Task<Customer> DeleteCustomer(string customerId)
      {
        var item = context.Customers
          .Where(i => i.CustomerID == customerId)
          .Include(i => i.Orders)
          .Include(i => i.CustomerCustomerDemos)
          .FirstOrDefault();

        try
        {
            OnCustomerDeleted(item);
            context.Customers.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return item;
      }
    
      public async Task<Customer> GetCustomerByCustomerId(string customerId)
      {
        return await Task.FromResult(context.Customers.Find(customerId));
      }
    
      
      partial void OnCustomerUpdated(Models.Northwind.Customer item);

      public async Task<Customer> UpdateCustomer(string customerId, Customer customer)
      {
        try
        {
          OnCustomerUpdated(customer);
          context.Customers.Update(customer);
          context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return customer;
      }
            
      partial void OnCustomerAndSuppliersByCitiesRead(ref IQueryable<Models.Northwind.CustomerAndSuppliersByCity> items);

      public async Task<IEnumerable<CustomerAndSuppliersByCity>> GetCustomerAndSuppliersByCities()
      {
        var items = context.CustomerAndSuppliersByCities.AsNoTracking().AsQueryable<Models.Northwind.CustomerAndSuppliersByCity>();

        OnCustomerAndSuppliersByCitiesRead(ref items);

        return await Task.FromResult(
          items
        );
      }
            
      partial void OnCustomerCustomerDemosRead(ref IQueryable<Models.Northwind.CustomerCustomerDemo> items);

      public async Task<IEnumerable<CustomerCustomerDemo>> GetCustomerCustomerDemos()
      {
        var items = context.CustomerCustomerDemos.AsQueryable<Models.Northwind.CustomerCustomerDemo>();

        OnCustomerCustomerDemosRead(ref items);

        return await Task.FromResult(
          items
          .Include(i => i.Customer)
          .Include(i => i.CustomerDemographic)
        );
      }
    
      public async Task<CustomerCustomerDemo> CreateCustomerCustomerDemo(CustomerCustomerDemo customerCustomerDemo)
      {
        try
        {
            context.CustomerCustomerDemos.Add(customerCustomerDemo);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }
        return customerCustomerDemo;
      }
            
      partial void OnCustomerDemographicsRead(ref IQueryable<Models.Northwind.CustomerDemographic> items);

      public async Task<IEnumerable<CustomerDemographic>> GetCustomerDemographics()
      {
        var items = context.CustomerDemographics.AsQueryable<Models.Northwind.CustomerDemographic>();

        OnCustomerDemographicsRead(ref items);

        return await Task.FromResult(
          items
        );
      }
    
      public async Task<CustomerDemographic> CreateCustomerDemographic(CustomerDemographic customerDemographic)
      {
        try
        {
            context.CustomerDemographics.Add(customerDemographic);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }
        return customerDemographic;
      }
    

      partial void OnCustomerDemographicDeleted(Models.Northwind.CustomerDemographic item);        

      public async Task<CustomerDemographic> DeleteCustomerDemographic(string customerTypeId)
      {
        var item = context.CustomerDemographics
          .Where(i => i.CustomerTypeID == customerTypeId)
          .Include(i => i.CustomerCustomerDemos)
          .FirstOrDefault();

        try
        {
            OnCustomerDemographicDeleted(item);
            context.CustomerDemographics.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return item;
      }
    
      public async Task<CustomerDemographic> GetCustomerDemographicByCustomerTypeId(string customerTypeId)
      {
        return await Task.FromResult(context.CustomerDemographics.Find(customerTypeId));
      }
    
      
      partial void OnCustomerDemographicUpdated(Models.Northwind.CustomerDemographic item);

      public async Task<CustomerDemographic> UpdateCustomerDemographic(string customerTypeId, CustomerDemographic customerDemographic)
      {
        try
        {
          OnCustomerDemographicUpdated(customerDemographic);
          context.CustomerDemographics.Update(customerDemographic);
          context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return customerDemographic;
      }
            
      partial void OnEmployeesRead(ref IQueryable<Models.Northwind.Employee> items);

      public async Task<IEnumerable<Employee>> GetEmployees()
      {
        var items = context.Employees.AsQueryable<Models.Northwind.Employee>();

        OnEmployeesRead(ref items);

        return await Task.FromResult(
          items
          .Include(i => i.Employee1)
        );
      }
    
      public async Task<Employee> CreateEmployee(Employee employee)
      {
        try
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }
        return employee;
      }
    

      partial void OnEmployeeDeleted(Models.Northwind.Employee item);        

      public async Task<Employee> DeleteEmployee(int? employeeId)
      {
        var item = context.Employees
          .Where(i => i.EmployeeID == employeeId)
          .Include(i => i.Orders)
          .Include(i => i.Employees1)
          .Include(i => i.EmployeeTerritories)
          .FirstOrDefault();

        try
        {
            OnEmployeeDeleted(item);
            context.Employees.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return item;
      }
    
      public async Task<Employee> GetEmployeeByEmployeeId(int? employeeId)
      {
        return await Task.FromResult(context.Employees.Find(employeeId));
      }
    
      
      partial void OnEmployeeUpdated(Models.Northwind.Employee item);

      public async Task<Employee> UpdateEmployee(int? employeeId, Employee employee)
      {
        try
        {
          OnEmployeeUpdated(employee);
          context.Employees.Update(employee);
          context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return employee;
      }
            
      public async Task<IQueryable<Models.Northwind.EmployeeSalesByCountry>> GetEmployeeSalesByCountries(string Beginning_Date, string Ending_Date)
      {
          OnEmployeeSalesByCountriesDefaultParams(ref Beginning_Date, ref Ending_Date);
  
          var items = context.EmployeeSalesByCountries.FromSqlRaw("EXEC [dbo].[Employee Sales by Country] @Beginning_Date={0}, @Ending_Date={1}", string.IsNullOrEmpty(Beginning_Date) ? (DateTime?)null : DateTime.Parse(Beginning_Date, null, System.Globalization.DateTimeStyles.RoundtripKind), string.IsNullOrEmpty(Ending_Date) ? (DateTime?)null : DateTime.Parse(Ending_Date, null, System.Globalization.DateTimeStyles.RoundtripKind));
  
          OnEmployeeSalesByCountriesInvoke(ref items);
  
          return await Task.FromResult(items);
      }
  
      partial void OnEmployeeSalesByCountriesDefaultParams(ref string Beginning_Date, ref string Ending_Date);
  
      partial void OnEmployeeSalesByCountriesInvoke(ref IQueryable<Models.Northwind.EmployeeSalesByCountry> items);            
            
      partial void OnEmployeeTerritoriesRead(ref IQueryable<Models.Northwind.EmployeeTerritory> items);

      public async Task<IEnumerable<EmployeeTerritory>> GetEmployeeTerritories()
      {
        var items = context.EmployeeTerritories.AsQueryable<Models.Northwind.EmployeeTerritory>();

        OnEmployeeTerritoriesRead(ref items);

        return await Task.FromResult(
          items
          .Include(i => i.Employee)
          .Include(i => i.Territory)
        );
      }
    
      public async Task<EmployeeTerritory> CreateEmployeeTerritory(EmployeeTerritory employeeTerritory)
      {
        try
        {
            context.EmployeeTerritories.Add(employeeTerritory);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }
        return employeeTerritory;
      }
                  public async Task<Models.Northwind.GetProductDetailResult> GetProductDetails(int? ProductId)
      {
          OnGetProductDetailsDefaultParams(ref ProductId);
  
          SqlParameter[] @params =
          {
              new SqlParameter("@returnVal", SqlDbType.Int) {Direction = ParameterDirection.Output},
              new SqlParameter("@ProductId", SqlDbType.Int) {Direction = ParameterDirection.Input, Value = ProductId},
              new SqlParameter("@ProductName", SqlDbType.VarChar, 100) {Direction = ParameterDirection.Output},
              new SqlParameter("@UnitPrice", SqlDbType.Decimal) {Direction = ParameterDirection.Output},
              new SqlParameter("@QuantityPerUnit", SqlDbType.VarChar, 20) {Direction = ParameterDirection.Output},
          };
          context.Database.ExecuteSqlRaw("EXEC @returnVal=[dbo].[GetProductDetails] @ProductId, @ProductName out, @UnitPrice out, @QuantityPerUnit out", @params);
  
          var result = new Models.Northwind.GetProductDetailResult();
          result.returnValue = Convert.ToInt32(@params[0].Value);
          result.ProductName = Convert.ToString(@params[2].Value);
          result.UnitPrice = Convert.ToDecimal(@params[3].Value);
          result.QuantityPerUnit = Convert.ToString(@params[4].Value);
  
          OnGetProductDetailsInvoke(ref result);
  
          return await Task.FromResult(result);
      }
  
      partial void OnGetProductDetailsDefaultParams(ref int? ProductId);
      partial void OnGetProductDetailsInvoke(ref Models.Northwind.GetProductDetailResult result);
            
      partial void OnInvoicesRead(ref IQueryable<Models.Northwind.Invoice> items);

      public async Task<IEnumerable<Invoice>> GetInvoices()
      {
        var items = context.Invoices.AsNoTracking().AsQueryable<Models.Northwind.Invoice>();

        OnInvoicesRead(ref items);

        return await Task.FromResult(
          items
        );
      }
            
      partial void OnOrdersRead(ref IQueryable<Models.Northwind.Order> items);

      public async Task<IEnumerable<Order>> GetOrders()
      {
        var items = context.Orders.AsQueryable<Models.Northwind.Order>();

        OnOrdersRead(ref items);

        return await Task.FromResult(
          items
          .Include(i => i.Customer)
          .Include(i => i.Employee)
          .Include(i => i.Shipper)
        );
      }
    
      public async Task<Order> CreateOrder(Order order)
      {
        try
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }
        return order;
      }
    

      partial void OnOrderDeleted(Models.Northwind.Order item);        

      public async Task<Order> DeleteOrder(int? orderId)
      {
        var item = context.Orders
          .Where(i => i.OrderID == orderId)
          .Include(i => i.OrderDetails)
          .FirstOrDefault();

        try
        {
            OnOrderDeleted(item);
            context.Orders.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return item;
      }
    
      public async Task<Order> GetOrderByOrderId(int? orderId)
      {
        return await Task.FromResult(context.Orders.Find(orderId));
      }
    
      
      partial void OnOrderUpdated(Models.Northwind.Order item);

      public async Task<Order> UpdateOrder(int? orderId, Order order)
      {
        try
        {
          OnOrderUpdated(order);
          context.Orders.Update(order);
          context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return order;
      }
            
      partial void OnOrderDetailsRead(ref IQueryable<Models.Northwind.OrderDetail> items);

      public async Task<IEnumerable<OrderDetail>> GetOrderDetails()
      {
        var items = context.OrderDetails.AsQueryable<Models.Northwind.OrderDetail>();

        OnOrderDetailsRead(ref items);

        return await Task.FromResult(
          items
          .Include(i => i.Order)
          .Include(i => i.Product)
        );
      }
    
      public async Task<OrderDetail> CreateOrderDetail(OrderDetail orderDetail)
      {
        try
        {
            context.OrderDetails.Add(orderDetail);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }
        return orderDetail;
      }
            
      partial void OnOrderDetailsExtendedsRead(ref IQueryable<Models.Northwind.OrderDetailsExtended> items);

      public async Task<IEnumerable<OrderDetailsExtended>> GetOrderDetailsExtendeds()
      {
        var items = context.OrderDetailsExtendeds.AsNoTracking().AsQueryable<Models.Northwind.OrderDetailsExtended>();

        OnOrderDetailsExtendedsRead(ref items);

        return await Task.FromResult(
          items
        );
      }
            
      partial void OnOrderSubtotalsRead(ref IQueryable<Models.Northwind.OrderSubtotal> items);

      public async Task<IEnumerable<OrderSubtotal>> GetOrderSubtotals()
      {
        var items = context.OrderSubtotals.AsNoTracking().AsQueryable<Models.Northwind.OrderSubtotal>();

        OnOrderSubtotalsRead(ref items);

        return await Task.FromResult(
          items
        );
      }
            
      partial void OnOrdersQriesRead(ref IQueryable<Models.Northwind.OrdersQry> items);

      public async Task<IEnumerable<OrdersQry>> GetOrdersQries()
      {
        var items = context.OrdersQries.AsNoTracking().AsQueryable<Models.Northwind.OrdersQry>();

        OnOrdersQriesRead(ref items);

        return await Task.FromResult(
          items
        );
      }
            
      partial void OnProductsRead(ref IQueryable<Models.Northwind.Product> items);

      public async Task<IEnumerable<Product>> GetProducts()
      {
        var items = context.Products.AsQueryable<Models.Northwind.Product>();

        OnProductsRead(ref items);

        return await Task.FromResult(
          items
          .Include(i => i.Supplier)
          .Include(i => i.Category)
        );
      }
    
      public async Task<Product> CreateProduct(Product product)
      {
        try
        {
            context.Products.Add(product);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }
        return product;
      }
    

      partial void OnProductDeleted(Models.Northwind.Product item);        

      public async Task<Product> DeleteProduct(int? productId)
      {
        var item = context.Products
          .Where(i => i.ProductID == productId)
          .Include(i => i.OrderDetails)
          .FirstOrDefault();

        try
        {
            OnProductDeleted(item);
            context.Products.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return item;
      }
    
      public async Task<Product> GetProductByProductId(int? productId)
      {
        return await Task.FromResult(context.Products.Find(productId));
      }
    
      
      partial void OnProductUpdated(Models.Northwind.Product item);

      public async Task<Product> UpdateProduct(int? productId, Product product)
      {
        try
        {
          OnProductUpdated(product);
          context.Products.Update(product);
          context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return product;
      }
            
      partial void OnProductSalesFor1997sRead(ref IQueryable<Models.Northwind.ProductSalesFor1997> items);

      public async Task<IEnumerable<ProductSalesFor1997>> GetProductSalesFor1997S()
      {
        var items = context.ProductSalesFor1997s.AsNoTracking().AsQueryable<Models.Northwind.ProductSalesFor1997>();

        OnProductSalesFor1997sRead(ref items);

        return await Task.FromResult(
          items
        );
      }
            
      partial void OnProductsAboveAveragePricesRead(ref IQueryable<Models.Northwind.ProductsAboveAveragePrice> items);

      public async Task<IEnumerable<ProductsAboveAveragePrice>> GetProductsAboveAveragePrices()
      {
        var items = context.ProductsAboveAveragePrices.AsNoTracking().AsQueryable<Models.Northwind.ProductsAboveAveragePrice>();

        OnProductsAboveAveragePricesRead(ref items);

        return await Task.FromResult(
          items
        );
      }
            
      partial void OnProductsByCategoriesRead(ref IQueryable<Models.Northwind.ProductsByCategory> items);

      public async Task<IEnumerable<ProductsByCategory>> GetProductsByCategories()
      {
        var items = context.ProductsByCategories.AsNoTracking().AsQueryable<Models.Northwind.ProductsByCategory>();

        OnProductsByCategoriesRead(ref items);

        return await Task.FromResult(
          items
        );
      }
            
      partial void OnQuarterlyOrdersRead(ref IQueryable<Models.Northwind.QuarterlyOrder> items);

      public async Task<IEnumerable<QuarterlyOrder>> GetQuarterlyOrders()
      {
        var items = context.QuarterlyOrders.AsNoTracking().AsQueryable<Models.Northwind.QuarterlyOrder>();

        OnQuarterlyOrdersRead(ref items);

        return await Task.FromResult(
          items
        );
      }
            
      partial void OnRegionsRead(ref IQueryable<Models.Northwind.Region> items);

      public async Task<IEnumerable<Region>> GetRegions()
      {
        var items = context.Regions.AsQueryable<Models.Northwind.Region>();

        OnRegionsRead(ref items);

        return await Task.FromResult(
          items
        );
      }
    
      public async Task<Region> CreateRegion(Region region)
      {
        try
        {
            context.Regions.Add(region);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }
        return region;
      }
    

      partial void OnRegionDeleted(Models.Northwind.Region item);        

      public async Task<Region> DeleteRegion(int? regionId)
      {
        var item = context.Regions
          .Where(i => i.RegionID == regionId)
          .Include(i => i.Territories)
          .FirstOrDefault();

        try
        {
            OnRegionDeleted(item);
            context.Regions.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return item;
      }
    
      public async Task<Region> GetRegionByRegionId(int? regionId)
      {
        return await Task.FromResult(context.Regions.Find(regionId));
      }
    
      
      partial void OnRegionUpdated(Models.Northwind.Region item);

      public async Task<Region> UpdateRegion(int? regionId, Region region)
      {
        try
        {
          OnRegionUpdated(region);
          context.Regions.Update(region);
          context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return region;
      }
            
      partial void OnRolePermissionsRead(ref IQueryable<Models.Northwind.RolePermission> items);

      public async Task<IEnumerable<RolePermission>> GetRolePermissions()
      {
        var items = context.RolePermissions.AsQueryable<Models.Northwind.RolePermission>();

        OnRolePermissionsRead(ref items);

        return await Task.FromResult(
          items
        );
      }
    
      public async Task<RolePermission> CreateRolePermission(RolePermission rolePermission)
      {
        try
        {
            context.RolePermissions.Add(rolePermission);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }
        return rolePermission;
      }
            
      partial void OnSalesByCategoriesRead(ref IQueryable<Models.Northwind.SalesByCategory> items);

      public async Task<IEnumerable<SalesByCategory>> GetSalesByCategories()
      {
        var items = context.SalesByCategories.AsNoTracking().AsQueryable<Models.Northwind.SalesByCategory>();

        OnSalesByCategoriesRead(ref items);

        return await Task.FromResult(
          items
        );
      }
            
      public async Task<IQueryable<Models.Northwind.SalesByCategory1>> GetSalesByCategory1S(string CategoryName, string OrdYear)
      {
          OnSalesByCategory1sDefaultParams(ref CategoryName, ref OrdYear);
  
          var items = context.SalesByCategory1s.FromSqlRaw("EXEC [dbo].[SalesByCategory] @CategoryName={0}, @OrdYear={1}", CategoryName, OrdYear);
  
          OnSalesByCategory1sInvoke(ref items);
  
          return await Task.FromResult(items);
      }
  
      partial void OnSalesByCategory1sDefaultParams(ref string CategoryName, ref string OrdYear);
  
      partial void OnSalesByCategory1sInvoke(ref IQueryable<Models.Northwind.SalesByCategory1> items);            
            
      public async Task<IQueryable<Models.Northwind.SalesByYear>> GetSalesByYears(string Beginning_Date, string Ending_Date)
      {
          OnSalesByYearsDefaultParams(ref Beginning_Date, ref Ending_Date);
  
          var items = context.SalesByYears.FromSqlRaw("EXEC [dbo].[Sales by Year] @Beginning_Date={0}, @Ending_Date={1}", string.IsNullOrEmpty(Beginning_Date) ? (DateTime?)null : DateTime.Parse(Beginning_Date, null, System.Globalization.DateTimeStyles.RoundtripKind), string.IsNullOrEmpty(Ending_Date) ? (DateTime?)null : DateTime.Parse(Ending_Date, null, System.Globalization.DateTimeStyles.RoundtripKind));
  
          OnSalesByYearsInvoke(ref items);
  
          return await Task.FromResult(items);
      }
  
      partial void OnSalesByYearsDefaultParams(ref string Beginning_Date, ref string Ending_Date);
  
      partial void OnSalesByYearsInvoke(ref IQueryable<Models.Northwind.SalesByYear> items);            
            
      partial void OnSalesTotalsByAmountsRead(ref IQueryable<Models.Northwind.SalesTotalsByAmount> items);

      public async Task<IEnumerable<SalesTotalsByAmount>> GetSalesTotalsByAmounts()
      {
        var items = context.SalesTotalsByAmounts.AsNoTracking().AsQueryable<Models.Northwind.SalesTotalsByAmount>();

        OnSalesTotalsByAmountsRead(ref items);

        return await Task.FromResult(
          items
        );
      }
            
      partial void OnShippersRead(ref IQueryable<Models.Northwind.Shipper> items);

      public async Task<IEnumerable<Shipper>> GetShippers()
      {
        var items = context.Shippers.AsQueryable<Models.Northwind.Shipper>();

        OnShippersRead(ref items);

        return await Task.FromResult(
          items
        );
      }
    
      public async Task<Shipper> CreateShipper(Shipper shipper)
      {
        try
        {
            context.Shippers.Add(shipper);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }
        return shipper;
      }
    

      partial void OnShipperDeleted(Models.Northwind.Shipper item);        

      public async Task<Shipper> DeleteShipper(int? shipperId)
      {
        var item = context.Shippers
          .Where(i => i.ShipperID == shipperId)
          .Include(i => i.Orders)
          .FirstOrDefault();

        try
        {
            OnShipperDeleted(item);
            context.Shippers.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return item;
      }
    
      public async Task<Shipper> GetShipperByShipperId(int? shipperId)
      {
        return await Task.FromResult(context.Shippers.Find(shipperId));
      }
    
      
      partial void OnShipperUpdated(Models.Northwind.Shipper item);

      public async Task<Shipper> UpdateShipper(int? shipperId, Shipper shipper)
      {
        try
        {
          OnShipperUpdated(shipper);
          context.Shippers.Update(shipper);
          context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return shipper;
      }
            
      partial void OnSummaryOfSalesByQuartersRead(ref IQueryable<Models.Northwind.SummaryOfSalesByQuarter> items);

      public async Task<IEnumerable<SummaryOfSalesByQuarter>> GetSummaryOfSalesByQuarters()
      {
        var items = context.SummaryOfSalesByQuarters.AsNoTracking().AsQueryable<Models.Northwind.SummaryOfSalesByQuarter>();

        OnSummaryOfSalesByQuartersRead(ref items);

        return await Task.FromResult(
          items
        );
      }
            
      partial void OnSummaryOfSalesByYearsRead(ref IQueryable<Models.Northwind.SummaryOfSalesByYear> items);

      public async Task<IEnumerable<SummaryOfSalesByYear>> GetSummaryOfSalesByYears()
      {
        var items = context.SummaryOfSalesByYears.AsNoTracking().AsQueryable<Models.Northwind.SummaryOfSalesByYear>();

        OnSummaryOfSalesByYearsRead(ref items);

        return await Task.FromResult(
          items
        );
      }
            
      partial void OnSuppliersRead(ref IQueryable<Models.Northwind.Supplier> items);

      public async Task<IEnumerable<Supplier>> GetSuppliers()
      {
        var items = context.Suppliers.AsQueryable<Models.Northwind.Supplier>();

        OnSuppliersRead(ref items);

        return await Task.FromResult(
          items
        );
      }
    
      public async Task<Supplier> CreateSupplier(Supplier supplier)
      {
        try
        {
            context.Suppliers.Add(supplier);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }
        return supplier;
      }
    

      partial void OnSupplierDeleted(Models.Northwind.Supplier item);        

      public async Task<Supplier> DeleteSupplier(int? supplierId)
      {
        var item = context.Suppliers
          .Where(i => i.SupplierID == supplierId)
          .Include(i => i.Products)
          .FirstOrDefault();

        try
        {
            OnSupplierDeleted(item);
            context.Suppliers.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return item;
      }
    
      public async Task<Supplier> GetSupplierBySupplierId(int? supplierId)
      {
        return await Task.FromResult(context.Suppliers.Find(supplierId));
      }
    
      
      partial void OnSupplierUpdated(Models.Northwind.Supplier item);

      public async Task<Supplier> UpdateSupplier(int? supplierId, Supplier supplier)
      {
        try
        {
          OnSupplierUpdated(supplier);
          context.Suppliers.Update(supplier);
          context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return supplier;
      }
            
      public async Task<IQueryable<Models.Northwind.TenMostExpensiveProduct>> GetTenMostExpensiveProducts()
      {
          var items = context.TenMostExpensiveProducts.FromSqlRaw("EXEC [dbo].[Ten Most Expensive Products]");
  
          OnTenMostExpensiveProductsInvoke(ref items);
  
          return await Task.FromResult(items);
      }
  
      partial void OnTenMostExpensiveProductsInvoke(ref IQueryable<Models.Northwind.TenMostExpensiveProduct> items);
            
      partial void OnTerritoriesRead(ref IQueryable<Models.Northwind.Territory> items);

      public async Task<IEnumerable<Territory>> GetTerritories()
      {
        var items = context.Territories.AsQueryable<Models.Northwind.Territory>();

        OnTerritoriesRead(ref items);

        return await Task.FromResult(
          items
          .Include(i => i.Region)
        );
      }
    
      public async Task<Territory> CreateTerritory(Territory territory)
      {
        try
        {
            context.Territories.Add(territory);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }
        return territory;
      }
    

      partial void OnTerritoryDeleted(Models.Northwind.Territory item);        

      public async Task<Territory> DeleteTerritory(string territoryId)
      {
        var item = context.Territories
          .Where(i => i.TerritoryID == territoryId)
          .Include(i => i.EmployeeTerritories)
          .FirstOrDefault();

        try
        {
            OnTerritoryDeleted(item);
            context.Territories.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return item;
      }
    
      public async Task<Territory> GetTerritoryByTerritoryId(string territoryId)
      {
        return await Task.FromResult(context.Territories.Find(territoryId));
      }
    
      
      partial void OnTerritoryUpdated(Models.Northwind.Territory item);

      public async Task<Territory> UpdateTerritory(string territoryId, Territory territory)
      {
        try
        {
          OnTerritoryUpdated(territory);
          context.Territories.Update(territory);
          context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return territory;
      }
            

      partial void OnCustomerCustomerDemoDeleted(Models.Northwind.CustomerCustomerDemo item);        

      public async Task<CustomerCustomerDemo> DeleteCustomerCustomerDemo(string customerId, string customerTypeId)
      {
        var item = context.CustomerCustomerDemos
          .Where(i => i.CustomerID == customerId && i.CustomerTypeID == customerTypeId)
          .FirstOrDefault();

        try
        {
            OnCustomerCustomerDemoDeleted(item);
            context.CustomerCustomerDemos.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return item;
      }
    
      public async Task<CustomerCustomerDemo> GetCustomerCustomerDemoByCustomerIdAndCustomerTypeId(string customerId, string customerTypeId)
      {
        return await Task.FromResult(context.CustomerCustomerDemos.Find(customerId, customerTypeId));
      }
    
      
      partial void OnCustomerCustomerDemoUpdated(Models.Northwind.CustomerCustomerDemo item);

      public async Task<CustomerCustomerDemo> UpdateCustomerCustomerDemo(string customerId, string customerTypeId, CustomerCustomerDemo customerCustomerDemo)
      {
        try
        {
          OnCustomerCustomerDemoUpdated(customerCustomerDemo);
          context.CustomerCustomerDemos.Update(customerCustomerDemo);
          context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return customerCustomerDemo;
      }
            

      partial void OnEmployeeTerritoryDeleted(Models.Northwind.EmployeeTerritory item);        

      public async Task<EmployeeTerritory> DeleteEmployeeTerritory(int? employeeId, string territoryId)
      {
        var item = context.EmployeeTerritories
          .Where(i => i.EmployeeID == employeeId && i.TerritoryID == territoryId)
          .FirstOrDefault();

        try
        {
            OnEmployeeTerritoryDeleted(item);
            context.EmployeeTerritories.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return item;
      }
    
      public async Task<EmployeeTerritory> GetEmployeeTerritoryByEmployeeIdAndTerritoryId(int? employeeId, string territoryId)
      {
        return await Task.FromResult(context.EmployeeTerritories.Find(employeeId, territoryId));
      }
    
      
      partial void OnEmployeeTerritoryUpdated(Models.Northwind.EmployeeTerritory item);

      public async Task<EmployeeTerritory> UpdateEmployeeTerritory(int? employeeId, string territoryId, EmployeeTerritory employeeTerritory)
      {
        try
        {
          OnEmployeeTerritoryUpdated(employeeTerritory);
          context.EmployeeTerritories.Update(employeeTerritory);
          context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return employeeTerritory;
      }
            

      partial void OnOrderDetailDeleted(Models.Northwind.OrderDetail item);        

      public async Task<OrderDetail> DeleteOrderDetail(int? orderId, int? productId)
      {
        var item = context.OrderDetails
          .Where(i => i.OrderID == orderId && i.ProductID == productId)
          .FirstOrDefault();

        try
        {
            OnOrderDetailDeleted(item);
            context.OrderDetails.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return item;
      }
    
      public async Task<OrderDetail> GetOrderDetailByOrderIdAndProductId(int? orderId, int? productId)
      {
        return await Task.FromResult(context.OrderDetails.Find(orderId, productId));
      }
    
      
      partial void OnOrderDetailUpdated(Models.Northwind.OrderDetail item);

      public async Task<OrderDetail> UpdateOrderDetail(int? orderId, int? productId, OrderDetail orderDetail)
      {
        try
        {
          OnOrderDetailUpdated(orderDetail);
          context.OrderDetails.Update(orderDetail);
          context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return orderDetail;
      }
            

      partial void OnRolePermissionDeleted(Models.Northwind.RolePermission item);        

      public async Task<RolePermission> DeleteRolePermission(string roleName, string permissionId)
      {
        var item = context.RolePermissions
          .Where(i => i.RoleName == roleName && i.PermissionId == permissionId)
          .FirstOrDefault();

        try
        {
            OnRolePermissionDeleted(item);
            context.RolePermissions.Remove(item);
            context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return item;
      }
    
      public async Task<RolePermission> GetRolePermissionByRoleNameAndPermissionId(string roleName, string permissionId)
      {
        return await Task.FromResult(context.RolePermissions.Find(roleName, permissionId));
      }
    
      
      partial void OnRolePermissionUpdated(Models.Northwind.RolePermission item);

      public async Task<RolePermission> UpdateRolePermission(string roleName, string permissionId, RolePermission rolePermission)
      {
        try
        {
          OnRolePermissionUpdated(rolePermission);
          context.RolePermissions.Update(rolePermission);
          context.SaveChanges();
        }
        catch(Exception ex) 
        {
            return null;
        }

        return rolePermission;
      }
        
  }
}
