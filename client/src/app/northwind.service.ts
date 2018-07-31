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

  getCategories(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/Categories`, { filter, top, skip, orderby, expand, count });
  }

  createCategory(category: any) {
    return this.odata.post(`/Categories`, category);
  }

  getCategoryById(categoryId: number) {
    return this.odata.get(`/Categories(${categoryId})`);
  }

  deleteCategory(categoryId: number) {
    return this.odata.delete(`/Categories(${categoryId})`, item => !(item.CategoryID == categoryId));
  }

  updateCategory(categoryId: number, category: any) {
    return this.odata.patch(`/Categories(${categoryId})`, category, item => item.CategoryID == categoryId);
  }

  getCustomers(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/Customers`, { filter, top, skip, orderby, expand, count });
  }

  createCustomer(customer: any) {
    return this.odata.post(`/Customers`, customer);
  }

  getCustomerById(customerId: string) {
    return this.odata.get(`/Customers('${customerId}')`);
  }

  deleteCustomer(customerId: string) {
    return this.odata.delete(`/Customers('${customerId}')`, item => !(item.CustomerID == customerId));
  }

  updateCustomer(customerId: string, customer: any) {
    return this.odata.patch(`/Customers('${customerId}')`, customer, item => item.CustomerID == customerId);
  }

  getCustomerCustomerDemos(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/CustomerCustomerDemos`, { filter, top, skip, orderby, expand, count });
  }

  createCustomerCustomerDemo(customerCustomerDemo: any) {
    return this.odata.post(`/CustomerCustomerDemos`, customerCustomerDemo);
  }

  getCustomerCustomerDemoById(customerId: string, customerTypeId: string) {
    return this.odata.get(`/CustomerCustomerDemos('${customerId}','${customerTypeId}')`);
  }

  deleteCustomerCustomerDemo(customerId: string, customerTypeId: string) {
    return this.odata.delete(`/CustomerCustomerDemos('${customerId}','${customerTypeId}')`, item => !(item.CustomerID == customerId && item.CustomerTypeID == customerTypeId));
  }

  updateCustomerCustomerDemo(customerId: string, customerTypeId: string, customerCustomerDemo: any) {
    return this.odata.patch(`/CustomerCustomerDemos('${customerId}','${customerTypeId}')`, customerCustomerDemo, item => item.CustomerID == customerId && item.CustomerTypeID == customerTypeId);
  }

  getCustomerDemographics(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/CustomerDemographics`, { filter, top, skip, orderby, expand, count });
  }

  createCustomerDemographic(customerDemographic: any) {
    return this.odata.post(`/CustomerDemographics`, customerDemographic);
  }

  getCustomerDemographicById(customerTypeId: string) {
    return this.odata.get(`/CustomerDemographics('${customerTypeId}')`);
  }

  deleteCustomerDemographic(customerTypeId: string) {
    return this.odata.delete(`/CustomerDemographics('${customerTypeId}')`, item => !(item.CustomerTypeID == customerTypeId));
  }

  updateCustomerDemographic(customerTypeId: string, customerDemographic: any) {
    return this.odata.patch(`/CustomerDemographics('${customerTypeId}')`, customerDemographic, item => item.CustomerTypeID == customerTypeId);
  }

  getEmployees(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/Employees`, { filter, top, skip, orderby, expand, count });
  }

  createEmployee(employee: any) {
    return this.odata.post(`/Employees`, employee);
  }

  getEmployeeById(employeeId: number) {
    return this.odata.get(`/Employees(${employeeId})`);
  }

  deleteEmployee(employeeId: number) {
    return this.odata.delete(`/Employees(${employeeId})`, item => !(item.EmployeeID == employeeId));
  }

  updateEmployee(employeeId: number, employee: any) {
    return this.odata.patch(`/Employees(${employeeId})`, employee, item => item.EmployeeID == employeeId);
  }

  getEmployeeTerritories(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/EmployeeTerritories`, { filter, top, skip, orderby, expand, count });
  }

  createEmployeeTerritory(employeeTerritory: any) {
    return this.odata.post(`/EmployeeTerritories`, employeeTerritory);
  }

  getEmployeeTerritoryById(employeeId: number, territoryId: string) {
    return this.odata.get(`/EmployeeTerritories(${employeeId},'${territoryId}')`);
  }

  deleteEmployeeTerritory(employeeId: number, territoryId: string) {
    return this.odata.delete(`/EmployeeTerritories(${employeeId},'${territoryId}')`, item => !(item.EmployeeID == employeeId && item.TerritoryID == territoryId));
  }

  updateEmployeeTerritory(employeeId: number, territoryId: string, employeeTerritory: any) {
    return this.odata.patch(`/EmployeeTerritories(${employeeId},'${territoryId}')`, employeeTerritory, item => item.EmployeeID == employeeId && item.TerritoryID == territoryId);
  }

  getNorthwindOrders(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/NorthwindOrders`, { filter, top, skip, orderby, expand, count });
  }

  createNorthwindOrder(northwindOrder: any) {
    return this.odata.post(`/NorthwindOrders`, northwindOrder);
  }

  getNorthwindOrderById(orderId: number) {
    return this.odata.get(`/NorthwindOrders(${orderId})`);
  }

  deleteNorthwindOrder(orderId: number) {
    return this.odata.delete(`/NorthwindOrders(${orderId})`, item => !(item.OrderID == orderId));
  }

  updateNorthwindOrder(orderId: number, northwindOrder: any) {
    return this.odata.patch(`/NorthwindOrders(${orderId})`, northwindOrder, item => item.OrderID == orderId);
  }

  getNorthwindOrderDetails(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/NorthwindOrderDetails`, { filter, top, skip, orderby, expand, count });
  }

  createNorthwindOrderDetail(northwindOrderDetail: any) {
    return this.odata.post(`/NorthwindOrderDetails`, northwindOrderDetail);
  }

  getNorthwindOrderDetailById(orderId: number, productId: number) {
    return this.odata.get(`/NorthwindOrderDetails(${orderId},${productId})`);
  }

  deleteNorthwindOrderDetail(orderId: number, productId: number) {
    return this.odata.delete(`/NorthwindOrderDetails(${orderId},${productId})`, item => !(item.OrderID == orderId && item.ProductID == productId));
  }

  updateNorthwindOrderDetail(orderId: number, productId: number, northwindOrderDetail: any) {
    return this.odata.patch(`/NorthwindOrderDetails(${orderId},${productId})`, northwindOrderDetail, item => item.OrderID == orderId && item.ProductID == productId);
  }

  getNorthwindProducts(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/NorthwindProducts`, { filter, top, skip, orderby, expand, count });
  }

  createNorthwindProduct(northwindProduct: any) {
    return this.odata.post(`/NorthwindProducts`, northwindProduct);
  }

  getNorthwindProductById(productId: number) {
    return this.odata.get(`/NorthwindProducts(${productId})`);
  }

  deleteNorthwindProduct(productId: number) {
    return this.odata.delete(`/NorthwindProducts(${productId})`, item => !(item.ProductID == productId));
  }

  updateNorthwindProduct(productId: number, northwindProduct: any) {
    return this.odata.patch(`/NorthwindProducts(${productId})`, northwindProduct, item => item.ProductID == productId);
  }

  getRegions(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/Regions`, { filter, top, skip, orderby, expand, count });
  }

  createRegion(region: any) {
    return this.odata.post(`/Regions`, region);
  }

  getRegionById(regionId: number) {
    return this.odata.get(`/Regions(${regionId})`);
  }

  deleteRegion(regionId: number) {
    return this.odata.delete(`/Regions(${regionId})`, item => !(item.RegionID == regionId));
  }

  updateRegion(regionId: number, region: any) {
    return this.odata.patch(`/Regions(${regionId})`, region, item => item.RegionID == regionId);
  }

  getSuppliers(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/Suppliers`, { filter, top, skip, orderby, expand, count });
  }

  createSupplier(supplier: any) {
    return this.odata.post(`/Suppliers`, supplier);
  }

  getSupplierById(supplierId: number) {
    return this.odata.get(`/Suppliers(${supplierId})`);
  }

  deleteSupplier(supplierId: number) {
    return this.odata.delete(`/Suppliers(${supplierId})`, item => !(item.SupplierID == supplierId));
  }

  updateSupplier(supplierId: number, supplier: any) {
    return this.odata.patch(`/Suppliers(${supplierId})`, supplier, item => item.SupplierID == supplierId);
  }

  getTerritories(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/Territories`, { filter, top, skip, orderby, expand, count });
  }

  createTerritory(territory: any) {
    return this.odata.post(`/Territories`, territory);
  }

  getTerritoryById(territoryId: string) {
    return this.odata.get(`/Territories('${territoryId}')`);
  }

  deleteTerritory(territoryId: string) {
    return this.odata.delete(`/Territories('${territoryId}')`, item => !(item.TerritoryID == territoryId));
  }

  updateTerritory(territoryId: string, territory: any) {
    return this.odata.patch(`/Territories('${territoryId}')`, territory, item => item.TerritoryID == territoryId);
  }
}
