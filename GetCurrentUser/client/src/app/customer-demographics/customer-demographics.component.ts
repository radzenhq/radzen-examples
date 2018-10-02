import { Component, Injector } from '@angular/core';
import { CustomerDemographicsGenerated } from './customer-demographics-generated.component';

@Component({
  selector: 'page-customer-demographics',
  templateUrl: './customer-demographics.component.html'
})
export class CustomerDemographicsComponent extends CustomerDemographicsGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
