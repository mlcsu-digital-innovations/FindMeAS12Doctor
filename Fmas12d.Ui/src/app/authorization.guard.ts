import { Injectable } from '@angular/core';
import { CanActivate, CanLoad, ActivatedRouteSnapshot, RouterStateSnapshot, Route } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { RouterService } from './services/router/router.service';
import { UserDetailsService } from './services/user/user-details.service';
import { User } from './interfaces/user';

@Injectable()
export class AuthorizationGuard implements CanActivate, CanLoad {

  constructor(private routerService: RouterService,
              private oidcSecurityService: OidcSecurityService,
              private userDetailsService: UserDetailsService) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    return this.checkUser();
  }

  canLoad(state: Route): Observable<boolean> {
    return this.checkUser();
  }

  private checkUser(): any {

    return this.oidcSecurityService.getIsAuthorized().pipe(
      tap((isAuthorized: boolean) => {
        if (!isAuthorized) {
          this.routerService.navigate(['/unauthorized']);
          return false;
        }

        this.oidcSecurityService.getUserData().subscribe(user => {

          // check session storage for user access
          const key = `userAppData_${user.oid}`;
          const validatedUser = sessionStorage.getItem(key);

          if (validatedUser === null ) {
            this.userDetailsService.getCurrentUserDetails().subscribe((usr: User) => {
              sessionStorage.setItem(key, '');
              return true;
            }, err => {
              // user isn't valid
              this.routerService.navigate(['/unauthorized']);
              return false;
            });
          } else {
            return true;
          }
        });
      })
    );
  }
}
