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
    console.log('AuthorizationGuard, canActivate');

    return this.oidcSecurityService.getIsAuthorized().pipe(
      tap((isAuthorized: boolean) => {
        console.log('AuthorizationGuard, canActivate isAuthorized: ' + isAuthorized);

        if (!isAuthorized) {
          this.routerService.navigate(['/unauthorized']);
        }
      })
    );
  }
}