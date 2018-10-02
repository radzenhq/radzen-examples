import { Component, Injector } from '@angular/core';
import { CustomersGenerated } from './customers-generated.component';

@Component({
  selector: 'page-customers',
  templateUrl: './customers.component.html'
})
export class CustomersComponent extends CustomersGenerated {
  constructor(injector: Injector) {
    super(injector);
  }

  filter(filters) {
    if (this.City) {
      filters = filters ? 
        `${filters} ${this.operator} contains(City,'${this.City}')` : 
        `contains(City,'${this.City}')`;
    }

    if (this.Country) {
      filters = filters ? 
        `${filters} ${this.operator} contains(Country,'${this.Country}')` : 
        `contains(Country,'${this.Country}')`;
    }

    return filters;
  }
}
