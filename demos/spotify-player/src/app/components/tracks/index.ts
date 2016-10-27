import { Component, Injector } from '@angular/core';
import { TracksGenerated } from './generated';

@Component({
  selector: 'tracks',
  template: require('./index.html')
})
export class TracksComponent extends TracksGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
