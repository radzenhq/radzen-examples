import { Component, Injector } from '@angular/core';
import { NewReleasesGenerated } from './new-releases-generated.component';

@Component({
  selector: 'new-releases',
  templateUrl: './new-releases.component.html'
})
export class NewReleasesComponent extends NewReleasesGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
