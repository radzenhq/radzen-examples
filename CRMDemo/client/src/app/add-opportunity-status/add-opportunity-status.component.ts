import { Component, Injector } from '@angular/core';
import { AddOpportunityStatusGenerated } from './add-opportunity-status-generated.component';

@Component({
  selector: 'page-add-opportunity-status',
  templateUrl: './add-opportunity-status.component.html'
})
export class AddOpportunityStatusComponent extends AddOpportunityStatusGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
