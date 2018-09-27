import { Component, Injector } from '@angular/core';
import { EditEmployeeGenerated } from './edit-employee-generated.component';

@Component({
  selector: 'edit-employee',
  templateUrl: './edit-employee.component.html'
})
export class EditEmployeeComponent extends EditEmployeeGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
