import { Component, Injector } from '@angular/core';
import { RegionsGenerated } from './regions-generated.component';

@Component({
  selector: 'regions',
  templateUrl: './regions.component.html'
})
export class RegionsComponent extends RegionsGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
