import { Component, Injector } from '@angular/core';
import { AddOrderDetailGenerated } from './add-order-detail-generated.component';

@Component({
  selector: 'page-add-order-detail',
  templateUrl: './add-order-detail.component.html'
})
export class AddOrderDetailComponent extends AddOrderDetailGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
