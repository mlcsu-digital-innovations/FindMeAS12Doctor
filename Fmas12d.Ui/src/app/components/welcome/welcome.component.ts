import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { RouterService } from 'src/app/services/router/router.service';
import { Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent implements OnInit {

  isAuthorizedSubscription: Subscription;
  isAuthorized: boolean;
  isDevelopment: boolean;
  userDataSubscription: Subscription;
  userData: any;

  constructor(
    private oidcSecurityService: OidcSecurityService,
    private routerService: RouterService) { }

  ngOnInit() {

    this.isDevelopment = !environment.production;

    this.isAuthorizedSubscription = this.oidcSecurityService.getIsAuthorized().subscribe(      
      (isAuthorized: boolean) => {

        this.isAuthorized = isAuthorized;
        this.userDataSubscription = this.oidcSecurityService.getUserData().subscribe(userData => {
          this.userData = userData;
          if (this.userData !== '') {
            this.userData.access_token = this.oidcSecurityService.getToken();
          }
        });
      }
    );

  }

  ngOnDestroy() {
    this.userDataSubscription.unsubscribe();
  }

  copyToken() {
    navigator.clipboard.writeText(this.userData.access_token);
  }

  gotoReferralList() {
    this.routerService.navigate(['/referral/list']);
  }

  login() {
    this.oidcSecurityService.authorize();
  }

}
