import { Component, Injector } from '@angular/core';
import { AddTaskTypeGenerated } from './add-task-type-generated.component';

@Component({
  selector: 'page-add-task-type',
  templateUrl: './add-task-type.component.html'
})
export class AddTaskTypeComponent extends AddTaskTypeGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
