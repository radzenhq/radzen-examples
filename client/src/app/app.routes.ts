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
import { NorthwindOrdersComponent } from './northwind-orders/northwind-orders.component';
import { AddNorthwindOrderComponent } from './add-northwind-order/add-northwind-order.component';
import { EditNorthwindOrderComponent } from './edit-northwind-order/edit-northwind-order.component';
import { NorthwindOrderDetailsComponent } from './northwind-order-details/northwind-order-details.component';
import { AddNorthwindOrderDetailComponent } from './add-northwind-order-detail/add-northwind-order-detail.component';
import { EditNorthwindOrderDetailComponent } from './edit-northwind-order-detail/edit-northwind-order-detail.component';
import { NorthwindProductsComponent } from './northwind-products/northwind-products.component';
import { AddNorthwindProductComponent } from './add-northwind-product/add-northwind-product.component';
import { EditNorthwindProductComponent } from './edit-northwind-product/edit-northwind-product.component';
import { RegionsComponent } from './regions/regions.component';
import { AddRegionComponent } from './add-region/add-region.component';
import { EditRegionComponent } from './edit-region/edit-region.component';
import { SuppliersComponent } from './suppliers/suppliers.component';
import { AddSupplierComponent } from './add-supplier/add-supplier.component';
import { EditSupplierComponent } from './edit-supplier/edit-supplier.component';
import { TerritoriesComponent } from './territories/territories.component';
import { AddTerritoryComponent } from './add-territory/add-territory.component';
import { EditTerritoryComponent } from './edit-territory/edit-territory.component';

export const routes: Routes = [
  { path: '', redirectTo: '/categories', pathMatch: 'full' },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {
        path: 'categories',
        component: CategoriesComponent
      },
      {
        path: 'add-category',
        component: AddCategoryComponent
      },
      {
        path: 'edit-category/:CategoryID',
        component: EditCategoryComponent
      },
      {
        path: 'customers',
        component: CustomersComponent
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
        path: 'customer-customer-demos',
        component: CustomerCustomerDemosComponent
      },
      {
        path: 'add-customer-customer-demo',
        component: AddCustomerCustomerDemoComponent
      },
      {
        path: 'edit-customer-customer-demo/:CustomerID/:CustomerTypeID',
        component: EditCustomerCustomerDemoComponent
      },
      {
        path: 'customer-demographics',
        component: CustomerDemographicsComponent
      },
      {
        path: 'add-customer-demographic',
        component: AddCustomerDemographicComponent
      },
      {
        path: 'edit-customer-demographic/:CustomerTypeID',
        component: EditCustomerDemographicComponent
      },
      {
        path: 'employees',
        component: EmployeesComponent
      },
      {
        path: 'add-employee',
        component: AddEmployeeComponent
      },
      {
        path: 'edit-employee/:EmployeeID',
        component: EditEmployeeComponent
      },
      {
        path: 'employee-territories',
        component: EmployeeTerritoriesComponent
      },
      {
        path: 'add-employee-territory',
        component: AddEmployeeTerritoryComponent
      },
      {
        path: 'edit-employee-territory/:EmployeeID/:TerritoryID',
        component: EditEmployeeTerritoryComponent
      },
      {
        path: 'northwind-orders',
        component: NorthwindOrdersComponent
      },
      {
        path: 'add-northwind-order',
        component: AddNorthwindOrderComponent
      },
      {
        path: 'edit-northwind-order/:OrderID',
        component: EditNorthwindOrderComponent
      },
      {
        path: 'northwind-order-details',
        component: NorthwindOrderDetailsComponent
      },
      {
        path: 'add-northwind-order-detail',
        component: AddNorthwindOrderDetailComponent
      },
      {
        path: 'edit-northwind-order-detail/:OrderID/:ProductID',
        component: EditNorthwindOrderDetailComponent
      },
      {
        path: 'northwind-products',
        component: NorthwindProductsComponent
      },
      {
        path: 'add-northwind-product',
        component: AddNorthwindProductComponent
      },
      {
        path: 'edit-northwind-product/:ProductID',
        component: EditNorthwindProductComponent
      },
      {
        path: 'regions',
        component: RegionsComponent
      },
      {
        path: 'add-region',
        component: AddRegionComponent
      },
      {
        path: 'edit-region/:RegionID',
        component: EditRegionComponent
      },
      {
        path: 'suppliers',
        component: SuppliersComponent
      },
      {
        path: 'add-supplier',
        component: AddSupplierComponent
      },
      {
        path: 'edit-supplier/:SupplierID',
        component: EditSupplierComponent
      },
      {
        path: 'territories',
        component: TerritoriesComponent
      },
      {
        path: 'add-territory',
        component: AddTerritoryComponent
      },
      {
        path: 'edit-territory/:TerritoryID',
        component: EditTerritoryComponent
      },
    ]
  },
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
