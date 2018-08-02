import { Component, Injector } from '@angular/core';
import { OrdersByOrderIdGenerated } from './orders-by-order-id-generated.component';

@Component({
  selector: 'orders-by-order-id',
  templateUrl: './orders-by-order-id.component.html'
})
export class OrdersByOrderIdComponent extends OrdersByOrderIdGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
