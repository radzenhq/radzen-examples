import { Component, Injector } from '@angular/core';
import { EditNorthwindOrderGenerated } from './edit-northwind-order-generated.component';

@Component({
  selector: 'edit-northwind-order',
  templateUrl: './edit-northwind-order.component.html'
})
export class EditNorthwindOrderComponent extends EditNorthwindOrderGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
