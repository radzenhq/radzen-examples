import { Location } from '@angular/common';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { map, switchMap, catchError } from 'rxjs/operators';

export interface ODataQueryParams {
  top?: number;
  skip?: number;
  orderby?: string;
  count?: boolean;
  inlinecount?: string;
  expand?: string;
  filter?: string;
  select?: string;
  format?: string;
}

function cacheKey(path: string) {
  return path.replace(/\(.*?\)$/, '');
}

function toLegacyFilter(value: string) {
  // Cast GUID
  value = value.replace(
    /([0-9A-F]{8}-[0-9A-F]{4}-[1-5][0-9A-F]{3}-[89AB][0-9A-F]{3}-[0-9A-F]{12})/i,
    "guid'$&'"
  );

  // Cast DateTime
  value = value.replace(
    /(\d{4}-[01]\d-[0-3]\dT[0-2]\d:[0-5]\d:[0-5]\d\.\d+([+-][0-2]\d:[0-5]\d|Z))|(\d{4}-[01]\d-[0-3]\dT[0-2]\d:[0-5]\d:[0-5]\d([+-][0-2]\d:[0-5]\d|Z))|(\d{4}-[01]\d-[0-3]\dT[0-2]\d:[0-5]\d([+-][0-2]\d:[0-5]\d|Z))/i,
    "datetime'$&'"
  );

  // Change contains() to substringof()
  value = value.replace(
    /contains\((.+?), (.+?)\)/g,
    'substringof($2, $1) eq true'
  );

  return value;
}

function updateCount(result, amount) {
  if ('@odata.count' in result) {
    result['@odata.count'] = result['@odata.count'] + amount;
  } else if ('odata.count' in result) {
    result['odata.count'] = +result['odata.count'] + amount;
  }
}

function errorResponse(response) {
  if (response.status == 0) {
    return { ...response, error: { message: response.statusText } };
  } else {
    return { ...response, ...response.error };
  }
}

function toHttpParams(odataParams: ODataQueryParams | null, legacy: boolean): HttpParams | null {
  if (odataParams == null) {
    return null;
  }

  return Object.keys(odataParams).reduce((params, key) => {
    let value = odataParams[key];

    if (value == null || value === '') {
      return params;
    }

    if (key == 'filter') {
      if (legacy) {
        value = toLegacyFilter(value);
      }

      if (value.startsWith(' and ') || value.endsWith(' and ')) {
        value = value.split(' and ').filter(v => v).join(' and ');
      }
    }

    if (key == 'orderby') {
      value = value.split(',').filter(v => v).join(',');
    }

    return params.set(`$${key}`, value.toString());
  }, new HttpParams());
}

export class ODataClient {
  cache: { [resource: string]: { result: BehaviorSubject<any> } } = {};

  constructor(private http: HttpClient, private basePath: string, private options: { legacy: boolean, withCredentials: boolean }) {
  }

  getById(path: string, odataParams?: ODataQueryParams) {
    const params = toHttpParams(odataParams, this.options.legacy);

    return this.request('get', path, params)
      .pipe(map(response => {
        switch (response.status) {
          case 200:
            return this.filterResponseBody(response.body);
        }
      }));
  }

  get(path: string, odataParams?: ODataQueryParams) {
    if (!odataParams) {
      return this.request('get', path)
      .pipe(map(response => {
        switch (response.status) {
          case 200:
            return this.filterResponseBody(response.body);
        }
      }));
    }

    if (odataParams.format == 'csv' || odataParams.format == 'xlsx') {
      return this.export(path, odataParams);
    }

    const params = toHttpParams(odataParams, this.options.legacy);

    return this.request('get', path, params)
    .pipe(map(response => {
      switch (response.status) {
        case 200: {
          const key = cacheKey(path);
          const cache = this.cache[key];
          if (cache) {
            cache.result.unsubscribe();
          }
          const result = new BehaviorSubject(this.filterResponseBody(response.body));
          this.cache[key] = { result };
          return result;
        }
      }
    }), switchMap(result => result));
  }

  delete(path: string, filterByKeys: (item: any) => boolean) {
    return this.request('delete', path)
    .pipe(map(response => {
      switch (response.status) {
        case 204: {
          const cache = this.cache[cacheKey(path)];

          if (cache) {
            const result = cache.result.getValue();
            result.value = result.value.filter(filterByKeys);

            updateCount(result, -1);

            cache.result.next(result);
          }

          return {};
        }
      }
    }));
  }

  invoke(path: string, body?: any) {
    return this.request('post', path, null, body)
    .pipe(map(response => {
      switch (response.status) {
        case 200: {
          return response.body;
        }
      }
    }));
  }

  post(path: string, body: any, odataParams?: ODataQueryParams, navigationProperties?: string[]) {
    const cache = this.cache[cacheKey(path)];

    const params = toHttpParams(odataParams, this.options.legacy);

    return this.request('post', path, params, body)
    .pipe(map(response => {
      switch (response.status) {
        case 201: {
          const { body } = response;

          if (cache) {
            const result = cache.result.getValue();
            result.value = [...result.value, body];

            updateCount(result, 1);

            cache.result.next(result);
          }

          return body;
        }
      }
    }));
  }

