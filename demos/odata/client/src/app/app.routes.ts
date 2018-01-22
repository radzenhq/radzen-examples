import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ActivatedRoute } from '@angular/router';

import { PageTitleComponent } from './app.component';
import { OrdersComponent } from './orders/orders.component';
import { AddOrderComponent } from './add-order/add-order.component';
import { EditOrderComponent } from './edit-order/edit-order.component';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { AddOrderDetailComponent } from './add-order-detail/add-order-detail.component';
import { EditOrderDetailComponent } from './edit-order-detail/edit-order-detail.component';
import { ProductsComponent } from './products/products.component';
import { AddProductComponent } from './add-product/add-product.component';
import { EditProductComponent } from './edit-product/edit-product.component';

export const pageRoutes: Routes = [
  {
    path: 'orders',
    children: [
      {
        path: '',
        component: OrdersComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Orders',
        }
      }
    ]
  },
  {
    path: 'add-order',
    children: [
      {
        path: '',
        component: AddOrderComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Add Order',
        }
      }
    ]
  },
  {
    path: 'edit-order',
    children: [
      {
        path: ':Id',
        component: EditOrderComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Edit Order',
        }
      }
    ]
  },
  {
    path: 'order-details',
    children: [
      {
        path: '',
        component: OrderDetailsComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Order Details',
        }
      }
    ]
  },
  {
    path: 'add-order-detail',
    children: [
      {
        path: '',
        component: AddOrderDetailComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Add Order Detail',
        }
      }
    ]
  },
  {
    path: 'edit-order-detail',
    children: [
      {
        path: ':Id',
        component: EditOrderDetailComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Edit Order Detail',
        }
      }
    ]
  },
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
          title: 'Products',
        }
      }
    ]
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
          title: 'Add Product',
        }
      }
    ]
  },
  {
    path: 'edit-product',
    children: [
      {
        path: ':Id',
        component: EditProductComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Edit Product',
        }
      }
    ]
  },
  { path: '', redirectTo: '/orders', pathMatch: 'full' }
];

export const popupRoutes: Routes = [
  {
    path: 'orders',
    component: OrdersComponent,
    outlet: 'popup',
    data: {
      title: 'Orders'
    }
  },
  {
    path: 'add-order',
    component: AddOrderComponent,
    outlet: 'popup',
    data: {
      title: 'Add Order'
    }
  },
  {
    path: 'edit-order/:Id',
    component: EditOrderComponent,
    outlet: 'popup',
    data: {
      title: 'Edit Order'
    }
  },
  {
    path: 'order-details',
    component: OrderDetailsComponent,
    outlet: 'popup',
    data: {
      title: 'Order Details'
    }
  },
  {
    path: 'add-order-detail',
    component: AddOrderDetailComponent,
    outlet: 'popup',
    data: {
      title: 'Add Order Detail'
    }
  },
  {
    path: 'edit-order-detail/:Id',
    component: EditOrderDetailComponent,
    outlet: 'popup',
    data: {
      title: 'Edit Order Detail'
    }
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
    component: AddProductComponent,
    outlet: 'popup',
    data: {
      title: 'Add Product'
    }
  },
  {
    path: 'edit-product/:Id',
    component: EditProductComponent,
    outlet: 'popup',
    data: {
      title: 'Edit Product'
    }
  },
];

export const routes: Routes = [
  ...pageRoutes,
  ...popupRoutes,
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
