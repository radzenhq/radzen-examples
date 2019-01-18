import { Component, Injector } from '@angular/core';
import { EditTaskTypeGenerated } from './edit-task-type-generated.component';

@Component({
  selector: 'page-edit-task-type',
  templateUrl: './edit-task-type.component.html'
})
export class EditTaskTypeComponent extends EditTaskTypeGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
