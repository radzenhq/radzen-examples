import {NgModule} from '@angular/core';
import {ServiceWorkerModule} from '@angular/service-worker';
import {RouterModule} from '@angular/router';
import {environment} from '../environments/environment';
import {routes} from './app.routes';
import {
  AppImports,
  AppComponent,
  AppDeclarations,
  AppProviders
} from './app.module-generated';

@NgModule({
  declarations: [...AppDeclarations],
  imports: [
    environment.production
      ? ServiceWorkerModule.register('ngsw-worker.js')
      : [],
    ...AppImports,
    RouterModule.forRoot(routes, {useHash: true})
  ],
  providers: [...AppProviders],
  bootstrap: [AppComponent]
})
export class AppModule {}
