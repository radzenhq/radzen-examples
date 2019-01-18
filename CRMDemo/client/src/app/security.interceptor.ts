import { Injectable, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { SecurityService } from './security.service';
import { environment } from '../environments/environment';

@Injectable()
export class SecurityInterceptor implements HttpInterceptor {
  constructor(private injector: Injector, private router: Router) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const auth = this.injector.get(SecurityService);

    if (request.url.indexOf(environment.crm) != 0 && request.url.indexOf(environment.securityUrl) != 0) {
      return next.handle(request);
    }

    if (auth.accessToken) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${auth.accessToken}`
        }
      });
    }

    return next.handle(request).catch((response: any) => {
      if (response.status === 401) {
        const redirectUrl = this.router.url;

        this.router.navigate([{outlets: {popup: null}}]).then(() =>
            this.router.navigate(['login'], {queryParams: {redirectUrl}})
        );

        response.error = {error: {message: 'Session expired.'}};
      }

      return Observable.throw(response);
    });
  }
}
