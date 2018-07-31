import { Component, Injector } from '@angular/core';
import { AddCustomerDemographicGenerated } from './add-customer-demographic-generated.component';

@Component({
  selector: 'add-customer-demographic',
  templateUrl: './add-customer-demographic.component.html'
})
export class AddCustomerDemographicComponent extends AddCustomerDemographicGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
