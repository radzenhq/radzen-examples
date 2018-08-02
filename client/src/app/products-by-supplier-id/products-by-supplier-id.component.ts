import { Component, Injector } from '@angular/core';
import { ProductsBySupplierIdGenerated } from './products-by-supplier-id-generated.component';

@Component({
  selector: 'products-by-supplier-id',
  templateUrl: './products-by-supplier-id.component.html'
})
export class ProductsBySupplierIdComponent extends ProductsBySupplierIdGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
