import { Component, Injector } from '@angular/core';
import { EditContactGenerated } from './edit-contact-generated.component';

@Component({
  selector: 'page-edit-contact',
  templateUrl: './edit-contact.component.html'
})
export class EditContactComponent extends EditContactGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
