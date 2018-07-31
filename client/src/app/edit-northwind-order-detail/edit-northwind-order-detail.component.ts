import { Component, Injector } from '@angular/core';
import { EditNorthwindOrderDetailGenerated } from './edit-northwind-order-detail-generated.component';

@Component({
  selector: 'edit-northwind-order-detail',
  templateUrl: './edit-northwind-order-detail.component.html'
})
export class EditNorthwindOrderDetailComponent extends EditNorthwindOrderDetailGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
