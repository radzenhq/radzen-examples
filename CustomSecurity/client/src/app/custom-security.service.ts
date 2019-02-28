import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { ConfigService } from './config.service';
import { ODataClient } from './odata-client';
import * as models from './custom-security.model';

@Injectable()
export class CustomSecurityService {
  odata: ODataClient;
  basePath: string;

  constructor(private http: HttpClient, private config: ConfigService) {
    this.basePath = config.get('customSecurity');
    this.odata = new ODataClient(this.http, this.basePath, { legacy: false, withCredentials: true });
  }

  getRoles(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/Roles`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createRole(expand: string | null, role: models.Role | null) {
    return this.odata.post(`/Roles`, role, { expand });
  }

  deleteRole(id: number | null) {
    return this.odata.delete(`/Roles(${id})`, item => !(item.Id == id));
  }

  getRoleById(expand: string | null, id: number | null) {
    return this.odata.getById(`/Roles(${id})`, { expand });
  }

  updateRole(expand: string | null, id: number | null, role: models.Role | null) {
    return this.odata.patch(`/Roles(${id})`, role, item => item.Id == id, { expand });
  }

  getUsers(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/Users`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createUser(expand: string | null, user: models.User | null) {
    return this.odata.post(`/Users`, user, { expand });
  }

  deleteUser(id: number | null) {
    return this.odata.delete(`/Users(${id})`, item => !(item.Id == id));
  }

  getUserById(expand: string | null, id: number | null) {
    return this.odata.getById(`/Users(${id})`, { expand });
  }

  updateUser(expand: string | null, id: number | null, user: models.User | null) {
    return this.odata.patch(`/Users(${id})`, user, item => item.Id == id, { expand });
  }

  getUserRoles(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null, format: string | null, select: string | null) {
    return this.odata.get(`/UserRoles`, { filter, top, skip, orderby, count, expand, format, select });
  }

  createUserRole(expand: string | null, userRole: models.UserRole | null) {
    return this.odata.post(`/UserRoles`, userRole, { expand });
  }

  deleteUserRole(userId: number | null, roleId: number | null) {
    return this.odata.delete(`/UserRoles(UserId=${userId},RoleId=${roleId})`, item => !(item.UserId == userId && item.RoleId == roleId));
  }

  getUserRoleByUserIdAndRoleId(userId: number | null, roleId: number | null, expand: string | null) {
    return this.odata.getById(`/UserRoles(UserId=${userId},RoleId=${roleId})`, { expand });
  }

  updateUserRole(userId: number | null, roleId: number | null, expand: string | null, userRole: models.UserRole | null) {
    return this.odata.patch(`/UserRoles(UserId=${userId},RoleId=${roleId})`, userRole, item => item.UserId == userId && item.RoleId == roleId, { expand });
  }
}
