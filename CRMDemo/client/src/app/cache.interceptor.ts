import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpResponse, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../environments/environment';

@Injectable()
export class CacheInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (request.url.indexOf(environment.crm) != 0 && request.url.indexOf(environment.securityUrl) != 0 && request.url.indexOf(environment.serverMethodsUrl) != 0) {
      return next.handle(request);
    }

    request = request.clone({
      setHeaders: {
        'Cache-Control': 'no-cache',
        'Pragma': 'no-cache',
        'Expires': 'Sat, 01 Jan 2000 00:00:00 GMT'
      }
    });

    return next.handle(request);
  }
}
