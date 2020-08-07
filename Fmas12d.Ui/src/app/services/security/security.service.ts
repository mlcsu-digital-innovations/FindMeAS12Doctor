import { EventTypes, OidcSecurityService, PublicEventsService, AuthorizationResult, AuthorizedState } from 'angular-auth-oidc-client';
import { filter } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/interfaces/user';
import { UserDetailsService } from '../user/user-details.service';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {

  constructor(
    public oidcSecurityService: OidcSecurityService,
    private eventService: PublicEventsService,
    private router: Router,
    private userDetailsService: UserDetailsService
    ) {
      this.oidcSecurityService.checkAuth()
      .subscribe((isAuthenticated) => {

        if (!isAuthenticated) {
          if ('/autologin' !== window.location.pathname) {
            this.write('redirect', window.location.pathname);
            this.router.navigate(['/autologin']);
          }
        }

        if (isAuthenticated) {
          this.navigateToStoredEndpoint();
        }
      });

      this.eventService
      .registerForEvents()
      .pipe(filter((notification) => notification.type === EventTypes.NewAuthorizationResult))
      .subscribe((result) => {
        console.log('NewAuthorizationResult with value from app', result);
        this.onAuthorizationResultComplete(result.value);
      });

      this.eventService
      .registerForEvents()
      .pipe(filter((notification) => notification.type === EventTypes.UserDataChanged))
      .subscribe((value) => {
        console.log('UserDataChanged with value from app', value);
        this.onUserDataChangeComplete();
      });

      this.eventService
      .registerForEvents()
      .pipe(filter((notification) => notification.type === EventTypes.ConfigLoaded))
      .subscribe((value) => {
        console.log('ConfigLoaded with value from app', value);
      });
  }

  private navigateToStoredEndpoint() {
    const path = this.read('redirect');
    if (this.router.url === path) {
      return;
    }
    if (path.toString().includes('/unauthorized')) {
      this.router.navigate(['/']);
    } else {
      this.router.navigate([path]);
    }
  }

  getIsAuthorized() {
    return this.oidcSecurityService.isAuthenticated$;
  }

  login() {
    this.oidcSecurityService.authorize();
  }

  refreshSession() {
    this.oidcSecurityService.authorize();
  }

  logout() {
    this.oidcSecurityService.logoff();
    this.router.navigate(['/signout']);
  }

  private onUserDataChangeComplete() {
    this.userDetailsService.getCurrentUserDetails().subscribe((user: User) => {

      if (user.isDoctor) {
        this.router.navigate(['/doctor/claims/list']);
      } else if (user.isFinance) {
        this.router.navigate(['/finance/claims/list']);
      } else {
        this.router.navigate(['/referral/list']);
      }
    });
  }


  private onAuthorizationResultComplete(authorizationResult: AuthorizationResult) {

    const path = this.read('redirect');

    if (authorizationResult.authorizationState === AuthorizedState.Authorized) {
      if (path === '/') {

        this.userDetailsService.getCurrentUserDetails().subscribe((user: User) => {

          if (user.isDoctor) {
            this.router.navigate(['/doctor/claims/list']);
          } else if (user.isFinance) {
            this.router.navigate(['/finance/claims/list']);
          } else {
            this.router.navigate(['/referral/list']);
          }
        });
      } else if (path) {
        this.router.navigate([path]);
      }
    } else {
      this.router.navigate(['/unauthorized']);
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
