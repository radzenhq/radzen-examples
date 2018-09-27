import { Component, Injector } from '@angular/core';
import { EmployeesByEmployeeIdGenerated } from './employees-by-employee-id-generated.component';

@Component({
  selector: 'employees-by-employee-id',
  templateUrl: './employees-by-employee-id.component.html'
})
export class EmployeesByEmployeeIdComponent extends EmployeesByEmployeeIdGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
