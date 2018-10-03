import { HostListener, Component, Input } from '@angular/core';
import { Location } from '@angular/common';

import { SpotifyAuthorizationService } from './spotify-auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {

  constructor (spotify: SpotifyAuthorizationService,private location: Location) {
  }
}
