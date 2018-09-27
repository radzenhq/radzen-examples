import { Component, Injector } from '@angular/core';
import { ApplicationUsersGenerated } from './application-users-generated.component';

@Component({
  selector: 'application-users',
  templateUrl: './application-users.component.html'
})
export class ApplicationUsersComponent extends ApplicationUsersGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
