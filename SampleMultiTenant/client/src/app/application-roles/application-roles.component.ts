import { Component, Injector } from '@angular/core';
import { ApplicationRolesGenerated } from './application-roles-generated.component';

@Component({
  selector: 'page-application-roles',
  templateUrl: './application-roles.component.html'
})
export class ApplicationRolesComponent extends ApplicationRolesGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
