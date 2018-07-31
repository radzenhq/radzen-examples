import { Component, Injector } from '@angular/core';
import { NorthwindOrderDetailsGenerated } from './northwind-order-details-generated.component';

@Component({
  selector: 'northwind-order-details',
  templateUrl: './northwind-order-details.component.html'
})
export class NorthwindOrderDetailsComponent extends NorthwindOrderDetailsGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
