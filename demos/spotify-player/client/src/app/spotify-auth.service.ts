import { Injectable } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { URLSearchParams } from '@angular/http';

@Injectable()
export class SpotifyAuthorizationService {
  login() {
    const redirectUrl = encodeURIComponent(location.href.replace(this.router.url, '/'));

    const scopes = encodeURIComponent('');

    const url = `https://accounts.spotify.com/authorize?client_id=da4bd9113dec43578cca7c59c6bf6e44&response_type=token&scope=${scopes}&redirect_uri=${redirectUrl}`;

    location.href = url
  }

  get token(): string {
    const token = sessionStorage['SPOTIFY_TOKEN'];

    if (!token) {
      this.login();
    }

    return token;
  }

  set token(value: string) {
    sessionStorage['SPOTIFY_TOKEN'] = value;
  }

  constructor(private router: Router) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        const params = new URLSearchParams(event.url.replace('/#', ''));

        let token = params.get('id_token') || params.get('access_token');

        if (token) {
          this.token = decodeURIComponent(token);
        }
      }
    });
  }
}
