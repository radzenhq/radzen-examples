import { Component, Injector } from '@angular/core';
import { EditTaskGenerated } from './edit-task-generated.component';

@Component({
  selector: 'page-edit-task',
  templateUrl: './edit-task.component.html'
})
export class EditTaskComponent extends EditTaskGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
