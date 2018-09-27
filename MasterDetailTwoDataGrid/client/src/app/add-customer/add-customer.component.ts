import { Component, Injector } from '@angular/core';
import { AddCustomerGenerated } from './add-customer-generated.component';

@Component({
  selector: 'add-customer',
  templateUrl: './add-customer.component.html'
})
export class AddCustomerComponent extends AddCustomerGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
