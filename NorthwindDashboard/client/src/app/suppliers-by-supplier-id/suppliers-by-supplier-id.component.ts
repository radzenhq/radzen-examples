import { Component, Injector } from '@angular/core';
import { SuppliersBySupplierIdGenerated } from './suppliers-by-supplier-id-generated.component';

@Component({
  selector: 'suppliers-by-supplier-id',
  templateUrl: './suppliers-by-supplier-id.component.html'
})
export class SuppliersBySupplierIdComponent extends SuppliersBySupplierIdGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
