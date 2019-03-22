import { Component, Injector } from '@angular/core';
import { AddApplicationRoleGenerated } from './add-application-role-generated.component';

@Component({
  selector: 'page-add-application-role',
  templateUrl: './add-application-role.component.html'
})
export class AddApplicationRoleComponent extends AddApplicationRoleGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
