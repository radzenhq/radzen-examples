import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ActivatedRoute } from '@angular/router';

import { PageTitleComponent } from './app.component';
import { NewReleasesComponent } from './new-releases/new-releases.component';
import { TracksComponent } from './tracks/tracks.component';


export const routes: Routes = [
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
    path: 'new-releases',
    component: NewReleasesComponent,
    outlet: 'popup',
    data: {
      title: 'New Releases'
    }
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
  {
    path: 'tracks/:id',
    component: TracksComponent,
    outlet: 'popup',
    data: {
      title: 'Tracks'
    }
  },
  { path: '', redirectTo: '/new-releases', pathMatch: 'full' }
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
