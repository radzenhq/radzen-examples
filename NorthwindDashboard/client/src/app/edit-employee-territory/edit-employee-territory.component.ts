import { Component, Injector } from '@angular/core';
import { EditEmployeeTerritoryGenerated } from './edit-employee-territory-generated.component';

@Component({
  selector: 'edit-employee-territory',
  templateUrl: './edit-employee-territory.component.html'
})
export class EditEmployeeTerritoryComponent extends EditEmployeeTerritoryGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
