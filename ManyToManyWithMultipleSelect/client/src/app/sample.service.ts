import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, BehaviorSubject, throwError } from 'rxjs';

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

  getOrders(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Orders`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createOrder(expand: string | null, order: models.Order | null) : Observable<any> {
    return this.odata.post(`/Orders`, order, { expand }, []);
  }

  deleteOrder(id: number | null) : Observable<any> {
    return this.odata.delete(`/Orders(${id})`, item => !(item.Id == id));
  }

  getOrderById(expand: string | null, id: number | null) : Observable<any> {
    return this.odata.getById(`/Orders(${id})`, { expand });
  }

  updateOrder(expand: string | null, id: number | null, order: models.Order | null) : Observable<any> {
    return this.odata.patch(`/Orders(${id})`, order, item => item.Id == id, { expand }, []);
  }

  getOrderDetails(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/OrderDetails`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createOrderDetail(expand: string | null, orderDetail: models.OrderDetail | null) : Observable<any> {
    return this.odata.post(`/OrderDetails`, orderDetail, { expand }, ['Order', 'Product']);
  }

  deleteOrderDetail(id: number | null) : Observable<any> {
    return this.odata.delete(`/OrderDetails(${id})`, item => !(item.Id == id));
  }

  getOrderDetailById(expand: string | null, id: number | null) : Observable<any> {
    return this.odata.getById(`/OrderDetails(${id})`, { expand });
  }

  updateOrderDetail(expand: string | null, id: number | null, orderDetail: models.OrderDetail | null) : Observable<any> {
    return this.odata.patch(`/OrderDetails(${id})`, orderDetail, item => item.Id == id, { expand }, ['Order','Product']);
  }

  getProducts(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) : Observable<any> {
    return this.odata.get(`/Products`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createProduct(expand: string | null, product: models.Product | null) : Observable<any> {
    return this.odata.post(`/Products`, product, { expand }, []);
  }

  deleteProduct(id: number | null) : Observable<any> {
    return this.odata.delete(`/Products(${id})`, item => !(item.Id == id));
  }

  getProductById(expand: string | null, id: number | null) : Observable<any> {
    return this.odata.getById(`/Products(${id})`, { expand });
  }

  updateProduct(expand: string | null, id: number | null, product: models.Product | null) : Observable<any> {
    return this.odata.patch(`/Products(${id})`, product, item => item.Id == id, { expand }, []);
  }
}
