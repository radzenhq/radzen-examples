import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ActivatedRoute } from '@angular/router';

import { PageTitleComponent } from './app.component';
import { NewReleasesComponent } from './new-releases/new-releases.component';
import { TracksComponent } from './tracks/tracks.component';

export const pageRoutes: Routes = [
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
          title: 'New Releases',
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
          title: 'Tracks',
        }
      }
    ]
  },
  { path: '', redirectTo: '/new-releases', pathMatch: 'full' }
];

export const popupRoutes: Routes = [
  {
    path: 'new-releases',
    component: NewReleasesComponent,
    outlet: 'popup',
    data: {
      title: 'New Releases'
    }
  },
  {
    path: 'tracks/:id',
    component: TracksComponent,
    outlet: 'popup',
    data: {
      title: 'Tracks'
    }
  },
];

export const routes: Routes = [
  ...pageRoutes,
  ...popupRoutes,
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
