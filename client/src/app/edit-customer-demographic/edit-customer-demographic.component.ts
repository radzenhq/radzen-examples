import { Component, Injector } from '@angular/core';
import { EditCustomerDemographicGenerated } from './edit-customer-demographic-generated.component';

@Component({
  selector: 'edit-customer-demographic',
  templateUrl: './edit-customer-demographic.component.html'
})
export class EditCustomerDemographicComponent extends EditCustomerDemographicGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
