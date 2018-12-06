import { Injector, Injectable } from '@angular/core';

import { environment } from '../environments/environment';

@Injectable()
export class ConfigService {
  private config: any = {};

  load() {
    this.config = Object.assign({}, environment);
  }

  get(key: string) {
    return this.config[key];
  }
}

export function configServiceFactory(configService: ConfigService) {
  return () => configService.load();
}

