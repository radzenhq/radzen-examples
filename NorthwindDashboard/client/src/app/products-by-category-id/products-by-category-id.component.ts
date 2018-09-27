import { Component, Injector } from '@angular/core';
import { ProductsByCategoryIdGenerated } from './products-by-category-id-generated.component';

@Component({
  selector: 'products-by-category-id',
  templateUrl: './products-by-category-id.component.html'
})
export class ProductsByCategoryIdComponent extends ProductsByCategoryIdGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
