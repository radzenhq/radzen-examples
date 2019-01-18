import { Component, Injector } from '@angular/core';
import { TasksGenerated } from './tasks-generated.component';

@Component({
  selector: 'page-tasks',
  templateUrl: './tasks.component.html'
})
export class TasksComponent extends TasksGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
