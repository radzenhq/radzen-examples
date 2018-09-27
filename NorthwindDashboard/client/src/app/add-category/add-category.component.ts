import { Component, Injector } from '@angular/core';
import { AddCategoryGenerated } from './add-category-generated.component';

@Component({
  selector: 'add-category',
  templateUrl: './add-category.component.html'
})
export class AddCategoryComponent extends AddCategoryGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
