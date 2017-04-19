import { Injectable } from '@angular/core';
import { Http, Headers, URLSearchParams, QueryEncoder } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

import { environment } from '../environments/environment';
import { PlusQueryEncoder } from './plus-query-encoder';

import * as models from './sample.model';

import { SampleAuthorizationService } from './sample-auth.service';

@Injectable()
export class SampleService {
  basePath = environment.sample;

  constructor(private http: Http, private auth: SampleAuthorizationService) {
  }

  getProducts(filter?: string, top?: number, skip?: number, orderby?: string, count?: boolean, expand?: string) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);

    const search = new URLSearchParams('', new PlusQueryEncoder());

    if (filter) {
      search.set('$filter', filter);
    }

    if (top != null) {
      search.set('$top', top.toString());
    }

    if (skip != null) {
      search.set('$skip', skip.toString());
    }

    if (orderby) {
      search.set('$orderby', orderby);
    }

    if (count != null) {
      search.set('$count', count.toString());
    }

    if (expand) {
      search.set('$expand', expand);
    }

    return this.http.request(`${this.basePath}/Products`, {
      method: 'get',
      search,
      headers
    })
    .map(response => {
      switch (response.status) {
        case 200:
          return response.json();
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  createProduct(product: models.Product) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);
    headers.append('Content-Type', 'application/json');

    if (product.hasOwnProperty('@odata.etag')) {
      headers.append('If-Match', product['@odata.etag']);
    }

    return this.http.request(`${this.basePath}/Products`, {
      method: 'post',
      headers,
      body: JSON.stringify(product)
    })
    .map(response => {
      switch (response.status) {
        case 204:
          return product;
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  deleteProduct(id: number) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);

    return this.http.request(`${this.basePath}/Products(${id})`, {
      method: 'delete',
      headers
    })
    .map(response => {
      switch (response.status) {
        case 204:
          return {};
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  getProductById(id: number) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);

    return this.http.request(`${this.basePath}/Products(${id})`, {
      method: 'get',
      headers
    })
    .map(response => {
      switch (response.status) {
        case 200:
          return response.json();
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  updateProduct(id: number, product: models.Product) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);
    headers.append('Content-Type', 'application/json');

    if (product.hasOwnProperty('@odata.etag')) {
      headers.append('If-Match', product['@odata.etag']);
    }

    return this.http.request(`${this.basePath}/Products(${id})`, {
      method: 'patch',
      headers,
      body: JSON.stringify(product)
    })
    .map(response => {
      switch (response.status) {
        case 204:
          return product;
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }
}
