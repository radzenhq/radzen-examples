import { Component, Injector } from '@angular/core';
import { AddApplicationUserGenerated } from './add-application-user-generated.component';

@Component({
  selector: 'page-add-application-user',
  templateUrl: './add-application-user.component.html'
})
export class AddApplicationUserComponent extends AddApplicationUserGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
