import { Component, Injector } from '@angular/core';
import { AddEmployeeTerritoryGenerated } from './add-employee-territory-generated.component';

@Component({
  selector: 'add-employee-territory',
  templateUrl: './add-employee-territory.component.html'
})
export class AddEmployeeTerritoryComponent extends AddEmployeeTerritoryGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
