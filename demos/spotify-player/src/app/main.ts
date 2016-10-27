import { NgModule } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { RadzenModule } from '@radzen/angular';

import { SpotifyService } from './services/spotify';
import { SpotifyAuthorizationService } from './services/spotify-authorization.ts';

import { NewReleasesComponent } from './components/new-releases';
import { TracksComponent } from './components/tracks';

import 'primeng/resources/primeng.min.css';
import 'ng2d3/release/ng2d3.css';
import '@radzen/dusk-theme/dist/styles.css';

import { AppComponent, PageTitleComponent, routes } from './components/app';

@NgModule({
  imports: [
    BrowserModule,
    // Because of https://github.com/angular/angular/issues/11233
    RouterModule.forRoot(routes, { useHash: false }),
    FormsModule,
    CommonModule,
    HttpModule,
    RadzenModule
  ],
  declarations: [
    NewReleasesComponent,
    TracksComponent,
    AppComponent,
    PageTitleComponent
  ],
  providers: [
    SpotifyService,
    SpotifyAuthorizationService,
  ],
  bootstrap: [
    AppComponent
  ]
})
class AppModule {
}

document.addEventListener('DOMContentLoaded', function main() {
  platformBrowserDynamic()
  .bootstrapModule(AppModule)
  .then(() => {
    document.querySelector('.sidebar-toggler').addEventListener('click', function(e) {
      e.preventDefault();

      document.body.classList.toggle('sidebar-toggle')
    });
  });
});

