import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ActivatedRoute } from '@angular/router';

import { PageTitleComponent } from './app.component';
import { ProductsComponent } from './products/products.component';
import { AddProductComponent } from './add-product/add-product.component';
import { EditProductComponent } from './edit-product/edit-product.component';


export const routes: Routes = [
  {
    path: 'products',
    children: [
      {
        path: '',
        component: ProductsComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Products'
        }
      }
    ]
  },
  {
    path: 'products',
    component: ProductsComponent,
    outlet: 'popup',
    data: {
      title: 'Products'
    }
  },
  {
    path: 'add-product',
    children: [
      {
        path: '',
        component: AddProductComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Add Product'
        }
      }
    ]
  },
  {
    path: 'add-product',
    component: AddProductComponent,
    outlet: 'popup',
    data: {
      title: 'Add Product'
    }
  },
  {
    path: 'edit-product',
    children: [
      {
        path: ':ID',
        component: EditProductComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Edit Product'
        }
      }
    ]
  },
  {
    path: 'edit-product/:ID',
    component: EditProductComponent,
    outlet: 'popup',
    data: {
      title: 'Edit Product'
    }
  },
  { path: '', redirectTo: '/products', pathMatch: 'full' }
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
