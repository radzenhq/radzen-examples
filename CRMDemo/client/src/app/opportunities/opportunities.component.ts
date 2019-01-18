import { Component, Injector } from '@angular/core';
import { OpportunitiesGenerated } from './opportunities-generated.component';

@Component({
  selector: 'page-opportunities',
  templateUrl: './opportunities.component.html'
})
export class OpportunitiesComponent extends OpportunitiesGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
