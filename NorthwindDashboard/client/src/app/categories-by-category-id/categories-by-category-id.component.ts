import { Component, Injector } from '@angular/core';
import { CategoriesByCategoryIdGenerated } from './categories-by-category-id-generated.component';

@Component({
  selector: 'categories-by-category-id',
  templateUrl: './categories-by-category-id.component.html'
})
export class CategoriesByCategoryIdComponent extends CategoriesByCategoryIdGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
