import { Component, Injector } from '@angular/core';
import { RegionsByRegionIdGenerated } from './regions-by-region-id-generated.component';

@Component({
  selector: 'regions-by-region-id',
  templateUrl: './regions-by-region-id.component.html'
})
export class RegionsByRegionIdComponent extends RegionsByRegionIdGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
