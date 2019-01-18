import { Component, Injector } from '@angular/core';
import { ContactsGenerated } from './contacts-generated.component';

@Component({
  selector: 'page-contacts',
  templateUrl: './contacts.component.html'
})
export class ContactsComponent extends ContactsGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
