import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, BehaviorSubject, throwError } from 'rxjs';

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

  getCategories(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Categories`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createCategory(expand: string | null, category: models.Category | null) : Observable<any> {
    return this.odata.post(`/Categories`, category, { expand }, []);
  }

  deleteCategory(categoryId: number | null) : Observable<any> {
    return this.odata.delete(`/Categories(${categoryId})`, item => !(item.CategoryID == categoryId));
  }

  getCategoryByCategoryId(expand: string | null, categoryId: number | null) : Observable<any> {
    return this.odata.getById(`/Categories(${categoryId})`, { expand });
  }

  updateCategory(expand: string | null, categoryId: number | null, category: models.Category | null) : Observable<any> {
    return this.odata.patch(`/Categories(${categoryId})`, category, item => item.CategoryID == categoryId, { expand }, []);
  }

  getCustomers(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Customers`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createCustomer(expand: string | null, customer: models.Customer | null) : Observable<any> {
    return this.odata.post(`/Customers`, customer, { expand }, []);
  }

  deleteCustomer(customerId: string | null) : Observable<any> {
    return this.odata.delete(`/Customers('${encodeURIComponent(customerId)}')`, item => !(item.CustomerID == customerId));
  }

  getCustomerByCustomerId(expand: string | null, customerId: string | null) : Observable<any> {
    return this.odata.getById(`/Customers('${encodeURIComponent(customerId)}')`, { expand });
  }

  updateCustomer(expand: string | null, customerId: string | null, customer: models.Customer | null) : Observable<any> {
    return this.odata.patch(`/Customers('${encodeURIComponent(customerId)}')`, customer, item => item.CustomerID == customerId, { expand }, []);
  }

  getCustomerCustomerDemos(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/CustomerCustomerDemos`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createCustomerCustomerDemo(expand: string | null, customerCustomerDemo: models.CustomerCustomerDemo | null) : Observable<any> {
    return this.odata.post(`/CustomerCustomerDemos`, customerCustomerDemo, { expand }, ['Customer', 'CustomerDemographic']);
  }

  getCustomerDemographics(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/CustomerDemographics`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createCustomerDemographic(expand: string | null, customerDemographic: models.CustomerDemographic | null) : Observable<any> {
    return this.odata.post(`/CustomerDemographics`, customerDemographic, { expand }, []);
  }

  deleteCustomerDemographic(customerTypeId: string | null) : Observable<any> {
    return this.odata.delete(`/CustomerDemographics('${encodeURIComponent(customerTypeId)}')`, item => !(item.CustomerTypeID == customerTypeId));
  }

  getCustomerDemographicByCustomerTypeId(expand: string | null, customerTypeId: string | null) : Observable<any> {
    return this.odata.getById(`/CustomerDemographics('${encodeURIComponent(customerTypeId)}')`, { expand });
  }

  updateCustomerDemographic(expand: string | null, customerTypeId: string | null, customerDemographic: models.CustomerDemographic | null) : Observable<any> {
    return this.odata.patch(`/CustomerDemographics('${encodeURIComponent(customerTypeId)}')`, customerDemographic, item => item.CustomerTypeID == customerTypeId, { expand }, []);
  }

  getEmployees(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Employees`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createEmployee(expand: string | null, employee: models.Employee | null) : Observable<any> {
    return this.odata.post(`/Employees`, employee, { expand }, ['Employee1']);
  }

  deleteEmployee(employeeId: number | null) : Observable<any> {
    return this.odata.delete(`/Employees(${employeeId})`, item => !(item.EmployeeID == employeeId));
  }

  getEmployeeByEmployeeId(expand: string | null, employeeId: number | null) : Observable<any> {
    return this.odata.getById(`/Employees(${employeeId})`, { expand });
  }

  updateEmployee(expand: string | null, employeeId: number | null, employee: models.Employee | null) : Observable<any> {
    return this.odata.patch(`/Employees(${employeeId})`, employee, item => item.EmployeeID == employeeId, { expand }, ['Employee1']);
  }

  getEmployeeTerritories(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/EmployeeTerritories`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createEmployeeTerritory(expand: string | null, employeeTerritory: models.EmployeeTerritory | null) : Observable<any> {
    return this.odata.post(`/EmployeeTerritories`, employeeTerritory, { expand }, ['Employee', 'Territory']);
  }

  getOrders(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Orders`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createOrder(expand: string | null, order: models.Order | null) : Observable<any> {
    return this.odata.post(`/Orders`, order, { expand }, ['Customer', 'Employee', 'Shipper']);
  }

  deleteOrder(orderId: number | null) : Observable<any> {
    return this.odata.delete(`/Orders(${orderId})`, item => !(item.OrderID == orderId));
  }

  getOrderByOrderId(expand: string | null, orderId: number | null) : Observable<any> {
    return this.odata.getById(`/Orders(${orderId})`, { expand });
  }

  updateOrder(expand: string | null, orderId: number | null, order: models.Order | null) : Observable<any> {
    return this.odata.patch(`/Orders(${orderId})`, order, item => item.OrderID == orderId, { expand }, ['Customer','Employee','Shipper']);
  }

  getOrderDetails(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/OrderDetails`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createOrderDetail(expand: string | null, orderDetail: models.OrderDetail | null) : Observable<any> {
    return this.odata.post(`/OrderDetails`, orderDetail, { expand }, ['Order', 'Product']);
  }

  getProducts(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Products`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createProduct(expand: string | null, product: models.Product | null) : Observable<any> {
    return this.odata.post(`/Products`, product, { expand }, ['Supplier', 'Category']);
  }

  deleteProduct(productId: number | null) : Observable<any> {
    return this.odata.delete(`/Products(${productId})`, item => !(item.ProductID == productId));
  }

  getProductByProductId(expand: string | null, productId: number | null) : Observable<any> {
    return this.odata.getById(`/Products(${productId})`, { expand });
  }

  updateProduct(expand: string | null, productId: number | null, product: models.Product | null) : Observable<any> {
    return this.odata.patch(`/Products(${productId})`, product, item => item.ProductID == productId, { expand }, ['Supplier','Category']);
  }

  getRegions(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Regions`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createRegion(expand: string | null, region: models.Region | null) : Observable<any> {
    return this.odata.post(`/Regions`, region, { expand }, []);
  }

  deleteRegion(regionId: number | null) : Observable<any> {
    return this.odata.delete(`/Regions(${regionId})`, item => !(item.RegionID == regionId));
  }

  getRegionByRegionId(expand: string | null, regionId: number | null) : Observable<any> {
    return this.odata.getById(`/Regions(${regionId})`, { expand });
  }

  updateRegion(expand: string | null, regionId: number | null, region: models.Region | null) : Observable<any> {
    return this.odata.patch(`/Regions(${regionId})`, region, item => item.RegionID == regionId, { expand }, []);
  }

  getShippers(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Shippers`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createShipper(expand: string | null, shipper: models.Shipper | null) : Observable<any> {
    return this.odata.post(`/Shippers`, shipper, { expand }, []);
  }

  deleteShipper(shipperId: number | null) : Observable<any> {
    return this.odata.delete(`/Shippers(${shipperId})`, item => !(item.ShipperID == shipperId));
  }

  getShipperByShipperId(expand: string | null, shipperId: number | null) : Observable<any> {
    return this.odata.getById(`/Shippers(${shipperId})`, { expand });
  }

  updateShipper(expand: string | null, shipperId: number | null, shipper: models.Shipper | null) : Observable<any> {
    return this.odata.patch(`/Shippers(${shipperId})`, shipper, item => item.ShipperID == shipperId, { expand }, []);
  }

  getSuppliers(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Suppliers`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createSupplier(expand: string | null, supplier: models.Supplier | null) : Observable<any> {
    return this.odata.post(`/Suppliers`, supplier, { expand }, []);
  }

  deleteSupplier(supplierId: number | null) : Observable<any> {
    return this.odata.delete(`/Suppliers(${supplierId})`, item => !(item.SupplierID == supplierId));
  }

  getSupplierBySupplierId(expand: string | null, supplierId: number | null) : Observable<any> {
    return this.odata.getById(`/Suppliers(${supplierId})`, { expand });
  }

  updateSupplier(expand: string | null, supplierId: number | null, supplier: models.Supplier | null) : Observable<any> {
    return this.odata.patch(`/Suppliers(${supplierId})`, supplier, item => item.SupplierID == supplierId, { expand }, []);
  }

  getTerritories(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Territories`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createTerritory(expand: string | null, territory: models.Territory | null) : Observable<any> {
    return this.odata.post(`/Territories`, territory, { expand }, ['Region']);
  }

  deleteTerritory(territoryId: string | null) : Observable<any> {
    return this.odata.delete(`/Territories('${encodeURIComponent(territoryId)}')`, item => !(item.TerritoryID == territoryId));
  }

  getTerritoryByTerritoryId(expand: string | null, territoryId: string | null) : Observable<any> {
    return this.odata.getById(`/Territories('${encodeURIComponent(territoryId)}')`, { expand });
  }

  updateTerritory(expand: string | null, territoryId: string | null, territory: models.Territory | null) : Observable<any> {
    return this.odata.patch(`/Territories('${encodeURIComponent(territoryId)}')`, territory, item => item.TerritoryID == territoryId, { expand }, ['Region']);
  }

  deleteCustomerCustomerDemo(customerId: string | null, customerTypeId: string | null) : Observable<any> {
    return this.odata.delete(`/CustomerCustomerDemos(CustomerID='${encodeURIComponent(customerId)}',CustomerTypeID='${encodeURIComponent(customerTypeId)}')`, item => !(item.CustomerID == customerId && item.CustomerTypeID == customerTypeId));
  }

  getCustomerCustomerDemoByCustomerIdAndCustomerTypeId(customerId: string | null, customerTypeId: string | null, expand: string | null) : Observable<any> {
    return this.odata.getById(`/CustomerCustomerDemos(CustomerID='${encodeURIComponent(customerId)}',CustomerTypeID='${encodeURIComponent(customerTypeId)}')`, { expand });
  }

  updateCustomerCustomerDemo(customerId: string | null, customerTypeId: string | null, expand: string | null, customerCustomerDemo: models.CustomerCustomerDemo | null) : Observable<any> {
    return this.odata.patch(`/CustomerCustomerDemos(CustomerID='${encodeURIComponent(customerId)}',CustomerTypeID='${encodeURIComponent(customerTypeId)}')`, customerCustomerDemo, item => item.CustomerID == customerId && item.CustomerTypeID == customerTypeId, { expand }, ['Customer','CustomerDemographic']);
  }

  deleteEmployeeTerritory(employeeId: number | null, territoryId: string | null) : Observable<any> {
    return this.odata.delete(`/EmployeeTerritories(EmployeeID=${employeeId},TerritoryID='${encodeURIComponent(territoryId)}')`, item => !(item.EmployeeID == employeeId && item.TerritoryID == territoryId));
  }

  getEmployeeTerritoryByEmployeeIdAndTerritoryId(employeeId: number | null, territoryId: string | null, expand: string | null) : Observable<any> {
    return this.odata.getById(`/EmployeeTerritories(EmployeeID=${employeeId},TerritoryID='${encodeURIComponent(territoryId)}')`, { expand });
  }

  updateEmployeeTerritory(employeeId: number | null, territoryId: string | null, expand: string | null, employeeTerritory: models.EmployeeTerritory | null) : Observable<any> {
    return this.odata.patch(`/EmployeeTerritories(EmployeeID=${employeeId},TerritoryID='${encodeURIComponent(territoryId)}')`, employeeTerritory, item => item.EmployeeID == employeeId && item.TerritoryID == territoryId, { expand }, ['Employee','Territory']);
  }

  deleteOrderDetail(orderId: number | null, productId: number | null) : Observable<any> {
    return this.odata.delete(`/OrderDetails(OrderID=${orderId},ProductID=${productId})`, item => !(item.OrderID == orderId && item.ProductID == productId));
  }

  getOrderDetailByOrderIdAndProductId(orderId: number | null, productId: number | null, expand: string | null) : Observable<any> {
    return this.odata.getById(`/OrderDetails(OrderID=${orderId},ProductID=${productId})`, { expand });
  }

  updateOrderDetail(orderId: number | null, productId: number | null, expand: string | null, orderDetail: models.OrderDetail | null) : Observable<any> {
    return this.odata.patch(`/OrderDetails(OrderID=${orderId},ProductID=${productId})`, orderDetail, item => item.OrderID == orderId && item.ProductID == productId, { expand }, ['Order','Product']);
  }
}
