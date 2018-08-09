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
import { SuppliersComponent } from './suppliers/suppliers.component';
import { AddSupplierComponent } from './add-supplier/add-supplier.component';
import { EditSupplierComponent } from './edit-supplier/edit-supplier.component';
import { TerritoriesComponent } from './territories/territories.component';
import { AddTerritoryComponent } from './add-territory/add-territory.component';
import { EditTerritoryComponent } from './edit-territory/edit-territory.component';
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
import { RegionsByRegionIdComponent } from './regions-by-region-id/regions-by-region-id.component';
import { TerritoriesByRegionIdComponent } from './territories-by-region-id/territories-by-region-id.component';
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
        path: 'order-details-by-order-id/:OrderID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: OrderDetailsByOrderIdComponent
      },
      {
        path: 'orders-by-order-id/:OrderID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: OrdersByOrderIdComponent
      },
      {
        path: 'orders-by-customer-id/:CustomerID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: OrdersByCustomerIdComponent
      },
      {
        path: 'products-by-category-id/:CategoryID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: ProductsByCategoryIdComponent
      },
      {
        path: 'categories-by-category-id/:CategoryID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: CategoriesByCategoryIdComponent
      },
      {
        path: 'customers-by-customer-id/:CustomerID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: CustomersByCustomerIdComponent
      },
      {
        path: 'orders-by-employee-id/:EmployeeID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: OrdersByEmployeeIdComponent
      },
      {
        path: 'employees-by-employee-id/:EmployeeID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: EmployeesByEmployeeIdComponent
      },
      {
        path: 'products-by-product-id/:ProductID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: ProductsByProductIdComponent
      },
      {
        path: 'suppliers-by-supplier-id/:SupplierID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: SuppliersBySupplierIdComponent
      },
      {
        path: 'products-by-supplier-id/:SupplierID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: ProductsBySupplierIdComponent
      },
      {
        path: 'regions-by-region-id/:RegionID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: RegionsByRegionIdComponent
      },
      {
        path: 'territories-by-region-id/:RegionID',
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
        },
        component: TerritoriesByRegionIdComponent
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
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
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
        canActivate: [AuthGuard],
        data: {
          roles: ['Authenticated'],
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
