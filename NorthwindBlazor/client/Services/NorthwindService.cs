using System;
using System.Web;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using NorthwindBlazor.Models.Northwind;

namespace NorthwindBlazor.App
{
    public class NorthwindService 
    {
      public string BasePath { get; set; }

      public ODataService OData { get; set; }

      public NorthwindService()
      {
        OData = new ODataService();
      }
        
      public async Task<ODataServiceResult<Category>> GetCategories(string filter, int? top, int? skip, string orderby, bool? count, string expand, string format, string select)
      {
        return await OData.Get<Category>(new ODataServiceArgs() { Url = $"{BasePath}odata/Northwind/Categories", Filter = filter, Top = top, Skip = skip, Orderby = orderby, Count = count, Expand = expand, Format = format, Select = select });
      }
    
      public async Task<Category> CreateCategory(Category category)
      {
        return await OData.Add($"{BasePath}odata/Northwind/Categories", category);
      }
    
      public async Task<HttpResponseMessage> DeleteCategory(int? categoryId)
      {
        return await OData.Delete($"{BasePath}odata/Northwind/Categories({categoryId})");
      }
    
      public async Task<Category> GetCategoryByCategoryId(int? categoryId)
      {
        return await OData.GetBy<Category>(new ODataServiceItemArgs() { Url = $"{BasePath}odata/Northwind/Categories({categoryId})" });
      }
    
      public async Task<HttpResponseMessage> UpdateCategory(int? categoryId, Category category)
      {
        return await OData.Update($"{BasePath}odata/Northwind/Categories({categoryId})", category);
      }
            
      public async Task<ODataServiceResult<Customer>> GetCustomers(string filter, int? top, int? skip, string orderby, bool? count, string expand, string format, string select)
      {
        return await OData.Get<Customer>(new ODataServiceArgs() { Url = $"{BasePath}odata/Northwind/Customers", Filter = filter, Top = top, Skip = skip, Orderby = orderby, Count = count, Expand = expand, Format = format, Select = select });
      }
    
      public async Task<Customer> CreateCustomer(Customer customer)
      {
        return await OData.Add($"{BasePath}odata/Northwind/Customers", customer);
      }
    
      public async Task<HttpResponseMessage> DeleteCustomer(string customerId)
      {
        return await OData.Delete($"{BasePath}odata/Northwind/Customers('{HttpUtility.UrlEncode(customerId)}')");
      }
    
      public async Task<Customer> GetCustomerByCustomerId(string customerId)
      {
        return await OData.GetBy<Customer>(new ODataServiceItemArgs() { Url = $"{BasePath}odata/Northwind/Customers('{HttpUtility.UrlEncode(customerId)}')" });
      }
    
      public async Task<HttpResponseMessage> UpdateCustomer(string customerId, Customer customer)
      {
        return await OData.Update($"{BasePath}odata/Northwind/Customers('{HttpUtility.UrlEncode(customerId)}')", customer);
      }
            
      public async Task<ODataServiceResult<CustomerCustomerDemo>> GetCustomerCustomerDemos(string filter, int? top, int? skip, string orderby, bool? count, string expand, string format, string select)
      {
        return await OData.Get<CustomerCustomerDemo>(new ODataServiceArgs() { Url = $"{BasePath}odata/Northwind/CustomerCustomerDemos", Filter = filter, Top = top, Skip = skip, Orderby = orderby, Count = count, Expand = expand, Format = format, Select = select });
      }
    
      public async Task<CustomerCustomerDemo> CreateCustomerCustomerDemo(CustomerCustomerDemo customerCustomerDemo)
      {
        return await OData.Add($"{BasePath}odata/Northwind/CustomerCustomerDemos", customerCustomerDemo);
      }
            
      public async Task<ODataServiceResult<CustomerDemographic>> GetCustomerDemographics(string filter, int? top, int? skip, string orderby, bool? count, string expand, string format, string select)
      {
        return await OData.Get<CustomerDemographic>(new ODataServiceArgs() { Url = $"{BasePath}odata/Northwind/CustomerDemographics", Filter = filter, Top = top, Skip = skip, Orderby = orderby, Count = count, Expand = expand, Format = format, Select = select });
      }
    
      public async Task<CustomerDemographic> CreateCustomerDemographic(CustomerDemographic customerDemographic)
      {
        return await OData.Add($"{BasePath}odata/Northwind/CustomerDemographics", customerDemographic);
      }
    
      public async Task<HttpResponseMessage> DeleteCustomerDemographic(string customerTypeId)
      {
        return await OData.Delete($"{BasePath}odata/Northwind/CustomerDemographics('{HttpUtility.UrlEncode(customerTypeId)}')");
      }
    
