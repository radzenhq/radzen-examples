import { Component, Injector } from '@angular/core';
import { EditOpportunityGenerated } from './edit-opportunity-generated.component';

@Component({
  selector: 'page-edit-opportunity',
  templateUrl: './edit-opportunity.component.html'
})
export class EditOpportunityComponent extends EditOpportunityGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
