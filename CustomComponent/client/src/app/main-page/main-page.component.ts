import { Component, Injector } from '@angular/core';
import { MainPageGenerated } from './main-page-generated.component';

@Component({
  selector: 'main-page',
  templateUrl: './main-page.component.html'
})
export class MainPageComponent extends MainPageGenerated {
  constructor(injector: Injector) {
    super(injector);
  }

  onNavigate() {
    location.href = 'http://www.radzen.com';
  }
}
