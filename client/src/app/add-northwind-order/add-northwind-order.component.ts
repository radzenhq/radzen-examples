import { Component, Injector } from '@angular/core';
import { AddNorthwindOrderGenerated } from './add-northwind-order-generated.component';

@Component({
  selector: 'add-northwind-order',
  templateUrl: './add-northwind-order.component.html'
})
export class AddNorthwindOrderComponent extends AddNorthwindOrderGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
