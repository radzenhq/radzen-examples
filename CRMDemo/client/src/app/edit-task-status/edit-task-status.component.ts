import { Component, Injector } from '@angular/core';
import { EditTaskStatusGenerated } from './edit-task-status-generated.component';

@Component({
  selector: 'page-edit-task-status',
  templateUrl: './edit-task-status.component.html'
})
export class EditTaskStatusComponent extends EditTaskStatusGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
