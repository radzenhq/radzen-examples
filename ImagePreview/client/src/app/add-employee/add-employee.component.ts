import { Component, Injector } from '@angular/core';
import { AddEmployeeGenerated } from './add-employee-generated.component';

@Component({
  selector: 'add-employee',
  templateUrl: './add-employee.component.html'
})
export class AddEmployeeComponent extends AddEmployeeGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
