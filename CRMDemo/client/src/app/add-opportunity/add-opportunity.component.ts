import { Component, Injector } from '@angular/core';
import { AddOpportunityGenerated } from './add-opportunity-generated.component';

@Component({
  selector: 'page-add-opportunity',
  templateUrl: './add-opportunity.component.html'
})
export class AddOpportunityComponent extends AddOpportunityGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
