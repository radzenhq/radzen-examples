import { Component, Injector } from '@angular/core';
import { TaskTypesGenerated } from './task-types-generated.component';

@Component({
  selector: 'page-task-types',
  templateUrl: './task-types.component.html'
})
export class TaskTypesComponent extends TaskTypesGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
