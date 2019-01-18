import { Component, Injector } from '@angular/core';
import { AddTaskGenerated } from './add-task-generated.component';

@Component({
  selector: 'page-add-task',
  templateUrl: './add-task.component.html'
})
export class AddTaskComponent extends AddTaskGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
