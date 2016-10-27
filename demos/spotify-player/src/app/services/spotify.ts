import { Injectable } from '@angular/core';
import { Http, Headers, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs';

import * as models from '../models/spotify';

import { SpotifyAuthorizationService } from './spotify-authorization';

@Injectable()
export class SpotifyService {
  basePath = 'https://api.spotify.com/v1/';

  constructor(private http: Http, private auth: SpotifyAuthorizationService) {
    if (auth.token == null) {
      SpotifyAuthorizationService.login();
    }
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
