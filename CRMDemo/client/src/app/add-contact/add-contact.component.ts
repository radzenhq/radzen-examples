import { Component, Injector } from '@angular/core';
import { AddContactGenerated } from './add-contact-generated.component';

@Component({
  selector: 'page-add-contact',
  templateUrl: './add-contact.component.html'
})
export class AddContactComponent extends AddContactGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
