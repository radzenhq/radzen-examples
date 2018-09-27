import { Component, Injector } from '@angular/core';
import { EditTerritoryGenerated } from './edit-territory-generated.component';

@Component({
  selector: 'edit-territory',
  templateUrl: './edit-territory.component.html'
})
export class EditTerritoryComponent extends EditTerritoryGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
