import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';

import { SpotifyAuthorizationService } from './spotify-auth.service';

@Injectable()
export class SpotifyAuthorizationInterceptor implements HttpInterceptor {
  constructor(private auth: SpotifyAuthorizationService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (request.url.indexOf('https://api.spotify.com/v1/') != 0) {
      return next.handle(request);
    }

    if (this.auth.accessToken) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.auth.accessToken}`
        }
      });
    }

    return next.handle(request).do(() => {}, (err: any) => {
      if (err.status === 401) {
        this.auth.login();
      }
    });
  }
}
