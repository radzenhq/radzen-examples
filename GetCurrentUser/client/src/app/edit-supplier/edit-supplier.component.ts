import { Component, Injector } from '@angular/core';
import { EditSupplierGenerated } from './edit-supplier-generated.component';

@Component({
  selector: 'page-edit-supplier',
  templateUrl: './edit-supplier.component.html'
})
export class EditSupplierComponent extends EditSupplierGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
