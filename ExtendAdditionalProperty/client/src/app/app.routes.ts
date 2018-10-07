import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ActivatedRoute } from '@angular/router';

import { LoginLayoutComponent } from './login-layout/login-layout.component';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { AddOrderDetailComponent } from './add-order-detail/add-order-detail.component';
import { EditOrderDetailComponent } from './edit-order-detail/edit-order-detail.component';

export const routes: Routes = [
  { path: '', redirectTo: '/order-details', pathMatch: 'full' },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'order-details',
        component: OrderDetailsComponent
      },
      {
        path: 'add-order-detail',
        component: AddOrderDetailComponent
      },
      {
        path: 'edit-order-detail/:OrderID/:ProductID',
        component: EditOrderDetailComponent
      },
    ]
  },
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
