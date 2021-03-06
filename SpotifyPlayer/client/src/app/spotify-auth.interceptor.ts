import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';

import { ConfigService } from './config.service';
import { SpotifyAuthorizationService } from './spotify-auth.service';

@Injectable()
export class SpotifyAuthorizationInterceptor implements HttpInterceptor {
  constructor(private config: ConfigService, private auth: SpotifyAuthorizationService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const basePath = this.config.get('spotify');

    if (request.url.indexOf(basePath) != 0) {
      return next.handle(request);
    }

    if (this.auth.accessToken) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.auth.accessToken}`
        }
      });
    } else {
      this.auth.login();
    }

    return next.handle(request).do(() => {}, (err: any) => {
      if (err.status === 401 || err.status === 467) {
        this.auth.login();
      }
    });
  }
}
