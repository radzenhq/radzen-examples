import { Component, Injector } from '@angular/core';
import { EditProductGenerated } from './edit-product-generated.component';

@Component({
  selector: 'edit-product',
  templateUrl: './edit-product.component.html'
})
export class EditProductComponent extends EditProductGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
