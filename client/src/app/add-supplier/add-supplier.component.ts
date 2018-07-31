import { Component, Injector } from '@angular/core';
import { AddSupplierGenerated } from './add-supplier-generated.component';

@Component({
  selector: 'add-supplier',
  templateUrl: './add-supplier.component.html'
})
export class AddSupplierComponent extends AddSupplierGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
