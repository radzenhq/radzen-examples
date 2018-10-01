import { Component, Injector } from '@angular/core';
import { MainPageGenerated } from './main-page-generated.component';

// Import HttpClient and HttpParams which are needed to make HTTP calls.
import { HttpClient, HttpParams } from '@angular/common/http';
// Import the environment to get the server URL
import { environment } from '../../environments/environment';

@Component({
  selector: 'main-page',
  templateUrl: './main-page.component.html'
})
export class MainPageComponent extends MainPageGenerated {
  constructor(injector: Injector, private http: HttpClient) {
    super(injector);
  }

  getFile(fileName: string) {
    // Extract the server URL
    const url = environment.sample.replace(/odata.*/, '');

    return this.http
      // Request the GetFile action method
      .get(`${url}api/custommethod/getfile`, {
        responseType: 'blob',
        params: new HttpParams().set('fileName', fileName)
      })
      .map((value: Blob, index: number) => {
        // Force browser to show downloaded content as file
        if (navigator.msSaveBlob) {
          navigator.msSaveBlob(value, fileName);
        } else {
          const link = document.createElement("a");
          if (link.download !== undefined) {
            link.setAttribute("href", URL.createObjectURL(value));
            link.setAttribute("download", fileName);
            link.style.visibility = 'hidden';
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
          }
        }
      }).toPromise();
  }
}
