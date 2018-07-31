import { Component, Injector } from '@angular/core';
import { TerritoriesGenerated } from './territories-generated.component';

@Component({
  selector: 'territories',
  templateUrl: './territories.component.html'
})
export class TerritoriesComponent extends TerritoriesGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
