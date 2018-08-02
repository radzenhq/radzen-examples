import { Component, Injector } from '@angular/core';
import { CustomersByCustomerIdGenerated } from './customers-by-customer-id-generated.component';

@Component({
  selector: 'customers-by-customer-id',
  templateUrl: './customers-by-customer-id.component.html'
})
export class CustomersByCustomerIdComponent extends CustomersByCustomerIdGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
