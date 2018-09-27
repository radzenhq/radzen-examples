import { Component, Injector } from '@angular/core';
import { ProfileGenerated } from './profile-generated.component';

@Component({
  selector: 'profile',
  templateUrl: './profile.component.html'
})
export class ProfileComponent extends ProfileGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
