import { Component, Injector } from '@angular/core';
import { OrderDetailsByOrderIdGenerated } from './order-details-by-order-id-generated.component';

@Component({
  selector: 'order-details-by-order-id',
  templateUrl: './order-details-by-order-id.component.html'
})
export class OrderDetailsByOrderIdComponent extends OrderDetailsByOrderIdGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
