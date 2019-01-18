import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ActivatedRoute } from '@angular/router';

import { LoginLayoutComponent } from './login-layout/login-layout.component';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { ContactsComponent } from './contacts/contacts.component';
import { AddContactComponent } from './add-contact/add-contact.component';
import { EditContactComponent } from './edit-contact/edit-contact.component';
import { OpportunitiesComponent } from './opportunities/opportunities.component';
import { AddOpportunityComponent } from './add-opportunity/add-opportunity.component';
import { EditOpportunityComponent } from './edit-opportunity/edit-opportunity.component';
import { OpportunityStatusesComponent } from './opportunity-statuses/opportunity-statuses.component';
import { AddOpportunityStatusComponent } from './add-opportunity-status/add-opportunity-status.component';
import { EditOpportunityStatusComponent } from './edit-opportunity-status/edit-opportunity-status.component';
import { TasksComponent } from './tasks/tasks.component';
import { AddTaskComponent } from './add-task/add-task.component';
import { EditTaskComponent } from './edit-task/edit-task.component';
import { TaskTypesComponent } from './task-types/task-types.component';
import { AddTaskTypeComponent } from './add-task-type/add-task-type.component';
import { EditTaskTypeComponent } from './edit-task-type/edit-task-type.component';
import { LoginComponent } from './login/login.component';
import { AddApplicationRoleComponent } from './add-application-role/add-application-role.component';
import { ApplicationRolesComponent } from './application-roles/application-roles.component';
import { RegisterApplicationUserComponent } from './register-application-user/register-application-user.component';
import { ApplicationUsersComponent } from './application-users/application-users.component';
import { AddApplicationUserComponent } from './add-application-user/add-application-user.component';
import { EditApplicationUserComponent } from './edit-application-user/edit-application-user.component';
import { ProfileComponent } from './profile/profile.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { TaskStatusesComponent } from './task-statuses/task-statuses.component';
import { AddTaskStatusComponent } from './add-task-status/add-task-status.component';
import { EditTaskStatusComponent } from './edit-task-status/edit-task-status.component';
import { HomeComponent } from './home/home.component';

import { SecurityService } from './security.service';
import { AuthGuard } from './auth.guard';
export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'contacts',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: ContactsComponent
      },
      {
        path: 'add-contact',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddContactComponent
      },
      {
        path: 'edit-contact/:Id',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditContactComponent
      },
      {
        path: 'opportunities',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: OpportunitiesComponent
      },
      {
        path: 'add-opportunity',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddOpportunityComponent
      },
      {
        path: 'edit-opportunity/:Id',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditOpportunityComponent
      },
      {
        path: 'opportunity-statuses',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: OpportunityStatusesComponent
      },
      {
        path: 'add-opportunity-status',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddOpportunityStatusComponent
      },
      {
        path: 'edit-opportunity-status/:Id',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditOpportunityStatusComponent
      },
      {
        path: 'tasks',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: TasksComponent
      },
      {
        path: 'add-task',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddTaskComponent
      },
      {
        path: 'edit-task/:Id',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditTaskComponent
      },
      {
        path: 'task-types',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: TaskTypesComponent
      },
      {
        path: 'add-task-type',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddTaskTypeComponent
      },
      {
        path: 'edit-task-type/:Id',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditTaskTypeComponent
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
        path: 'application-roles',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: ApplicationRolesComponent
      },
      {
        path: 'register-application-user',
        data: {
          roles: ['Everybody'],
        },
        component: RegisterApplicationUserComponent
      },
      {
        path: 'application-users',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: ApplicationUsersComponent
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
        path: 'edit-application-user/:Id',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditApplicationUserComponent
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
        path: 'unauthorized',
        data: {
          roles: ['Everybody'],
        },
        component: UnauthorizedComponent
      },
      {
        path: 'task-statuses',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: TaskStatusesComponent
      },
      {
        path: 'add-task-status',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddTaskStatusComponent
      },
      {
        path: 'edit-task-status/:Id',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditTaskStatusComponent
      },
      {
        path: 'home',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: HomeComponent
      },
    ]
  },
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
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