      public async Task<CustomerDemographic> GetCustomerDemographicByCustomerTypeId(string customerTypeId)
      {
        return await OData.GetBy<CustomerDemographic>(new ODataServiceItemArgs() { Url = $"{BasePath}odata/Northwind/CustomerDemographics('{HttpUtility.UrlEncode(customerTypeId)}')" });
      }
    
      public async Task<HttpResponseMessage> UpdateCustomerDemographic(string customerTypeId, CustomerDemographic customerDemographic)
      {
        return await OData.Update($"{BasePath}odata/Northwind/CustomerDemographics('{HttpUtility.UrlEncode(customerTypeId)}')", customerDemographic);
      }
            
      public async Task<ODataServiceResult<Employee>> GetEmployees(string filter, int? top, int? skip, string orderby, bool? count, string expand, string format, string select)
      {
        return await OData.Get<Employee>(new ODataServiceArgs() { Url = $"{BasePath}odata/Northwind/Employees", Filter = filter, Top = top, Skip = skip, Orderby = orderby, Count = count, Expand = expand, Format = format, Select = select });
      }
    
      public async Task<Employee> CreateEmployee(Employee employee)
      {
        return await OData.Add($"{BasePath}odata/Northwind/Employees", employee);
      }
    
      public async Task<HttpResponseMessage> DeleteEmployee(int? employeeId)
      {
        return await OData.Delete($"{BasePath}odata/Northwind/Employees({employeeId})");
      }
    
      public async Task<Employee> GetEmployeeByEmployeeId(int? employeeId)
      {
        return await OData.GetBy<Employee>(new ODataServiceItemArgs() { Url = $"{BasePath}odata/Northwind/Employees({employeeId})" });
      }
    
      public async Task<HttpResponseMessage> UpdateEmployee(int? employeeId, Employee employee)
      {
        return await OData.Update($"{BasePath}odata/Northwind/Employees({employeeId})", employee);
      }
            
      public async Task<ODataServiceResult<EmployeeTerritory>> GetEmployeeTerritories(string filter, int? top, int? skip, string orderby, bool? count, string expand, string format, string select)
      {
        return await OData.Get<EmployeeTerritory>(new ODataServiceArgs() { Url = $"{BasePath}odata/Northwind/EmployeeTerritories", Filter = filter, Top = top, Skip = skip, Orderby = orderby, Count = count, Expand = expand, Format = format, Select = select });
      }
    
      public async Task<EmployeeTerritory> CreateEmployeeTerritory(EmployeeTerritory employeeTerritory)
      {
        return await OData.Add($"{BasePath}odata/Northwind/EmployeeTerritories", employeeTerritory);
      }
            
      public async Task<ODataServiceResult<Order>> GetOrders(string filter, int? top, int? skip, string orderby, bool? count, string expand, string format, string select)
      {
        return await OData.Get<Order>(new ODataServiceArgs() { Url = $"{BasePath}odata/Northwind/Orders", Filter = filter, Top = top, Skip = skip, Orderby = orderby, Count = count, Expand = expand, Format = format, Select = select });
      }
    
      public async Task<Order> CreateOrder(Order order)
      {
        return await OData.Add($"{BasePath}odata/Northwind/Orders", order);
      }
    
      public async Task<HttpResponseMessage> DeleteOrder(int? orderId)
      {
        return await OData.Delete($"{BasePath}odata/Northwind/Orders({orderId})");
      }
    
      public async Task<Order> GetOrderByOrderId(int? orderId)
      {
        return await OData.GetBy<Order>(new ODataServiceItemArgs() { Url = $"{BasePath}odata/Northwind/Orders({orderId})" });
      }
    
      public async Task<HttpResponseMessage> UpdateOrder(int? orderId, Order order)
      {
        return await OData.Update($"{BasePath}odata/Northwind/Orders({orderId})", order);
      }
            
      public async Task<ODataServiceResult<OrderDetail>> GetOrderDetails(string filter, int? top, int? skip, string orderby, bool? count, string expand, string format, string select)
      {
        return await OData.Get<OrderDetail>(new ODataServiceArgs() { Url = $"{BasePath}odata/Northwind/OrderDetails", Filter = filter, Top = top, Skip = skip, Orderby = orderby, Count = count, Expand = expand, Format = format, Select = select });
      }
    
      public async Task<OrderDetail> CreateOrderDetail(OrderDetail orderDetail)
      {
        return await OData.Add($"{BasePath}odata/Northwind/OrderDetails", orderDetail);
      }
            
      public async Task<ODataServiceResult<Product>> GetProducts(string filter, int? top, int? skip, string orderby, bool? count, string expand, string format, string select)
      {
        return await OData.Get<Product>(new ODataServiceArgs() { Url = $"{BasePath}odata/Northwind/Products", Filter = filter, Top = top, Skip = skip, Orderby = orderby, Count = count, Expand = expand, Format = format, Select = select });
      }
    
