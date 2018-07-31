import { Component, Injector } from '@angular/core';
import { EditCategoryGenerated } from './edit-category-generated.component';

@Component({
  selector: 'edit-category',
  templateUrl: './edit-category.component.html'
})
export class EditCategoryComponent extends EditCategoryGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
