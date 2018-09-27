import { Component, Injector } from '@angular/core';
import { OrdersByEmployeeIdGenerated } from './orders-by-employee-id-generated.component';

@Component({
  selector: 'orders-by-employee-id',
  templateUrl: './orders-by-employee-id.component.html'
})
export class OrdersByEmployeeIdComponent extends OrdersByEmployeeIdGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
