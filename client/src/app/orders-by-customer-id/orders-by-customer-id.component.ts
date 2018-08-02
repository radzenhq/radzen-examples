import { Component, Injector } from '@angular/core';
import { OrdersByCustomerIdGenerated } from './orders-by-customer-id-generated.component';

@Component({
  selector: 'orders-by-customer-id',
  templateUrl: './orders-by-customer-id.component.html'
})
export class OrdersByCustomerIdComponent extends OrdersByCustomerIdGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
