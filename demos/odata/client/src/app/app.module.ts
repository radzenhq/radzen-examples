import { NgModule } from '@angular/core';

import { AppImports, AppComponent, AppDeclarations, AppProviders } from './app.module-generated';

@NgModule({
  declarations: [
    ...AppDeclarations
  ],
  imports: [
    ...AppImports
  ],
  providers: [
    ...AppProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
