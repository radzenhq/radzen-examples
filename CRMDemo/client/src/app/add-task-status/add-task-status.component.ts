import { Component, Injector } from '@angular/core';
import { AddTaskStatusGenerated } from './add-task-status-generated.component';

@Component({
  selector: 'page-add-task-status',
  templateUrl: './add-task-status.component.html'
})
export class AddTaskStatusComponent extends AddTaskStatusGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
