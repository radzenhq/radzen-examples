import { NgModule } from '@angular/core';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { AppImports, AppComponent, AppDeclarations, AppProviders } from './app.module-generated';

@NgModule({
  declarations: [
    ...AppDeclarations
  ],
  imports: [
    [],
    ...AppImports
  ],
  providers: [
    ...AppProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
