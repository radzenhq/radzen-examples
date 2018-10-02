import { Component, Injector } from '@angular/core';
import { MaiPageGenerated } from './mai-page-generated.component';

@Component({
  selector: 'page-mai-page',
  templateUrl: './mai-page.component.html'
})
export class MaiPageComponent extends MaiPageGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
