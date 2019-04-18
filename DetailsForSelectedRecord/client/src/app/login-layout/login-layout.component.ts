import { Component, Injector } from '@angular/core';
import { LoginLayoutGenerated } from './login-layout-generated.component';

@Component({
  selector: 'page-login-layout',
  templateUrl: './login-layout.component.html'
})
export class LoginLayoutComponent extends LoginLayoutGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
