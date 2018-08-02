import { Component, Injector } from '@angular/core';
import { AddProductGenerated } from './add-product-generated.component';

@Component({
  selector: 'add-product',
  templateUrl: './add-product.component.html'
})
export class AddProductComponent extends AddProductGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