      public async Task<Product> CreateProduct(Product product)
      {
        return await OData.Add($"{BasePath}odata/Northwind/Products", product);
      }
    
      public async Task<HttpResponseMessage> DeleteProduct(int? productId)
      {
        return await OData.Delete($"{BasePath}odata/Northwind/Products({productId})");
      }
    
      public async Task<Product> GetProductByProductId(int? productId)
      {
        return await OData.GetBy<Product>(new ODataServiceItemArgs() { Url = $"{BasePath}odata/Northwind/Products({productId})" });
      }
    
      public async Task<HttpResponseMessage> UpdateProduct(int? productId, Product product)
      {
        return await OData.Update($"{BasePath}odata/Northwind/Products({productId})", product);
      }
            
      public async Task<ODataServiceResult<Region>> GetRegions(string filter, int? top, int? skip, string orderby, bool? count, string expand, string format, string select)
      {
        return await OData.Get<Region>(new ODataServiceArgs() { Url = $"{BasePath}odata/Northwind/Regions", Filter = filter, Top = top, Skip = skip, Orderby = orderby, Count = count, Expand = expand, Format = format, Select = select });
      }
    
      public async Task<Region> CreateRegion(Region region)
      {
        return await OData.Add($"{BasePath}odata/Northwind/Regions", region);
      }
    
      public async Task<HttpResponseMessage> DeleteRegion(int? regionId)
      {
        return await OData.Delete($"{BasePath}odata/Northwind/Regions({regionId})");
      }
    
      public async Task<Region> GetRegionByRegionId(int? regionId)
      {
        return await OData.GetBy<Region>(new ODataServiceItemArgs() { Url = $"{BasePath}odata/Northwind/Regions({regionId})" });
      }
    
      public async Task<HttpResponseMessage> UpdateRegion(int? regionId, Region region)
      {
        return await OData.Update($"{BasePath}odata/Northwind/Regions({regionId})", region);
      }
            
      public async Task<ODataServiceResult<Shipper>> GetShippers(string filter, int? top, int? skip, string orderby, bool? count, string expand, string format, string select)
      {
        return await OData.Get<Shipper>(new ODataServiceArgs() { Url = $"{BasePath}odata/Northwind/Shippers", Filter = filter, Top = top, Skip = skip, Orderby = orderby, Count = count, Expand = expand, Format = format, Select = select });
      }
    
      public async Task<Shipper> CreateShipper(Shipper shipper)
      {
        return await OData.Add($"{BasePath}odata/Northwind/Shippers", shipper);
      }
    
      public async Task<HttpResponseMessage> DeleteShipper(int? shipperId)
      {
        return await OData.Delete($"{BasePath}odata/Northwind/Shippers({shipperId})");
      }
    
      public async Task<Shipper> GetShipperByShipperId(int? shipperId)
      {
        return await OData.GetBy<Shipper>(new ODataServiceItemArgs() { Url = $"{BasePath}odata/Northwind/Shippers({shipperId})" });
      }
    
      public async Task<HttpResponseMessage> UpdateShipper(int? shipperId, Shipper shipper)
      {
        return await OData.Update($"{BasePath}odata/Northwind/Shippers({shipperId})", shipper);
      }
            
      public async Task<ODataServiceResult<Supplier>> GetSuppliers(string filter, int? top, int? skip, string orderby, bool? count, string expand, string format, string select)
      {
        return await OData.Get<Supplier>(new ODataServiceArgs() { Url = $"{BasePath}odata/Northwind/Suppliers", Filter = filter, Top = top, Skip = skip, Orderby = orderby, Count = count, Expand = expand, Format = format, Select = select });
      }
    
      public async Task<Supplier> CreateSupplier(Supplier supplier)
      {
        return await OData.Add($"{BasePath}odata/Northwind/Suppliers", supplier);
      }
    
      public async Task<HttpResponseMessage> DeleteSupplier(int? supplierId)
      {
        return await OData.Delete($"{BasePath}odata/Northwind/Suppliers({supplierId})");
      }
    
      public async Task<Supplier> GetSupplierBySupplierId(int? supplierId)
      {
        return await OData.GetBy<Supplier>(new ODataServiceItemArgs() { Url = $"{BasePath}odata/Northwind/Suppliers({supplierId})" });
      }
    
      public async Task<HttpResponseMessage> UpdateSupplier(int? supplierId, Supplier supplier)
      {
        return await OData.Update($"{BasePath}odata/Northwind/Suppliers({supplierId})", supplier);
      }
            
