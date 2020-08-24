import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs';
import {AuthService} from '../services/auth.service';
import {CurrentUserService} from '../services/current-user.service';
import {Role} from '../models/role';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router,
              private authService: AuthService,
              private currentUserService: CurrentUserService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
    Observable<boolean> | Promise<boolean> | boolean {
    if (this.authService.isAuthenticated()) {
      const routeRoles: Array<Role> = route.data.roles;
      const userRoles: Array<Role> = this.currentUserService.user.roles.map(x => x.value);

      if (routeRoles) {
        if (routeRoles.some(x => userRoles.some(role => role === x))) {
          return true;
        }

        this.router.navigate(['/access-denied']).then(() => {});
        return false;
      }

      return true;
    }

    this.router.navigate(['/access-denied']);
    return false;
  }
}
