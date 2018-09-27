import { Component, Injector } from '@angular/core';
import { MainPageGenerated } from './main-page-generated.component';

// Import HttpClient and HttpParams which are needed to make HTTP calls.
import {HttpClient, HttpParams} from '@angular/common/http';
// Import the environment to get the server URL
import {environment} from '../../environments/environment';

@Component({
  selector: 'main-page',
  templateUrl: './main-page.component.html'
})
export class MainPageComponent extends MainPageGenerated {
  constructor(injector: Injector, private http: HttpClient) {
    super(injector);
 }

  getData(type: string) {
    // Extract the server URL
    const url = environment.sample.replace(/odata.*/, '');

    return this.http
       // Request the GetData action method
       .get(`${url}api/custommethod/getdata`, {
          // Pass the type parameter
          params: new HttpParams().set('type', type)
       })
       .toPromise();
 }
}
