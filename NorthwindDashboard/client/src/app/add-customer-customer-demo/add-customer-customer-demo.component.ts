import { Component, Injector } from '@angular/core';
import { AddCustomerCustomerDemoGenerated } from './add-customer-customer-demo-generated.component';

@Component({
  selector: 'add-customer-customer-demo',
  templateUrl: './add-customer-customer-demo.component.html'
})
export class AddCustomerCustomerDemoComponent extends AddCustomerCustomerDemoGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
