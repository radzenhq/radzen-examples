import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ActivatedRoute } from '@angular/router';

import { LoginLayoutComponent } from './login-layout/login-layout.component';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { CategoriesComponent } from './categories/categories.component';
import { AddCategoryComponent } from './add-category/add-category.component';
import { EditCategoryComponent } from './edit-category/edit-category.component';
import { CustomersComponent } from './customers/customers.component';
import { AddCustomerComponent } from './add-customer/add-customer.component';
import { EditCustomerComponent } from './edit-customer/edit-customer.component';
import { EmployeesComponent } from './employees/employees.component';
import { AddEmployeeComponent } from './add-employee/add-employee.component';
import { EditEmployeeComponent } from './edit-employee/edit-employee.component';
import { OrdersComponent } from './orders/orders.component';
import { AddOrderComponent } from './add-order/add-order.component';
import { EditOrderComponent } from './edit-order/edit-order.component';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { AddOrderDetailComponent } from './add-order-detail/add-order-detail.component';
import { EditOrderDetailComponent } from './edit-order-detail/edit-order-detail.component';
import { ProductsComponent } from './products/products.component';
import { AddProductComponent } from './add-product/add-product.component';
import { EditProductComponent } from './edit-product/edit-product.component';
import { SuppliersComponent } from './suppliers/suppliers.component';
import { AddSupplierComponent } from './add-supplier/add-supplier.component';
import { EditSupplierComponent } from './edit-supplier/edit-supplier.component';
import { OrderDetailsByOrderIdComponent } from './order-details-by-order-id/order-details-by-order-id.component';
import { OrdersByOrderIdComponent } from './orders-by-order-id/orders-by-order-id.component';
import { OrdersByCustomerIdComponent } from './orders-by-customer-id/orders-by-customer-id.component';
import { ProductsByCategoryIdComponent } from './products-by-category-id/products-by-category-id.component';
import { CategoriesByCategoryIdComponent } from './categories-by-category-id/categories-by-category-id.component';
import { CustomersByCustomerIdComponent } from './customers-by-customer-id/customers-by-customer-id.component';
import { OrdersByEmployeeIdComponent } from './orders-by-employee-id/orders-by-employee-id.component';
import { EmployeesByEmployeeIdComponent } from './employees-by-employee-id/employees-by-employee-id.component';
import { ProductsByProductIdComponent } from './products-by-product-id/products-by-product-id.component';
import { SuppliersBySupplierIdComponent } from './suppliers-by-supplier-id/suppliers-by-supplier-id.component';
import { ProductsBySupplierIdComponent } from './products-by-supplier-id/products-by-supplier-id.component';
import { LoginComponent } from './login/login.component';
import { AddApplicationRoleComponent } from './add-application-role/add-application-role.component';
import { AddApplicationUserComponent } from './add-application-user/add-application-user.component';
import { ProfileComponent } from './profile/profile.component';
import { ApplicationRolesComponent } from './application-roles/application-roles.component';
import { ApplicationUsersComponent } from './application-users/application-users.component';
import { RegisterApplicationUserComponent } from './register-application-user/register-application-user.component';
import { EditApplicationUserComponent } from './edit-application-user/edit-application-user.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { OrdersByProductIdComponent } from './orders-by-product-id/orders-by-product-id.component';

