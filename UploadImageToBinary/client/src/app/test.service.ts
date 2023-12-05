import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, BehaviorSubject, throwError } from 'rxjs';

import { ConfigService } from './config.service';
import { ODataClient } from './odata-client';
import * as models from './test.model';

@Injectable()
export class TestService {
  odata: ODataClient;
  basePath: string;

  constructor(private http: HttpClient, private config: ConfigService) {
    this.basePath = config.get('test');
    this.odata = new ODataClient(this.http, this.basePath, { legacy: false, withCredentials: true });
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
