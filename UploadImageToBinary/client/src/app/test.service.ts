import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

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

  getProducts(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/Products`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createProduct(product: models.Product | null) {
    return this.odata.post(`/Products`, product);
  }

  deleteProduct(id: number | null) {
    return this.odata.delete(`/Products(${id})`, item => !(item.Id == id));
  }

  getProductById(id: number | null) {
    return this.odata.get(`/Products(${id})`);
  }

  updateProduct(id: number | null, product: models.Product | null) {
    return this.odata.patch(`/Products(${id})`, product, item => item.Id == id);
  }
}
