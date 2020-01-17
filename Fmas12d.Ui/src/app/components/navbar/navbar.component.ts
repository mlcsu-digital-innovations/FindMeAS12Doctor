import { Component, OnInit } from '@angular/core';
import { version } from '../../../../package.json';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  // Add standard navbar options here !
  public version: string = version;
  isAuthorizedSubscription: Subscription;
  isAuthorized: boolean;
  userDataSubscription: Subscription;
  userData: { name: string };

  constructor(public oidcSecurityService: OidcSecurityService) {
  }

  ngOnInit() {
    this.isAuthorizedSubscription = this.oidcSecurityService.getIsAuthorized().subscribe(
      (isAuthorized: boolean) => {
        this.isAuthorized = isAuthorized;
      });
    this.userDataSubscription =
      this.oidcSecurityService.getUserData<{ name: string }>().subscribe(userData => {
        this.userData = userData;
    });
  }

  ngOnDestroy(): void {
    this.isAuthorizedSubscription.unsubscribe();
  }

  login() {
    this.oidcSecurityService.authorize();
  }

  refreshSession() {
    this.oidcSecurityService.authorize();
  }

  logout() {
    this.oidcSecurityService.logoff();
  }

  home(): string{
    // TODO: replace with switch
    return '/referral/list';

  }
}
