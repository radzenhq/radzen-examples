import { Component, Injector } from '@angular/core';
import { OrderGenerated } from './order-generated.component';

@Component({
  selector: 'page-order',
  templateUrl: './order.component.html'
})
export class OrderComponent extends OrderGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
