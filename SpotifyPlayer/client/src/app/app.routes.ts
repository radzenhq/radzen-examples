import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ActivatedRoute } from '@angular/router';

import { MainLayoutComponent } from './main-layout/main-layout.component';
import { LoginLayoutComponent } from './login-layout/login-layout.component';
import { NewReleasesComponent } from './new-releases/new-releases.component';
import { TracksComponent } from './tracks/tracks.component';

export const routes: Routes = [
  { path: '', redirectTo: '/new-releases', pathMatch: 'full' },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'new-releases',
        component: NewReleasesComponent
      },
      {
        path: 'tracks/:id',
        component: TracksComponent
      },
    ]
  },
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
