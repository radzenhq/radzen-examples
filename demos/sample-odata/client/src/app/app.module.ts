import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RadzenModule } from '@radzen/angular';

import { AppRoutes } from './app.routes';
import { AppComponent, PageTitleComponent } from './app.component';
import { ProductsComponent } from './products/products.component';
import { AddProductComponent } from './add-product/add-product.component';
import { EditProductComponent } from './edit-product/edit-product.component';

import { SampleService } from './sample.service';
import { SampleAuthorizationService } from './sample-auth.service';

@NgModule({
  declarations: [
    ProductsComponent,
    AddProductComponent,
    EditProductComponent,
    AppComponent,
    PageTitleComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpModule,
    RadzenModule,
    AppRoutes
  ],
  providers: [
    SampleService,
    SampleAuthorizationService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
