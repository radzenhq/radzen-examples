import { Injectable } from '@angular/core';
import { Http, Headers, URLSearchParams, QueryEncoder } from '@angular/http';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

import { environment } from '../environments/environment';
import { PlusQueryEncoder } from './plus-query-encoder';

import * as models from './spotify.model';

import { SpotifyAuthorizationService } from './spotify-auth.service';

@Injectable()
export class SpotifyService {
  basePath = environment.spotify;

  constructor(private http: Http, private auth: SpotifyAuthorizationService) {
  }

  getAlbumTracks(id?: string) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);

    return this.http.request(`${this.basePath}albums/${id}/tracks`, {
      method: 'get',
      headers
    })
    .map(response => {
      switch (response.status) {
        case 200:
          return response.json();
      }
    })
    .toPromise();
  }

  getNewReleases() {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);

    return this.http.request(`${this.basePath}browse/new-releases`, {
      method: 'get',
      headers
    })
    .map(response => {
      switch (response.status) {
        case 200:
          return response.json();
      }
    })
    .toPromise();
  }
}
