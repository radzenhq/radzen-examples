import { Component, Injector } from '@angular/core';
import { DashboardGenerated } from './dashboard-generated.component';

@Component({
  selector: 'dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent extends DashboardGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
