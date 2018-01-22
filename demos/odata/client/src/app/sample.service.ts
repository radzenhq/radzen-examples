import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { environment } from '../environments/environment';
import { ODataClient } from './odata-client';
import * as models from './sample.model';

@Injectable()
export class SampleService {
  odata: ODataClient;
  basePath = environment.sample;

  constructor(private http: HttpClient) {
    this.odata = new ODataClient(this.http, this.basePath, { legacy: false, withCredentials: true });
  }

  getOrderDetails(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/OrderDetails`, { filter, top, skip, orderby, expand, count });
  }

  createOrderDetail(orderDetail: any) {
    return this.odata.post(`/OrderDetails`, orderDetail);
  }

  deleteOrderDetail(id: number) {
    return this.odata.delete(`/OrderDetails(${id})`, item => item.Id != id);
  }

  getOrderDetailById(id: number) {
    return this.odata.get(`/OrderDetails(${id})`);
  }

  updateOrderDetail(id: number, orderDetail: any) {
    return this.odata.patch(`/OrderDetails(${id})`, orderDetail, item => item.Id == id);
  }

  getOrders(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/Orders`, { filter, top, skip, orderby, expand, count });
  }

  createOrder(order: any) {
    return this.odata.post(`/Orders`, order);
  }

  deleteOrder(id: number) {
    return this.odata.delete(`/Orders(${id})`, item => item.Id != id);
  }

  getOrderById(id: number) {
    return this.odata.get(`/Orders(${id})`);
  }

  updateOrder(id: number, order: any) {
    return this.odata.patch(`/Orders(${id})`, order, item => item.Id == id);
  }

  getProducts(filter: string | null, top: number | null, skip: number | null, orderby: string | null, expand: string | null, count: boolean | null) {
    return this.odata.get(`/Products`, { filter, top, skip, orderby, expand, count });
  }

  createProduct(product: any) {
    return this.odata.post(`/Products`, product);
  }

  deleteProduct(id: number) {
    return this.odata.delete(`/Products(${id})`, item => item.Id != id);
  }

  getProductById(id: number) {
    return this.odata.get(`/Products(${id})`);
  }

  updateProduct(id: number, product: any) {
    return this.odata.patch(`/Products(${id})`, product, item => item.Id == id);
  }
}
