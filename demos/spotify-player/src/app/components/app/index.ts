import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { NewReleasesComponent } from '../new-releases';
import { TracksComponent } from '../tracks';

@Component({
  selector: 'page-title',
  template: '{{ (route.data | async).title }}'
})
export class PageTitleComponent {
  constructor(private route: ActivatedRoute) {
  }
}

export const routes = [
  {
    path: 'new-releases',
    children: [
      {
        path: '',
        component: NewReleasesComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'New Releases'
        }
      }
    ]
  },
  {
    path: 'tracks',
    children: [
      {
        path: ':id',
        component: TracksComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Tracks'
        }
      }
    ]
  },
  { path: '', redirectTo: '/new-releases', pathMatch: 'full' }
];

@Component({
  selector: 'my-app',
  template: require('./index.html')
})
export class AppComponent {
}
