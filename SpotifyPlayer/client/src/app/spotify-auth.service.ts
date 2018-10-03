import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { URLSearchParams } from '@angular/http';

@Injectable()
export class SpotifyAuthorizationService {
  constructor(private router: Router) {
    const params = new URLSearchParams(location.hash.replace('#', ''));
    const accessToken = params.get('access_token') || params.get('id_token');

    if (accessToken && this.currentSession == 'Spotify') {
      this.accessToken = decodeURIComponent(accessToken);
    }
  }

  login() {
    this.currentSession = 'Spotify';

    const redirectUrl = encodeURIComponent(location.href.replace(this.router.url, '/'));

    location.replace(`https://accounts.spotify.com/authorize?client_id=da4bd9113dec43578cca7c59c6bf6e44&scope=&response_type=token&redirect_uri=${redirectUrl}`);
  }

  get accessToken(): string {
    return sessionStorage.getItem('SPOTIFY_ACCESS_TOKEN');
  }

  set accessToken(value: string) {
    sessionStorage.setItem('SPOTIFY_ACCESS_TOKEN', value);
  }

  private get currentSession(): string {
    return sessionStorage.getItem('OAUTH_SESSION');
  }

  private set currentSession(value: string) {
    sessionStorage.setItem('OAUTH_SESSION', value);
  }
}