import { SecurityService } from './security.service';
import { AuthGuard } from './auth.guard';
export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'categories',
        data: {
          roles: ['Everybody'],
        },
        component: CategoriesComponent
      },
      {
        path: 'add-category',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddCategoryComponent
      },
      {
        path: 'edit-category/:CategoryID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditCategoryComponent
      },
      {
        path: 'customers',
        data: {
          roles: ['Everybody'],
        },
        component: CustomersComponent
      },
      {
        path: 'add-customer',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddCustomerComponent
      },
      {
        path: 'edit-customer/:CustomerID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditCustomerComponent
      },
      {
        path: 'employees',
        data: {
          roles: ['Everybody'],
        },
        component: EmployeesComponent
      },
      {
        path: 'add-employee',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddEmployeeComponent
      },
      {
        path: 'edit-employee/:EmployeeID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditEmployeeComponent
      },
      {
        path: 'orders',
        data: {
          roles: ['Everybody'],
        },
        component: OrdersComponent
      },
      {
        path: 'add-order',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddOrderComponent
      },
      {
        path: 'edit-order/:OrderID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditOrderComponent
      },
      {
        path: 'order-details',
        data: {
          roles: ['Everybody'],
        },
        component: OrderDetailsComponent
      },
      {
        path: 'add-order-detail',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddOrderDetailComponent
      },
      {
        path: 'edit-order-detail/:OrderID/:ProductID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditOrderDetailComponent
      },
      {
        path: 'products',
        data: {
          roles: ['Everybody'],
        },
        component: ProductsComponent
      },
      {
        path: 'add-product',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddProductComponent
      },
      {
        path: 'edit-product/:ProductID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditProductComponent
      },
      {
        path: 'suppliers',
        data: {
          roles: ['Everybody'],
        },
        component: SuppliersComponent
      },
      {
        path: 'add-supplier',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddSupplierComponent
      },
      {
        path: 'edit-supplier/:SupplierID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditSupplierComponent
      },
      {
        path: 'order-details-by-order-id/:OrderID',
        data: {
          roles: ['Everybody'],
        },
        component: OrderDetailsByOrderIdComponent
      },
      {
        path: 'orders-by-order-id/:OrderID',
        data: {
          roles: ['Everybody'],
        },
        component: OrdersByOrderIdComponent
      },
      {
        path: 'orders-by-customer-id/:CustomerID',
        data: {
          roles: ['Everybody'],
        },
        component: OrdersByCustomerIdComponent
      },
      {
        path: 'products-by-category-id/:CategoryID',
        data: {
          roles: ['Everybody'],
        },
        component: ProductsByCategoryIdComponent
      },
      {
        path: 'categories-by-category-id/:CategoryID',
        data: {
          roles: ['Everybody'],
        },
        component: CategoriesByCategoryIdComponent
      },
      {
        path: 'customers-by-customer-id/:CustomerID',
        data: {
          roles: ['Everybody'],
        },
        component: CustomersByCustomerIdComponent
      },
      {
        path: 'orders-by-employee-id/:EmployeeID',
        data: {
          roles: ['Everybody'],
        },
        component: OrdersByEmployeeIdComponent
      },
      {
        path: 'employees-by-employee-id/:EmployeeID',
        data: {
          roles: ['Everybody'],
        },
        component: EmployeesByEmployeeIdComponent
      },
      {
        path: 'products-by-product-id/:ProductID',
        data: {
          roles: ['Everybody'],
        },
        component: ProductsByProductIdComponent
      },
      {
        path: 'suppliers-by-supplier-id/:SupplierID',
        data: {
          roles: ['Everybody'],
        },
        component: SuppliersBySupplierIdComponent
      },
      {
        path: 'products-by-supplier-id/:SupplierID',
        data: {
          roles: ['Everybody'],
        },
        component: ProductsBySupplierIdComponent
      },
      {
        path: 'add-application-role',
        canActivate: [AuthGuard],
        data: {
          roles: ['admin'],
        },
        component: AddApplicationRoleComponent
      },
      {
        path: 'add-application-user',
        canActivate: [AuthGuard],
        data: {
          roles: ['admin'],
        },
        component: AddApplicationUserComponent
      },
      {
        path: 'profile',
        data: {
          roles: ['Everybody'],
        },
        component: ProfileComponent
      },
      {
        path: 'application-roles',
        canActivate: [AuthGuard],
        data: {
          roles: ['admin'],
        },
        component: ApplicationRolesComponent
      },
      {
        path: 'application-users',
        canActivate: [AuthGuard],
        data: {
          roles: ['admin'],
        },
        component: ApplicationUsersComponent
      },
      {
        path: 'register-application-user',
        data: {
          roles: ['Everybody'],
        },
        component: RegisterApplicationUserComponent
      },
      {
        path: 'edit-application-user/:Id',
        canActivate: [AuthGuard],
        data: {
          roles: ['admin'],
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
        path: 'dashboard',
        data: {
          roles: ['Everybody'],
        },
        component: DashboardComponent
      },
      {
        path: 'orders-by-product-id/:ProductID',
        data: {
          roles: ['Everybody'],
        },
        component: OrdersByProductIdComponent
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
