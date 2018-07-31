import { Component, Injector } from '@angular/core';
import { AddNorthwindOrderDetailGenerated } from './add-northwind-order-detail-generated.component';

@Component({
  selector: 'add-northwind-order-detail',
  templateUrl: './add-northwind-order-detail.component.html'
})
export class AddNorthwindOrderDetailComponent extends AddNorthwindOrderDetailGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
