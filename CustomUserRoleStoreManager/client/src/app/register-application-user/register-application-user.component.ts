import { Component, Injector } from '@angular/core';
import { RegisterApplicationUserGenerated } from './register-application-user-generated.component';

@Component({
  selector: 'page-register-application-user',
  templateUrl: './register-application-user.component.html'
})
export class RegisterApplicationUserComponent extends RegisterApplicationUserGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
