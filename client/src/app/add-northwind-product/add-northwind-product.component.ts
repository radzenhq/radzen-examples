import { Component, Injector } from '@angular/core';
import { AddNorthwindProductGenerated } from './add-northwind-product-generated.component';

@Component({
  selector: 'add-northwind-product',
  templateUrl: './add-northwind-product.component.html'
})
export class AddNorthwindProductComponent extends AddNorthwindProductGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
