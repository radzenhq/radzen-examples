import { Component, Injector } from '@angular/core';
import { NorthwindOrdersGenerated } from './northwind-orders-generated.component';

@Component({
  selector: 'northwind-orders',
  templateUrl: './northwind-orders.component.html'
})
export class NorthwindOrdersComponent extends NorthwindOrdersGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
