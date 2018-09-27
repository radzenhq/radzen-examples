import { Component, Injector } from '@angular/core';
import { CustomersGenerated } from './customers-generated.component';

@Component({
  selector: 'customers',
  templateUrl: './customers.component.html'
})
export class CustomersComponent extends CustomersGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
