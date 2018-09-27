import { Component, Injector } from '@angular/core';
import { AddOrderGenerated } from './add-order-generated.component';

@Component({
  selector: 'add-order',
  templateUrl: './add-order.component.html'
})
export class AddOrderComponent extends AddOrderGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
