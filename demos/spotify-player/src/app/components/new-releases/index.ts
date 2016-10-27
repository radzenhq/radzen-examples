import { Component, Injector } from '@angular/core';
import { NewReleasesGenerated } from './generated';

@Component({
  selector: 'new-releases',
  template: require('./index.html')
})
export class NewReleasesComponent extends NewReleasesGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
