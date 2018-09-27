import { Component, Injector } from '@angular/core';
import { EditCustomerCustomerDemoGenerated } from './edit-customer-customer-demo-generated.component';

@Component({
  selector: 'edit-customer-customer-demo',
  templateUrl: './edit-customer-customer-demo.component.html'
})
export class EditCustomerCustomerDemoComponent extends EditCustomerCustomerDemoGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
