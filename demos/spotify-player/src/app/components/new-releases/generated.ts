import { Injector } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

import { SpotifyService } from '../../services/spotify';

const { components } = require('../../../../meta/pages/new-releases.json');

export class NewReleasesGenerated {
  components = components;

  messages = [];

  router: Router;

  spotify: SpotifyService;

  releases: BehaviorSubject<any>;

  parameters: BehaviorSubject<any>;

  ngOnInit() {
    this.load();
  }

  load() {
    this.spotify.getNewReleases()
    .then(result => {
      if (this.releases) {
        this.releases.next(result.albums.items);
      } else {
        this.releases = new BehaviorSubject(result.albums.items);
      }
    }, result => {

    });
  }

  grid0Select(event: any) {
    this.router.navigate(['tracks', event.id])
  }

  constructor(injector: Injector) {
    this.router = injector.get(Router);

    const route = injector.get(ActivatedRoute);

    this.parameters = new BehaviorSubject(route.snapshot.params);

    this.spotify = injector.get(SpotifyService);
  }
}
