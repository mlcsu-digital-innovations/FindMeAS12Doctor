import { Injectable } from '@angular/core';
import { OidcSecurityService, AuthorizationResult, AuthorizationState } from 'angular-auth-oidc-client';
import { RouterService } from '../router/router.service';
import { UserDetailsService } from '../user/user-details.service';
import { User } from 'src/app/interfaces/user';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {

  constructor(
    public oidcSecurityService: OidcSecurityService,
    private routerService: RouterService,
    private userDetailsService: UserDetailsService
    ) {

    if (this.oidcSecurityService.moduleSetup) {
      this.onOidcModuleSetup();
    } else {
      this.oidcSecurityService.onModuleSetup.subscribe(() => {
        this.onOidcModuleSetup();
      });
    }

    this.oidcSecurityService.onAuthorizationResult.subscribe(
      (authorizationResult: AuthorizationResult) => {
        this.onAuthorizationResultComplete(authorizationResult);
      });
  }

  getIsAuthorized() {
    return this.oidcSecurityService.getIsAuthorized();
  }

  login() {
    this.oidcSecurityService.authorize();
  }

  refreshSession() {
    this.oidcSecurityService.authorize();
  }

  logout() {
    this.oidcSecurityService.logoff();
    this.routerService.navigate(['/signout']);
  }

  private onOidcModuleSetup() {
    if (window.location.hash) {
      this.oidcSecurityService.authorizedImplicitFlowCallback();
    } else {
      if ('/autologin' !== window.location.pathname) {
        this.write('redirect', window.location.pathname);
      }
      this.oidcSecurityService.getIsAuthorized().subscribe((authorized: boolean) => {
        if (!authorized) {
          this.routerService.navigate(['/autologin']);
        }
      });
    }
  }

  private onAuthorizationResultComplete(authorizationResult: AuthorizationResult) {

    const path = this.read('redirect');
    if (authorizationResult.authorizationState === AuthorizationState.authorized) {
      if (path === '/') {
        this.userDetailsService.getCurrentUserDetails().subscribe((user: User) => {
          if (user.isDoctor) {
            this.routerService.navigate(['/doctor/claims/list']);
          } else if (user.isFinance) {
            this.routerService.navigate(['/finance/claims/list']);
          } else {
            this.routerService.navigate(['/referral/list']);
          }
        });
      } else if (path) {
        this.routerService.navigate([path]);
      }
    } else {
      this.routerService.navigate(['/unauthorized']);
    }
  }

  private read(key: string): any {
    const data = localStorage.getItem(key);
    if (data != null) {
      return JSON.parse(data);
    }

    return;
  }

  private write(key: string, value: any): void {
    localStorage.setItem(key, JSON.stringify(value));
  }
}
