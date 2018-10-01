import { Component, Injector } from '@angular/core';
import { CustomerCustomerDemosGenerated } from './customer-customer-demos-generated.component';

@Component({
  selector: 'page-customer-customer-demos',
  templateUrl: './customer-customer-demos.component.html'
})
export class CustomerCustomerDemosComponent extends CustomerCustomerDemosGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
