import { Component, Injector } from '@angular/core';
import { ProductsByProductIdGenerated } from './products-by-product-id-generated.component';

@Component({
  selector: 'products-by-product-id',
  templateUrl: './products-by-product-id.component.html'
})
export class ProductsByProductIdComponent extends ProductsByProductIdGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
