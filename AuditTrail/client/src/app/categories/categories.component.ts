import { Component, Injector } from '@angular/core';
import { CategoriesGenerated } from './categories-generated.component';

@Component({
  selector: 'page-categories',
  templateUrl: './categories.component.html'
})
export class CategoriesComponent extends CategoriesGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
