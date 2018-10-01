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

  getCustOrderHists(customerId: string | null, filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/CustOrderHistsFunc(CustomerID='${encodeURIComponent(customerId)}')`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getCustOrdersDetails(orderId: number | null, filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/CustOrdersDetailsFunc(OrderID=${orderId})`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getCustOrdersOrders(customerId: string | null, filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/CustOrdersOrdersFunc(CustomerID='${encodeURIComponent(customerId)}')`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getEmployeeSalesByCountries(beginningDate: string | null, endingDate: string | null, filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/EmployeeSalesByCountriesFunc(Beginning_Date='${encodeURIComponent(beginningDate)}',Ending_Date='${encodeURIComponent(endingDate)}')`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getSalesByCategory1S(categoryName: string | null, ordYear: string | null, filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/SalesByCategory1sFunc(CategoryName='${encodeURIComponent(categoryName)}',OrdYear='${encodeURIComponent(ordYear)}')`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getSalesByYears(beginningDate: string | null, endingDate: string | null, filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/SalesByYearsFunc(Beginning_Date='${encodeURIComponent(beginningDate)}',Ending_Date='${encodeURIComponent(endingDate)}')`, { filter, top, skip, orderby, count, expand, format, select });
  }

  getTenMostExpensiveProducts(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/TenMostExpensiveProductsFunc()`, { filter, top, skip, orderby, count, expand, format, select });
  }
}