      public async Task<ODataServiceResult<Territory>> GetTerritories(string filter, int? top, int? skip, string orderby, bool? count, string expand, string format, string select)
      {
        return await OData.Get<Territory>(new ODataServiceArgs() { Url = $"{BasePath}odata/Northwind/Territories", Filter = filter, Top = top, Skip = skip, Orderby = orderby, Count = count, Expand = expand, Format = format, Select = select });
      }
    
      public async Task<Territory> CreateTerritory(Territory territory)
      {
        return await OData.Add($"{BasePath}odata/Northwind/Territories", territory);
      }
    
      public async Task<HttpResponseMessage> DeleteTerritory(string territoryId)
      {
        return await OData.Delete($"{BasePath}odata/Northwind/Territories('{HttpUtility.UrlEncode(territoryId)}')");
      }
    
      public async Task<Territory> GetTerritoryByTerritoryId(string territoryId)
      {
        return await OData.GetBy<Territory>(new ODataServiceItemArgs() { Url = $"{BasePath}odata/Northwind/Territories('{HttpUtility.UrlEncode(territoryId)}')" });
      }
    
      public async Task<HttpResponseMessage> UpdateTerritory(string territoryId, Territory territory)
      {
        return await OData.Update($"{BasePath}odata/Northwind/Territories('{HttpUtility.UrlEncode(territoryId)}')", territory);
      }
            
      public async Task<HttpResponseMessage> DeleteCustomerCustomerDemo(string customerId, string customerTypeId)
      {
        return await OData.Delete($"{BasePath}odata/Northwind/CustomerCustomerDemos(CustomerID='{HttpUtility.UrlEncode(customerId)}',CustomerTypeID='{HttpUtility.UrlEncode(customerTypeId)}')");
      }
    
      public async Task<CustomerCustomerDemo> GetCustomerCustomerDemoByCustomerIdAndCustomerTypeId(string customerId, string customerTypeId)
      {
        return await OData.GetBy<CustomerCustomerDemo>(new ODataServiceItemArgs() { Url = $"{BasePath}odata/Northwind/CustomerCustomerDemos(CustomerID='{HttpUtility.UrlEncode(customerId)}',CustomerTypeID='{HttpUtility.UrlEncode(customerTypeId)}')" });
      }
    
      public async Task<HttpResponseMessage> UpdateCustomerCustomerDemo(string customerId, string customerTypeId, CustomerCustomerDemo customerCustomerDemo)
      {
        return await OData.Update($"{BasePath}odata/Northwind/CustomerCustomerDemos(CustomerID='{HttpUtility.UrlEncode(customerId)}',CustomerTypeID='{HttpUtility.UrlEncode(customerTypeId)}')", customerCustomerDemo);
      }
            
      public async Task<HttpResponseMessage> DeleteEmployeeTerritory(int? employeeId, string territoryId)
      {
        return await OData.Delete($"{BasePath}odata/Northwind/EmployeeTerritories(EmployeeID={employeeId},TerritoryID='{HttpUtility.UrlEncode(territoryId)}')");
      }
    
      public async Task<EmployeeTerritory> GetEmployeeTerritoryByEmployeeIdAndTerritoryId(int? employeeId, string territoryId)
      {
        return await OData.GetBy<EmployeeTerritory>(new ODataServiceItemArgs() { Url = $"{BasePath}odata/Northwind/EmployeeTerritories(EmployeeID={employeeId},TerritoryID='{HttpUtility.UrlEncode(territoryId)}')" });
      }
    
      public async Task<HttpResponseMessage> UpdateEmployeeTerritory(int? employeeId, string territoryId, EmployeeTerritory employeeTerritory)
      {
        return await OData.Update($"{BasePath}odata/Northwind/EmployeeTerritories(EmployeeID={employeeId},TerritoryID='{HttpUtility.UrlEncode(territoryId)}')", employeeTerritory);
      }
            
      public async Task<HttpResponseMessage> DeleteOrderDetail(int? orderId, int? productId)
      {
        return await OData.Delete($"{BasePath}odata/Northwind/OrderDetails(OrderID={orderId},ProductID={productId})");
      }
    
      public async Task<OrderDetail> GetOrderDetailByOrderIdAndProductId(int? orderId, int? productId)
      {
        return await OData.GetBy<OrderDetail>(new ODataServiceItemArgs() { Url = $"{BasePath}odata/Northwind/OrderDetails(OrderID={orderId},ProductID={productId})" });
      }
    
      public async Task<HttpResponseMessage> UpdateOrderDetail(int? orderId, int? productId, OrderDetail orderDetail)
      {
        return await OData.Update($"{BasePath}odata/Northwind/OrderDetails(OrderID={orderId},ProductID={productId})", orderDetail);
      }
        
  }
}
