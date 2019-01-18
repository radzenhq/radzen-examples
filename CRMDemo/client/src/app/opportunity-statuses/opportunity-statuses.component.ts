import { Component, Injector } from '@angular/core';
import { OpportunityStatusesGenerated } from './opportunity-statuses-generated.component';

@Component({
  selector: 'page-opportunity-statuses',
  templateUrl: './opportunity-statuses.component.html'
})
export class OpportunityStatusesComponent extends OpportunityStatusesGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
