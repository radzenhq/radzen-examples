import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { ConfigService } from './config.service';
import { ODataClient } from './odata-client';
import * as models from './audit-trail-db.model';

@Injectable()
export class AuditTrailDbService {
  odata: ODataClient;
  basePath: string;

  constructor(private http: HttpClient, private config: ConfigService) {
    this.basePath = config.get('auditTrailDb');
    this.odata = new ODataClient(this.http, this.basePath, { legacy: false, withCredentials: true });
  }

  getCategories(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/Categories`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createCategory(expand: string | null, category: models.Category | null) {
    return this.odata.post(`/Categories`, category, { expand });
  }

  deleteCategory(id: number | null) {
    return this.odata.delete(`/Categories(${id})`, item => !(item.Id == id));
  }

  getCategoryById(expand: string | null, id: number | null) {
    return this.odata.getById(`/Categories(${id})`, { expand });
  }

  updateCategory(expand: string | null, id: number | null, category: models.Category | null) {
    return this.odata.patch(`/Categories(${id})`, category, item => item.Id == id, { expand });
  }
}
