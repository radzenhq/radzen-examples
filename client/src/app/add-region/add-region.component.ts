import { Component, Injector } from '@angular/core';
import { AddRegionGenerated } from './add-region-generated.component';

@Component({
  selector: 'add-region',
  templateUrl: './add-region.component.html'
})
export class AddRegionComponent extends AddRegionGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
