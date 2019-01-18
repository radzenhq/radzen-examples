import { Component, Injector } from '@angular/core';
import { EditOpportunityStatusGenerated } from './edit-opportunity-status-generated.component';

@Component({
  selector: 'page-edit-opportunity-status',
  templateUrl: './edit-opportunity-status.component.html'
})
export class EditOpportunityStatusComponent extends EditOpportunityStatusGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
