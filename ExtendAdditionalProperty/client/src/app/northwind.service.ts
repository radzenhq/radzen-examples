import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { ConfigService } from './config.service';
import { ODataClient } from './odata-client';
import * as models from './northwind.model';

@Injectable()
export class NorthwindService {
  odata: ODataClient;
  basePath: string;

  constructor(private http: HttpClient, private config: ConfigService) {
    this.basePath = config.get('northwind');
    this.odata = new ODataClient(this.http, this.basePath, { legacy: false, withCredentials: true });
  }

  getAlphabeticalListOfProducts(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/AlphabeticalListOfProducts`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getCategories(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/Categories`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createCategory(category: models.Category | null) {
    return this.odata.post(`/Categories`, category);
  }

  deleteCategory(categoryId: number | null) {
    return this.odata.delete(`/Categories(${categoryId})`, item => !(item.CategoryID == categoryId));
  }

  getCategoryByCategoryId(categoryId: number | null) {
    return this.odata.get(`/Categories(${categoryId})`);
  }

  updateCategory(categoryId: number | null, category: models.Category | null) {
    return this.odata.patch(`/Categories(${categoryId})`, category, item => item.CategoryID == categoryId);
  }

  getCategorySalesFor1997S(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/CategorySalesFor1997s`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getCurrentProductLists(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/CurrentProductLists`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getCustOrderHists(customerId: string | null, filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/CustOrderHistsFunc(CustomerID='${encodeURIComponent(customerId)}')`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getCustOrdersDetails(orderId: number | null, filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/CustOrdersDetailsFunc(OrderID=${orderId})`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getCustOrdersOrders(customerId: string | null, filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/CustOrdersOrdersFunc(CustomerID='${encodeURIComponent(customerId)}')`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getCustomers(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/Customers`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createCustomer(customer: models.Customer | null) {
    return this.odata.post(`/Customers`, customer);
  }

  deleteCustomer(customerId: string | null) {
    return this.odata.delete(`/Customers('${encodeURIComponent(customerId)}')`, item => !(item.CustomerID == customerId));
  }

  getCustomerByCustomerId(customerId: string | null) {
    return this.odata.get(`/Customers('${encodeURIComponent(customerId)}')`);
  }

  updateCustomer(customerId: string | null, customer: models.Customer | null) {
    return this.odata.patch(`/Customers('${encodeURIComponent(customerId)}')`, customer, item => item.CustomerID == customerId);
  }

  getCustomerAndSuppliersByCities(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/CustomerAndSuppliersByCities`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getCustomerCustomerDemos(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/CustomerCustomerDemos`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createCustomerCustomerDemo(customerCustomerDemo: models.CustomerCustomerDemo | null) {
    return this.odata.post(`/CustomerCustomerDemos`, customerCustomerDemo);
  }

  getCustomerDemographics(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/CustomerDemographics`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createCustomerDemographic(customerDemographic: models.CustomerDemographic | null) {
    return this.odata.post(`/CustomerDemographics`, customerDemographic);
  }

  deleteCustomerDemographic(customerTypeId: string | null) {
    return this.odata.delete(`/CustomerDemographics('${encodeURIComponent(customerTypeId)}')`, item => !(item.CustomerTypeID == customerTypeId));
  }

  getCustomerDemographicByCustomerTypeId(customerTypeId: string | null) {
    return this.odata.get(`/CustomerDemographics('${encodeURIComponent(customerTypeId)}')`);
  }

  updateCustomerDemographic(customerTypeId: string | null, customerDemographic: models.CustomerDemographic | null) {
    return this.odata.patch(`/CustomerDemographics('${encodeURIComponent(customerTypeId)}')`, customerDemographic, item => item.CustomerTypeID == customerTypeId);
  }

  getEmployees(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/Employees`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createEmployee(employee: models.Employee | null) {
    return this.odata.post(`/Employees`, employee);
  }

  deleteEmployee(employeeId: number | null) {
    return this.odata.delete(`/Employees(${employeeId})`, item => !(item.EmployeeID == employeeId));
  }

  getEmployeeByEmployeeId(employeeId: number | null) {
    return this.odata.get(`/Employees(${employeeId})`);
  }

  updateEmployee(employeeId: number | null, employee: models.Employee | null) {
    return this.odata.patch(`/Employees(${employeeId})`, employee, item => item.EmployeeID == employeeId);
  }

  getEmployeeSalesByCountries(beginningDate: string | null, endingDate: string | null, filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/EmployeeSalesByCountriesFunc(Beginning_Date='${encodeURIComponent(beginningDate)}',Ending_Date='${encodeURIComponent(endingDate)}')`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getEmployeeTerritories(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/EmployeeTerritories`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createEmployeeTerritory(employeeTerritory: models.EmployeeTerritory | null) {
    return this.odata.post(`/EmployeeTerritories`, employeeTerritory);
  }

  getInvoices(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/Invoices`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getOrders(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/Orders`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createOrder(order: models.Order | null) {
    return this.odata.post(`/Orders`, order);
  }

  deleteOrder(orderId: number | null) {
    return this.odata.delete(`/Orders(${orderId})`, item => !(item.OrderID == orderId));
  }

  getOrderByOrderId(orderId: number | null) {
    return this.odata.get(`/Orders(${orderId})`);
  }

  updateOrder(orderId: number | null, order: models.Order | null) {
    return this.odata.patch(`/Orders(${orderId})`, order, item => item.OrderID == orderId);
  }

  getOrderDetails(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/OrderDetails`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createOrderDetail(orderDetail: models.OrderDetail | null) {
    return this.odata.post(`/OrderDetails`, orderDetail);
  }

  getOrderDetailsExtendeds(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/OrderDetailsExtendeds`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getOrderSubtotals(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/OrderSubtotals`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getOrdersQries(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/OrdersQries`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getProducts(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/Products`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createProduct(product: models.Product | null) {
    return this.odata.post(`/Products`, product);
  }

  deleteProduct(productId: number | null) {
    return this.odata.delete(`/Products(${productId})`, item => !(item.ProductID == productId));
  }

  getProductByProductId(productId: number | null) {
    return this.odata.get(`/Products(${productId})`);
  }

  updateProduct(productId: number | null, product: models.Product | null) {
    return this.odata.patch(`/Products(${productId})`, product, item => item.ProductID == productId);
  }

  getProductSalesFor1997S(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/ProductSalesFor1997s`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getProductsAboveAveragePrices(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/ProductsAboveAveragePrices`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getProductsByCategories(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/ProductsByCategories`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getQuarterlyOrders(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/QuarterlyOrders`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getRegions(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/Regions`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createRegion(region: models.Region | null) {
    return this.odata.post(`/Regions`, region);
  }

  deleteRegion(regionId: number | null) {
    return this.odata.delete(`/Regions(${regionId})`, item => !(item.RegionID == regionId));
  }

  getRegionByRegionId(regionId: number | null) {
    return this.odata.get(`/Regions(${regionId})`);
  }

  updateRegion(regionId: number | null, region: models.Region | null) {
    return this.odata.patch(`/Regions(${regionId})`, region, item => item.RegionID == regionId);
  }

  getSalesByCategories(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/SalesByCategories`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getSalesByCategory1S(categoryName: string | null, ordYear: string | null, filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/SalesByCategory1sFunc(CategoryName='${encodeURIComponent(categoryName)}',OrdYear='${encodeURIComponent(ordYear)}')`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getSalesByYears(beginningDate: string | null, endingDate: string | null, filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/SalesByYearsFunc(Beginning_Date='${encodeURIComponent(beginningDate)}',Ending_Date='${encodeURIComponent(endingDate)}')`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getSalesTotalsByAmounts(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/SalesTotalsByAmounts`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getShippers(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/Shippers`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createShipper(shipper: models.Shipper | null) {
    return this.odata.post(`/Shippers`, shipper);
  }

  deleteShipper(shipperId: number | null) {
    return this.odata.delete(`/Shippers(${shipperId})`, item => !(item.ShipperID == shipperId));
  }

  getShipperByShipperId(shipperId: number | null) {
    return this.odata.get(`/Shippers(${shipperId})`);
  }

  updateShipper(shipperId: number | null, shipper: models.Shipper | null) {
    return this.odata.patch(`/Shippers(${shipperId})`, shipper, item => item.ShipperID == shipperId);
  }

  getSummaryOfSalesByQuarters(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/SummaryOfSalesByQuarters`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getSummaryOfSalesByYears(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/SummaryOfSalesByYears`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getSuppliers(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/Suppliers`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createSupplier(supplier: models.Supplier | null) {
    return this.odata.post(`/Suppliers`, supplier);
  }

  deleteSupplier(supplierId: number | null) {
    return this.odata.delete(`/Suppliers(${supplierId})`, item => !(item.SupplierID == supplierId));
  }

  getSupplierBySupplierId(supplierId: number | null) {
    return this.odata.get(`/Suppliers(${supplierId})`);
  }

  updateSupplier(supplierId: number | null, supplier: models.Supplier | null) {
    return this.odata.patch(`/Suppliers(${supplierId})`, supplier, item => item.SupplierID == supplierId);
  }

  getTenMostExpensiveProducts(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/TenMostExpensiveProductsFunc()`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getTerritories(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/Territories`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createTerritory(territory: models.Territory | null) {
    return this.odata.post(`/Territories`, territory);
  }

  deleteTerritory(territoryId: string | null) {
    return this.odata.delete(`/Territories('${encodeURIComponent(territoryId)}')`, item => !(item.TerritoryID == territoryId));
  }

  getTerritoryByTerritoryId(territoryId: string | null) {
    return this.odata.get(`/Territories('${encodeURIComponent(territoryId)}')`);
  }

  updateTerritory(territoryId: string | null, territory: models.Territory | null) {
    return this.odata.patch(`/Territories('${encodeURIComponent(territoryId)}')`, territory, item => item.TerritoryID == territoryId);
  }

  deleteCustomerCustomerDemo(customerId: string | null, customerTypeId: string | null) {
    return this.odata.delete(`/CustomerCustomerDemos(CustomerID='${encodeURIComponent(customerId)}',CustomerTypeID='${encodeURIComponent(customerTypeId)}')`, item => !(item.CustomerID == customerId && item.CustomerTypeID == customerTypeId));
  }

  getCustomerCustomerDemoByCustomerIdAndCustomerTypeId(customerId: string | null, customerTypeId: string | null) {
    return this.odata.get(`/CustomerCustomerDemos(CustomerID='${encodeURIComponent(customerId)}',CustomerTypeID='${encodeURIComponent(customerTypeId)}')`);
  }

  updateCustomerCustomerDemo(customerId: string | null, customerTypeId: string | null, customerCustomerDemo: models.CustomerCustomerDemo | null) {
    return this.odata.patch(`/CustomerCustomerDemos(CustomerID='${encodeURIComponent(customerId)}',CustomerTypeID='${encodeURIComponent(customerTypeId)}')`, customerCustomerDemo, item => item.CustomerID == customerId && item.CustomerTypeID == customerTypeId);
  }

  deleteEmployeeTerritory(employeeId: number | null, territoryId: string | null) {
    return this.odata.delete(`/EmployeeTerritories(EmployeeID=${employeeId},TerritoryID='${encodeURIComponent(territoryId)}')`, item => !(item.EmployeeID == employeeId && item.TerritoryID == territoryId));
  }

  getEmployeeTerritoryByEmployeeIdAndTerritoryId(employeeId: number | null, territoryId: string | null) {
    return this.odata.get(`/EmployeeTerritories(EmployeeID=${employeeId},TerritoryID='${encodeURIComponent(territoryId)}')`);
  }

  updateEmployeeTerritory(employeeId: number | null, territoryId: string | null, employeeTerritory: models.EmployeeTerritory | null) {
    return this.odata.patch(`/EmployeeTerritories(EmployeeID=${employeeId},TerritoryID='${encodeURIComponent(territoryId)}')`, employeeTerritory, item => item.EmployeeID == employeeId && item.TerritoryID == territoryId);
  }

  deleteOrderDetail(orderId: number | null, productId: number | null) {
    return this.odata.delete(`/OrderDetails(OrderID=${orderId},ProductID=${productId})`, item => !(item.OrderID == orderId && item.ProductID == productId));
  }

  getOrderDetailByOrderIdAndProductId(orderId: number | null, productId: number | null) {
    return this.odata.get(`/OrderDetails(OrderID=${orderId},ProductID=${productId})`);
  }

  updateOrderDetail(orderId: number | null, productId: number | null, orderDetail: models.OrderDetail | null) {
    return this.odata.patch(`/OrderDetails(OrderID=${orderId},ProductID=${productId})`, orderDetail, item => item.OrderID == orderId && item.ProductID == productId);
  }
}
