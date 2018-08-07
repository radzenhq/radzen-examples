import { HostListener, Component, Input } from '@angular/core';
import { Location } from '@angular/common';

import { SecurityService } from './security.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {

  constructor (public security: SecurityService,private location: Location) {
  }
}
