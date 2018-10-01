import { Component, Injector } from '@angular/core';
import { EditShipperGenerated } from './edit-shipper-generated.component';

@Component({
  selector: 'page-edit-shipper',
  templateUrl: './edit-shipper.component.html'
})
export class EditShipperComponent extends EditShipperGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
