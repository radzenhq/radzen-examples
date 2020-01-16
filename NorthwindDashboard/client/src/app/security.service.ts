import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

import { environment } from '../environments/environment';
import { ODataClient } from './odata-client';

const TOKEN = 'id_token';
const NAME = 'unique_name';
const ROLE = 'role';

export class UserService {
  name: string;
  roles: string[];
  profile: any;
  jwt = new JwtHelperService();

  constructor() {
    this.init();
  }

  isAuthenticated() {
    const notExpired = !this.jwt.isTokenExpired(localStorage.getItem(TOKEN));

    if(localStorage.getItem(TOKEN) && !notExpired) {
      this.logout();
    }

    return notExpired;
  }

  isInRole(role: string | string[]): boolean {
    const roles = Array.isArray(role) ? role : [role];

    if (roles.indexOf('Everybody') >= 0) {
      return true;
    }

    if (!this.isAuthenticated()) {
      return false;
    }

    if (roles.indexOf('Authenticated') >= 0) {
      return true;
    }

    return roles.some(role => this.roles.indexOf(role) >= 0);
  }

  logout() {
    localStorage.removeItem(TOKEN);

    location.reload();
  }

  init() {
    let profile = {
      [NAME]: null,
      [ROLE]: null
    };

    if (this.isAuthenticated()) {
      profile = this.jwt.decodeToken(localStorage.getItem(TOKEN));
    }

    this.profile = profile;
    this.name = profile[NAME];
    this.roles = profile[ROLE];

    if (!this.roles) {
      this.roles = [];
    }

    this.roles = Array.isArray(this.roles) ? this.roles : [this.roles];
  }
}

@Injectable()
export class SecurityService {
  basePath = environment.securityUrl;
  odata: ODataClient;

  constructor(private router: Router, private http: HttpClient, public user: UserService) {
    this.odata = new ODataClient(this.http, this.basePath, { legacy: false, withCredentials: true });
  }

  isAuthenticated() {
    return this.user.isAuthenticated();
  }

  isLoggedIn() {
    return this.isAuthenticated();
  }

  isInRole(role: string | string[]): boolean {
    return this.user.isInRole(role);
  }

  get profile() {
    return this.user.profile;
  }

  get role(): string {
    return this.user.roles[0];
  }

  get roles(): string[] {
    return this.user.roles;
  }

  get name(): string {
    return this.user.name;
  }

  get accessToken(): string {
    return localStorage.getItem(TOKEN)
  }

  get token(): string {
    return this.accessToken;
  }

  logout() {
    this.user.logout();
  }

  canActivate(roles: string[], state: RouterStateSnapshot) {
    if (this.isAuthenticated()) {
      if (this.isInRole(roles)) {
        return true;
      } else {
        this.router.navigateByUrl('/unauthorized');
      }
    } else {
      this.router.navigate([{ outlets: { popup: null } } ])
          .then(() => this.router.navigate(['/login'], { queryParams: { redirectUrl: state.url } }));

      return false;
    }
  }

  login(username: string, password: string) : Observable<any> {
    return this.http.post(`${this.basePath}/login`, JSON.stringify({ username, password }),
      {
        observe: 'response',
        headers: new HttpHeaders().set('Content-Type', 'application/json')
      })
      .pipe(map((result: any) => {
        if (result.status == 200) {
          const { access_token } = result.body;

          localStorage.setItem(TOKEN, access_token);

          this.user.init();

          const { redirectUrl = '/' } = this.router.routerState.snapshot.root.queryParams;

          this.router.navigateByUrl(redirectUrl);
        }
      }))
      .pipe(catchError(response => {
        return throwError(response.error);
      }));
  }

  resetPassword(username: string) : Observable<any> {
    return this.http.post(`${this.basePath}/reset-password`, JSON.stringify({ username }),
      {
        observe: 'response',
        headers: new HttpHeaders().set('Content-Type', 'application/json')
      })
      .pipe(map((result: any) => {
        if (result.status == 200) {
          const { redirectUrl = '/' } = this.router.routerState.snapshot.root.queryParams;

          this.router.navigateByUrl(redirectUrl);
        }
      }))
      .pipe(catchError(response => {
        return throwError(response.error);
      }));
  }

  registerUser(user: any) : Observable<any> {
    return this.http.post(`${this.basePath}/register`, JSON.stringify(user),
      {
        observe: 'response',
        headers: new HttpHeaders().set('Content-Type', 'application/json')
      })
      .pipe(catchError(response => {
        return throwError(response.error);
      }));
  }

  changePassword(oldPassword: string, newPassword: string) : Observable<any> {
    return this.http.post( `${this.basePath}/change-password`, JSON.stringify({ oldPassword, newPassword }),
      {
        observe: 'response',
        headers: new HttpHeaders().set('Content-Type', 'application/json')
      })
      .pipe(catchError(response => {
        return throwError(response.error);
      }));
  }


  getRoleById(id: string) : Observable<any> {
    return this.odata.get(`/ApplicationRoles('${id}')`);
  }

  getRoles(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null) : Observable<any> {
    return this.odata.get('/ApplicationRoles', { filter, top, skip, orderby, count, expand });
  }

  createRole(role: any) : Observable<any> {
    return this.odata.post('/ApplicationRoles', role);
  }

  updateRole(id: string, role: any) : Observable<any> {
    return this.odata.patch(`/ApplicationRoles('${id}')`, role, role => role.Id == id);
  }

  deleteRole(id: string) : Observable<any> {
    return this.odata.delete(`/ApplicationRoles('${id}')`, role => role.Id != id);
  }

  getUserById(id: string) : Observable<any> {
    return this.odata.get(`/ApplicationUsers('${id}')`);
  }

  createUser(user: any) : Observable<any> {
    return this.odata.post('/ApplicationUsers', user);
  }

  getUsers(filter: string | null, top: number | null, skip: number | null, orderby: string | null, count: boolean | null, expand: string | null) : Observable<any> {
    return this.odata.get('/ApplicationUsers', { filter, top, skip, orderby, count, expand });
  }

  deleteUser(id: string) : Observable<any> {
    return this.odata.delete(`/ApplicationUsers('${id}')`, user => user.Id != id);
  }

  updateUser(id: string, user: any) : Observable<any> {
    return this.odata.patch(`/ApplicationUsers('${id}')`, user, user => user.Id == id);
  }
}
