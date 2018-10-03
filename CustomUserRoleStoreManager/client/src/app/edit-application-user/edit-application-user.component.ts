import { Component, Injector } from '@angular/core';
import { EditApplicationUserGenerated } from './edit-application-user-generated.component';

@Component({
  selector: 'page-edit-application-user',
  templateUrl: './edit-application-user.component.html'
})
export class EditApplicationUserComponent extends EditApplicationUserGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
