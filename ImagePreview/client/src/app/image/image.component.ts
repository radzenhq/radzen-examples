import { Component, Injector } from '@angular/core';
import { ImageGenerated } from './image-generated.component';

@Component({
  selector: 'image',
  templateUrl: './image.component.html'
})
export class ImageComponent extends ImageGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
