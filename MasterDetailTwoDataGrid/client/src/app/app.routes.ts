import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ActivatedRoute } from '@angular/router';

import { LoginLayoutComponent } from './login-layout/login-layout.component';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { CustomersComponent } from './customers/customers.component';
import { AddOrderComponent } from './add-order/add-order.component';
import { AddCustomerComponent } from './add-customer/add-customer.component';
import { EditCustomerComponent } from './edit-customer/edit-customer.component';
import { EditOrderComponent } from './edit-order/edit-order.component';

export const routes: Routes = [
  { path: '', redirectTo: '/customers', pathMatch: 'full' },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'customers',
        component: CustomersComponent
      },
      {
        path: 'add-order/:CustomerID',
        component: AddOrderComponent
      },
      {
        path: 'add-customer',
        component: AddCustomerComponent
      },
      {
        path: 'edit-customer/:CustomerID',
        component: EditCustomerComponent
      },
      {
        path: 'edit-order/:OrderID',
        component: EditOrderComponent
      },
    ]
  },
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
