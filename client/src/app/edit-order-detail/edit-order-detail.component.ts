import { Component, Injector } from '@angular/core';
import { EditOrderDetailGenerated } from './edit-order-detail-generated.component';

@Component({
  selector: 'edit-order-detail',
  templateUrl: './edit-order-detail.component.html'
})
export class EditOrderDetailComponent extends EditOrderDetailGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
