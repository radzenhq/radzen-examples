import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { ConfigService } from './config.service';
import { ODataClient } from './odata-client';
import * as models from './sample.model';

@Injectable()
export class SampleService {
  odata: ODataClient;
  basePath: string;

  constructor(private http: HttpClient, private config: ConfigService) {
    this.basePath = config.get('sample');
    this.odata = new ODataClient(this.http, this.basePath, { legacy: false, withCredentials: true });
  }

  getOrders(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/Orders`, { filter, top, skip, orderby, expand, count });
  }

  createOrder(order: any) {
    return this.odata.post(`/Orders`, order);
  }

  getOrderById(id: number) {
    return this.odata.get(`/Orders(${id})`);
  }

  deleteOrder(id: number) {
    return this.odata.delete(`/Orders(${id})`, item => !(item.Id == id));
  }

  updateOrder(id: number, order: any) {
    return this.odata.patch(`/Orders(${id})`, order, item => item.Id == id);
  }

  getOrderDetails(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/OrderDetails`, { filter, top, skip, orderby, expand, count });
  }

  createOrderDetail(orderDetail: any) {
    return this.odata.post(`/OrderDetails`, orderDetail);
  }

  getOrderDetailById(id: number) {
    return this.odata.get(`/OrderDetails(${id})`);
  }

  deleteOrderDetail(id: number) {
    return this.odata.delete(`/OrderDetails(${id})`, item => !(item.Id == id));
  }

  updateOrderDetail(id: number, orderDetail: any) {
    return this.odata.patch(`/OrderDetails(${id})`, orderDetail, item => item.Id == id);
  }

  getProducts(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/Products`, { filter, top, skip, orderby, expand, count });
  }

  createProduct(product: any) {
    return this.odata.post(`/Products`, product);
  }

  getProductById(id: number) {
    return this.odata.get(`/Products(${id})`);
  }

  deleteProduct(id: number) {
    return this.odata.delete(`/Products(${id})`, item => !(item.Id == id));
  }

  updateProduct(id: number, product: any) {
    return this.odata.patch(`/Products(${id})`, product, item => item.Id == id);
  }
}
