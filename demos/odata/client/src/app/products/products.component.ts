import { Component, Injector } from '@angular/core';
import { ProductsGenerated } from './products-generated.component';

@Component({
  selector: 'products',
  templateUrl: './products.component.html'
})
export class ProductsComponent extends ProductsGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
