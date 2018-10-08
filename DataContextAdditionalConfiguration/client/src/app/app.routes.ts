import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ActivatedRoute } from '@angular/router';

import { LoginLayoutComponent } from './login-layout/login-layout.component';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { MainPageComponent } from './main-page/main-page.component';

export const routes: Routes = [
  { path: '', redirectTo: '/main-page', pathMatch: 'full' },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'main-page',
        component: MainPageComponent
      },
    ]
  },
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
