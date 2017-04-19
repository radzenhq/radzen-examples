import { Injectable } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { URLSearchParams } from '@angular/http';

@Injectable()
export class SampleAuthorizationService {
  login() {
    const redirectUrl = encodeURIComponent(location.href.replace(this.router.url, '/'));

    const scopes = encodeURIComponent('openid');

    const url = `https://radzen.auth0.com/authorize?client_id=YxejI6yr9SVg8tuQ54ZcDNUdmQYJPijQ&response_type=token&scope=${scopes}&redirect_uri=${redirectUrl}`;

    location.href = url
  }

  get token(): string {
    const token = sessionStorage['SAMPLE_TOKEN'];

    if (!token) {
      this.login();
    }

    return token;
  }

  set token(value: string) {
    sessionStorage['SAMPLE_TOKEN'] = value;
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
