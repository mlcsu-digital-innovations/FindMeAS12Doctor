import { Injectable } from '@angular/core';
import { CanActivate, CanLoad, ActivatedRouteSnapshot, RouterStateSnapshot, Route } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { RouterService } from './services/router/router.service';

@Injectable()
export class AuthorizationGuard implements CanActivate, CanLoad {

  constructor(private routerService: RouterService,
              private oidcSecurityService: OidcSecurityService) { }

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
          console.log(user);

          const key = `userAppData_${user.oid}`;

          console.log('check session storage ...');
          

        }
        );

        return false;
      })
    );
  }
}
