import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ActivatedRoute } from '@angular/router';

import { LoginLayoutComponent } from './login-layout/login-layout.component';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { OrderComponent } from './order/order.component';
import { AddOrderDetailComponent } from './add-order-detail/add-order-detail.component';
import { AddOrderComponent } from './add-order/add-order.component';
import { EditOrderComponent } from './edit-order/edit-order.component';
import { EditOrderDetailComponent } from './edit-order-detail/edit-order-detail.component';

export const routes: Routes = [
  { path: '', redirectTo: '/order', pathMatch: 'full' },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'order',
        component: OrderComponent
      },
      {
        path: 'add-order-detail/:OrderId',
        component: AddOrderDetailComponent
      },
      {
        path: 'add-order',
        component: AddOrderComponent
      },
      {
        path: 'edit-order/:Id',
        component: EditOrderComponent
      },
      {
        path: 'edit-order-detail/:Id',
        component: EditOrderDetailComponent
      },
    ]
  },
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
