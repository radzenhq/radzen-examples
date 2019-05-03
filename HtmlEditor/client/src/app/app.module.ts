import {NgModule} from '@angular/core';
import {ServiceWorkerModule} from '@angular/service-worker';
import {environment} from '../environments/environment';
import {
  AppImports,
  AppComponent,
  AppDeclarations,
  AppProviders
} from './app.module-generated';
import * as Quill from 'quill/dist/quill';

(<any>window).Quill = Quill;

import {EditorModule} from 'primeng/editor';

@NgModule({
  declarations: [...AppDeclarations],
  imports: [
    environment.production
      ? ServiceWorkerModule.register('ngsw-worker.js')
      : [],
    EditorModule,
    ...AppImports
  ],
  providers: [...AppProviders],
  bootstrap: [AppComponent]
})
export class AppModule {}
