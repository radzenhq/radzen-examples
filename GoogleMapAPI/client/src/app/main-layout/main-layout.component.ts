import { Component, Injector } from '@angular/core';
import { MainLayoutGenerated } from './main-layout-generated.component';

@Component({
  selector: 'page-main-layout',
  templateUrl: './main-layout.component.html'
})
export class MainLayoutComponent extends MainLayoutGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
