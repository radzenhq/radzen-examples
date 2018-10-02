import { Component, Injector } from '@angular/core';
import { EmployeesGenerated } from './employees-generated.component';

@Component({
  selector: 'page-employees',
  templateUrl: './employees.component.html'
})
export class EmployeesComponent extends EmployeesGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
