import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ActivatedRoute } from '@angular/router';

import { LoginLayoutComponent } from './login-layout/login-layout.component';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { LoginComponent } from './login/login.component';
import { ApplicationUsersComponent } from './application-users/application-users.component';
import { ApplicationRolesComponent } from './application-roles/application-roles.component';
import { AddApplicationRoleComponent } from './add-application-role/add-application-role.component';
import { AddApplicationUserComponent } from './add-application-user/add-application-user.component';
import { RegisterApplicationUserComponent } from './register-application-user/register-application-user.component';
import { ProfileComponent } from './profile/profile.component';
import { EditApplicationUserComponent } from './edit-application-user/edit-application-user.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { MaiPageComponent } from './mai-page/mai-page.component';

import { SecurityService } from './security.service';
import { AuthGuard } from './auth.guard';
export const routes: Routes = [
  { path: '', redirectTo: '/mai-page', pathMatch: 'full' },
  {
    path: '',
    component: LoginLayoutComponent,
    children: [
      {
        path: 'login',
        data: {
          roles: ['Everybody'],
        },
        component: LoginComponent
      },
    ]
  },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'application-users',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: ApplicationUsersComponent
      },
      {
        path: 'application-roles',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: ApplicationRolesComponent
      },
      {
        path: 'add-application-role',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddApplicationRoleComponent
      },
      {
        path: 'add-application-user',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddApplicationUserComponent
      },
      {
        path: 'register-application-user',
        data: {
          roles: ['Everybody'],
        },
        component: RegisterApplicationUserComponent
      },
      {
        path: 'profile',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: ProfileComponent
      },
      {
        path: 'edit-application-user/:Id',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditApplicationUserComponent
      },
      {
        path: 'unauthorized',
        data: {
          roles: ['Everybody'],
        },
        component: UnauthorizedComponent
      },
      {
        path: 'mai-page',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: MaiPageComponent
      },
    ]
  },
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
