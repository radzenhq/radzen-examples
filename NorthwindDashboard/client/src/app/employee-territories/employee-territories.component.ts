import { Component, Injector } from '@angular/core';
import { EmployeeTerritoriesGenerated } from './employee-territories-generated.component';

@Component({
  selector: 'employee-territories',
  templateUrl: './employee-territories.component.html'
})
export class EmployeeTerritoriesComponent extends EmployeeTerritoriesGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
