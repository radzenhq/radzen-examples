import { Component, Injector } from '@angular/core';
import { ShippersGenerated } from './shippers-generated.component';

@Component({
  selector: 'page-shippers',
  templateUrl: './shippers.component.html'
})
export class ShippersComponent extends ShippersGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
