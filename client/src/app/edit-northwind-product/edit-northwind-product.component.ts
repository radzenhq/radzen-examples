import { Component, Injector } from '@angular/core';
import { EditNorthwindProductGenerated } from './edit-northwind-product-generated.component';

@Component({
  selector: 'edit-northwind-product',
  templateUrl: './edit-northwind-product.component.html'
})
export class EditNorthwindProductComponent extends EditNorthwindProductGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
