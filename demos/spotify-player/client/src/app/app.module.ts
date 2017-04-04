import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RadzenModule } from '@radzen/angular';

import { AppRoutes } from './app.routes';
import { AppComponent, PageTitleComponent } from './app.component';
import { NewReleasesComponent } from './new-releases/new-releases.component';
import { TracksComponent } from './tracks/tracks.component';

import { SpotifyService } from './spotify.service';
import { SpotifyAuthorizationService } from './spotify-auth.service';

@NgModule({
  declarations: [
    NewReleasesComponent,
    TracksComponent,
    AppComponent,
    PageTitleComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RadzenModule,
    AppRoutes
  ],
  providers: [
    SpotifyService,
    SpotifyAuthorizationService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
