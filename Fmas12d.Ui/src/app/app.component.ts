import { Component } from '@angular/core';
import { OidcSecurityService, AuthorizationResult, AuthorizationState } from 'angular-auth-oidc-client';
import { RouterService } from './services/router/router.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Fmas12d';

  constructor(public oidcSecurityService: OidcSecurityService,
    private routerService: RouterService,
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

  ngOnInit() {
  }

  ngOnDestroy(): void {
  }

  login() {
    console.log('start login');
    this.oidcSecurityService.authorize();
  }

  refreshSession() {
    console.log('start refreshSession');
    this.oidcSecurityService.authorize();
  }

  logout() {
    console.log('start logoff');
    this.oidcSecurityService.logoff();
  }

  private onOidcModuleSetup() {
    console.log('AppComponent:onAuthorizationResultComplete');
    if (window.location.hash) {
      this.oidcSecurityService.authorizedImplicitFlowCallback();
    } else {
      if ('/autologin' !== window.location.pathname) {
        this.write('redirect', window.location.pathname);
      }
      console.log('AppComponent:onModuleSetup');
      this.oidcSecurityService.getIsAuthorized().subscribe((authorized: boolean) => {
        if (!authorized) {
          this.routerService.navigate(['/autologin']);
        }
      });
    }
  }

  private onAuthorizationResultComplete(authorizationResult: AuthorizationResult) {
    console.log('AppComponent:onAuthorizationResultComplete');
    const path = this.read('redirect');
    if (authorizationResult.authorizationState === AuthorizationState.authorized) {
      if (path) {
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
