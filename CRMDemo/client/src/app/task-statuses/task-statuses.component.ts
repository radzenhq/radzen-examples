import { Component, Injector } from '@angular/core';
import { TaskStatusesGenerated } from './task-statuses-generated.component';

@Component({
  selector: 'page-task-statuses',
  templateUrl: './task-statuses.component.html'
})
export class TaskStatusesComponent extends TaskStatusesGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
