import { Component, Injector } from '@angular/core';
import { EditCustomerGenerated } from './edit-customer-generated.component';

@Component({
  selector: 'edit-customer',
  templateUrl: './edit-customer.component.html'
})
export class EditCustomerComponent extends EditCustomerGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
