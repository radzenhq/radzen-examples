import { Component, Injector } from '@angular/core';
import { AddTerritoryGenerated } from './add-territory-generated.component';

@Component({
  selector: 'page-add-territory',
  templateUrl: './add-territory.component.html'
})
export class AddTerritoryComponent extends AddTerritoryGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
