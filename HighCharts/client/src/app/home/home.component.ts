import { Component, Injector } from '@angular/core';
import { HomeGenerated } from './home-generated.component';

@Component({
  selector: 'page-home',
  templateUrl: './home.component.html'
})
export class HomeComponent extends HomeGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