  put(path: string, body: any, findByKeys: (item: any) => boolean, odataParams?: ODataQueryParams, navigationProperties?: string[]) {
    const cache = this.cache[cacheKey(path)];

    const params = toHttpParams(odataParams, this.options.legacy);

    return this.request('put', path, params, body)
    .pipe(map(response => {
      switch (response.status) {
        case 200:
        case 204: {
          if (cache) {
            const result = cache.result.getValue();
            const index = result.value.findIndex(findByKeys);

            const replacement = response.status == 200 ? response.body : Object.assign({}, result.value[index], body);

            result.value = [...result.value.slice(0, index), replacement, ...result.value.slice(index + 1)];

            cache.result.next(result);
          }

          return body;
        }
      }
    }));
  }

  upload(path: string, file: any) {
    return this.uploadFile('put', path, file)
    .pipe(map(response => {
      switch (response.status) {
        case 200:
        case 204: {
          return file;
        }
      }
    }));
  }

  patch(path: string, body: any, findByKeys: (item: any) => boolean, odataParams?: ODataQueryParams, navigationProperties?: string[]) {
    const cache = this.cache[cacheKey(path)];

    const params = toHttpParams(odataParams, this.options.legacy);

    return this.request('patch', path, params, body, navigationProperties)
    .pipe(map(response => {
      switch (response.status) {
        case 200:
        case 204: {
          if (cache) {
            const result = cache.result.getValue();
            const index = result.value.findIndex(findByKeys);

            const replacement = response.status == 200 ? response.body : Object.assign({}, result.value[index], body);

            result.value = [...result.value.slice(0, index), replacement, ...result.value.slice(index + 1)];

            cache.result.next(result);
          }

          return body;
        }
      }
    }));
  }

  export(path: string, odataParams?: ODataQueryParams) {
    let headers = new HttpHeaders();

    if (odataParams.format == 'xlsx') {
      headers = headers.set('Accept', 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet');
    } else if (odataParams.format == 'csv') {
      headers = headers.set('Accept', 'text/csv');
    }

    const params = Object.keys(odataParams).reduce((params, key) => {
      let value = odataParams[key];

      if (value == null || value === '') {
        return params;
      }

      if (key == 'filter') {
        if(this.options.legacy) {
          value = toLegacyFilter(value);
        }

        if(value.startsWith(' and ') || value.endsWith(' and ')) {
          value = value.split(' and ').filter(v => v).join(' and ');
        }
      }

      return params.set(`$${key}`, value.toString());
    }, new HttpParams());


    return this.http.request('get', Location.joinWithSlash(this.basePath, path.replace("'null'", null)), {
      responseType: 'blob',
      params: odataParams ? params : undefined,
      headers,
      withCredentials: this.options.withCredentials
    }).pipe(map(response => {
      this.downloadFile(response, `Export.${odataParams.format}`)
    }))
    .pipe(catchError(response => {
      return throwError(errorResponse(response));
    }));
  }

  private request(method: string, path: string, params?: HttpParams, body?: any, navigationProperties?: string[]) {
    let headers = new HttpHeaders();

    headers = headers.set('Accept', 'application/json');

    if (body) {
      headers = headers.set('Content-Type', 'application/json');
    }

    if ((method == 'delete' || body) && this.options.legacy && method != 'post') {
      headers = headers.set('If-Match', '*');
    }

    if (body && typeof body === 'object' && '@odata.etag' in body) {
      headers = headers.set('If-Match', body['@odata.etag']);
    }

    return this.http.request(method, Location.joinWithSlash(this.basePath, path.replace(/'null'/g, 'null')), {
      observe: 'response',
      body: body ? JSON.stringify(this.filterRequestBody(body, navigationProperties)) : undefined,
      params,
      headers,
      withCredentials: this.options.withCredentials
    })
    .pipe(catchError(response => {
      return throwError(errorResponse(response));
    }));
  }

  private uploadFile(method: string, path: string, file: any) {
    let headers = new HttpHeaders();

    headers = headers.set('Accept', 'application/json');
    headers = headers.set('Content-Type', 'application/octet-stream');

    return this.http.request(method, Location.joinWithSlash(this.basePath, path), {
      observe: 'response',
      body: file,
      headers,
      withCredentials: this.options.withCredentials
    });
  }

  private downloadFile(blob, fileName) {
    if (navigator.msSaveBlob) {
      navigator.msSaveBlob(blob, fileName);
    } else {
      var link = document.createElement("a");
      if (link.download !== undefined) {
        var url = URL.createObjectURL(blob);
        link.setAttribute("href", url);
        link.setAttribute("download", fileName);
        link.style.visibility = 'hidden';
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
      }
    }
  }

  private filterRequestBody(body, navigationProperties) {
    return Object.keys(body)
      .filter(key => key == 'RoleNames' || ((navigationProperties || []).indexOf(key) == -1 && !Array.isArray(body[key]) && !(Object.prototype.toString.call(body[key]) === '[object Object]')))
      .reduce((obj, key) => {
        return {
          ...obj,
          [key]: body[key]
        };
      }, {});
  }

  private filterResponseBody(body) {
    return Object.keys(body)
      .filter(key => key !== '@odata.context')
      .reduce((obj, key) => {
        return {
          ...obj,
          [key]: body[key]
        };
      }, {});
  }
}
