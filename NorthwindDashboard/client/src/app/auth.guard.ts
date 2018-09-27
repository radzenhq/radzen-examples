import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from '@angular/router';
import { SecurityService } from './security.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private security: SecurityService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const roles = route.data.roles as string[];

    return this.security.canActivate(roles, state);
  }
}

