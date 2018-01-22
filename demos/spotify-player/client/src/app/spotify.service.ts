import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/observable/throw';

import { environment } from '../environments/environment';
import * as models from './spotify.model';

@Injectable()
export class SpotifyService {
  basePath = environment.spotify;

  constructor(private http: HttpClient) {
  }

  getAlbumTracks(id: string | null) {
    let headers = new HttpHeaders();

    headers = headers.set('Accept', 'application/json');

    return this.http.request('get', Location.joinWithSlash(`${this.basePath}`, `albums/${id}/tracks`), {
      observe: 'response',
      headers
    })
    .map(response => {
      switch (response.status) {
        case 200: {
          return response.body;
        }
      }
    })
  }

  getNewReleases() {
    let headers = new HttpHeaders();

    headers = headers.set('Accept', 'application/json');

    return this.http.request('get', Location.joinWithSlash(`${this.basePath}`, `browse/new-releases`), {
      observe: 'response',
      headers
    })
    .map(response => {
      switch (response.status) {
        case 200: {
          return response.body;
        }
      }
    })
  }
}
