import { Component, Injector } from '@angular/core';
import { TerritoriesByRegionIdGenerated } from './territories-by-region-id-generated.component';

@Component({
  selector: 'territories-by-region-id',
  templateUrl: './territories-by-region-id.component.html'
})
export class TerritoriesByRegionIdComponent extends TerritoriesByRegionIdGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
