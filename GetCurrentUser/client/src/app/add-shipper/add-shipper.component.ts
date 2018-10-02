import { Component, Injector } from '@angular/core';
import { AddShipperGenerated } from './add-shipper-generated.component';

@Component({
  selector: 'page-add-shipper',
  templateUrl: './add-shipper.component.html'
})
export class AddShipperComponent extends AddShipperGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
