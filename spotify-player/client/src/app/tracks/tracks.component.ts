import { Component, Injector } from '@angular/core';
import { TracksGenerated } from './tracks-generated.component';

@Component({
  selector: 'tracks',
  templateUrl: './tracks.component.html'
})
export class TracksComponent extends TracksGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
