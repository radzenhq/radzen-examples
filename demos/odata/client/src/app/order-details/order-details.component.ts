import { Component, Injector } from '@angular/core';
import { OrderDetailsGenerated } from './order-details-generated.component';

@Component({
  selector: 'order-details',
  templateUrl: './order-details.component.html'
})
export class OrderDetailsComponent extends OrderDetailsGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
