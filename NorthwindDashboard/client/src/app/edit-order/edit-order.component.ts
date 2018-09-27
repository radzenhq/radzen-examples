import { Component, Injector } from '@angular/core';
import { EditOrderGenerated } from './edit-order-generated.component';

@Component({
  selector: 'edit-order',
  templateUrl: './edit-order.component.html'
})
export class EditOrderComponent extends EditOrderGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
