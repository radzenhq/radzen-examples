import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable()
export class SpotifyAuthorizationService {
  static login() {
    const redirectUrl = encodeURIComponent(`${location.protocol}//${location.host}/`);

    const scopes = encodeURIComponent('');

    const url = `https://accounts.spotify.com/authorize?client_id=da4bd9113dec43578cca7c59c6bf6e44&response_type=token&scope=${scopes}&redirect_uri=${redirectUrl}`;

    location.href = url
  }

  get token(): string {
    return sessionStorage['SPOTIFY_TOKEN'];
  }

  set token(value: string) {
    sessionStorage['SPOTIFY_TOKEN'] = value;
  }

  constructor(router: Router) {
    const href = location.href;

    let token = null;

    if (/access_token=([^&]*)/.test(href)) {
      token = RegExp.$1;
    }

    if (/id_token=([^&]*)/.test(href)) {
      token = RegExp.$1;
    }

    if (token) {
      this.token = decodeURIComponent(token);

      router.navigateByUrl('/');
    }
  }
}
