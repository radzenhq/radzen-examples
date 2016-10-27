import { Injector } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

import { SpotifyService } from '../../services/spotify';

const { components } = require('../../../../meta/pages/tracks.json');

export class TracksGenerated {
  components = components;

  messages = [];

  router: Router;

  spotify: SpotifyService;

  tracks: BehaviorSubject<any>;

  parameters: BehaviorSubject<any>;

  ngOnInit() {
    this.load();
  }

  load() {
    this.spotify.getAlbumTracks(`${this.parameters.getValue().id}`)
    .then(result => {
      if (this.tracks) {
        this.tracks.next(result.items);
      } else {
        this.tracks = new BehaviorSubject(result.items);
      }
    }, result => {

    });
  }

  constructor(injector: Injector) {
    this.router = injector.get(Router);

    const route = injector.get(ActivatedRoute);

    this.parameters = new BehaviorSubject(route.snapshot.params);

    this.spotify = injector.get(SpotifyService);
  }
}
