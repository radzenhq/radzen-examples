import { Component, Injector } from '@angular/core';
import { OrdersByProductIdGenerated } from './orders-by-product-id-generated.component';

@Component({
  selector: 'orders-by-product-id',
  templateUrl: './orders-by-product-id.component.html'
})
export class OrdersByProductIdComponent extends OrdersByProductIdGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
