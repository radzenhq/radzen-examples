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
import { CustomerCustomerDemosComponent } from './customer-customer-demos/customer-customer-demos.component';
import { AddCustomerCustomerDemoComponent } from './add-customer-customer-demo/add-customer-customer-demo.component';
import { EditCustomerCustomerDemoComponent } from './edit-customer-customer-demo/edit-customer-customer-demo.component';
import { CustomerDemographicsComponent } from './customer-demographics/customer-demographics.component';
import { AddCustomerDemographicComponent } from './add-customer-demographic/add-customer-demographic.component';
import { EditCustomerDemographicComponent } from './edit-customer-demographic/edit-customer-demographic.component';
import { EmployeesComponent } from './employees/employees.component';
import { AddEmployeeComponent } from './add-employee/add-employee.component';
import { EditEmployeeComponent } from './edit-employee/edit-employee.component';
import { EmployeeTerritoriesComponent } from './employee-territories/employee-territories.component';
import { AddEmployeeTerritoryComponent } from './add-employee-territory/add-employee-territory.component';
import { EditEmployeeTerritoryComponent } from './edit-employee-territory/edit-employee-territory.component';
import { OrdersComponent } from './orders/orders.component';
import { AddOrderComponent } from './add-order/add-order.component';
import { EditOrderComponent } from './edit-order/edit-order.component';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { AddOrderDetailComponent } from './add-order-detail/add-order-detail.component';
import { EditOrderDetailComponent } from './edit-order-detail/edit-order-detail.component';
import { ProductsComponent } from './products/products.component';
import { AddProductComponent } from './add-product/add-product.component';
import { EditProductComponent } from './edit-product/edit-product.component';
import { RegionsComponent } from './regions/regions.component';
import { AddRegionComponent } from './add-region/add-region.component';
import { EditRegionComponent } from './edit-region/edit-region.component';
import { ShippersComponent } from './shippers/shippers.component';
import { AddShipperComponent } from './add-shipper/add-shipper.component';
import { EditShipperComponent } from './edit-shipper/edit-shipper.component';
import { SuppliersComponent } from './suppliers/suppliers.component';
import { AddSupplierComponent } from './add-supplier/add-supplier.component';
import { EditSupplierComponent } from './edit-supplier/edit-supplier.component';
import { TerritoriesComponent } from './territories/territories.component';
import { AddTerritoryComponent } from './add-territory/add-territory.component';
import { EditTerritoryComponent } from './edit-territory/edit-territory.component';
import { LoginComponent } from './login/login.component';
import { ApplicationUsersComponent } from './application-users/application-users.component';
import { ApplicationRolesComponent } from './application-roles/application-roles.component';
import { RegisterApplicationUserComponent } from './register-application-user/register-application-user.component';
import { AddApplicationRoleComponent } from './add-application-role/add-application-role.component';
import { AddApplicationUserComponent } from './add-application-user/add-application-user.component';
import { ProfileComponent } from './profile/profile.component';
import { EditApplicationUserComponent } from './edit-application-user/edit-application-user.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';

import { SecurityService } from './security.service';
import { AuthGuard } from './auth.guard';
export const routes: Routes = [
  { path: '', redirectTo: '/categories', pathMatch: 'full' },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'categories',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
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
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
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
        path: 'customer-customer-demos',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: CustomerCustomerDemosComponent
      },
      {
        path: 'add-customer-customer-demo',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddCustomerCustomerDemoComponent
      },
      {
        path: 'edit-customer-customer-demo/:CustomerID/:CustomerTypeID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditCustomerCustomerDemoComponent
      },
      {
        path: 'customer-demographics',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: CustomerDemographicsComponent
      },
      {
        path: 'add-customer-demographic',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddCustomerDemographicComponent
      },
      {
        path: 'edit-customer-demographic/:CustomerTypeID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditCustomerDemographicComponent
      },
      {
        path: 'employees',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
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
        path: 'employee-territories',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EmployeeTerritoriesComponent
      },
      {
        path: 'add-employee-territory',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddEmployeeTerritoryComponent
      },
      {
        path: 'edit-employee-territory/:EmployeeID/:TerritoryID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditEmployeeTerritoryComponent
      },
      {
        path: 'orders',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
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
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
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
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
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
        path: 'regions',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: RegionsComponent
      },
      {
        path: 'add-region',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddRegionComponent
      },
      {
        path: 'edit-region/:RegionID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditRegionComponent
      },
      {
        path: 'shippers',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: ShippersComponent
      },
      {
        path: 'add-shipper',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddShipperComponent
      },
      {
        path: 'edit-shipper/:ShipperID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditShipperComponent
      },
      {
        path: 'suppliers',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
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
        path: 'territories',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: TerritoriesComponent
      },
      {
        path: 'add-territory',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: AddTerritoryComponent
      },
      {
        path: 'edit-territory/:TerritoryID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EditTerritoryComponent
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
