import { Component, Injector } from '@angular/core';
import { NorthwindProductsGenerated } from './northwind-products-generated.component';

@Component({
  selector: 'northwind-products',
  templateUrl: './northwind-products.component.html'
})
export class NorthwindProductsComponent extends NorthwindProductsGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
