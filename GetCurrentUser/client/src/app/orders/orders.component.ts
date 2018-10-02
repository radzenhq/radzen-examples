import { Component, Injector } from '@angular/core';
import { OrdersGenerated } from './orders-generated.component';

@Component({
  selector: 'page-orders',
  templateUrl: './orders.component.html'
})
export class OrdersComponent extends OrdersGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
