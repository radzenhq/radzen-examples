import { Component, Injector } from '@angular/core';
import { EditRegionGenerated } from './edit-region-generated.component';

@Component({
  selector: 'edit-region',
  templateUrl: './edit-region.component.html'
})
export class EditRegionComponent extends EditRegionGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
