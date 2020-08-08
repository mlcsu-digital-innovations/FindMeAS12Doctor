import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { RouterService } from 'src/app/services/router/router.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent implements OnInit {

  isAuthorized: boolean;
  isAuthorizedSubscription: Subscription;
  isDevelopment: boolean;
  userData: any;
  userDataSubscription: Subscription;

  constructor(
    private oidcSecurityService: OidcSecurityService,
    private routerService: RouterService) { }

  ngOnInit() {

    this.isDevelopment = !environment.production;

    this.oidcSecurityService.isAuthenticated$
    .pipe(
      map(
      (isAuthorized: boolean) => {

        this.isAuthorized = isAuthorized;

        if (isAuthorized) {
          this.userDataSubscription = this.oidcSecurityService.userData$.subscribe(userData => {
            this.userData = userData;
            if (this.userData !== null) {
              this.userData.access_token = this.oidcSecurityService.getToken();
              if (!this.isDevelopment) {
                console.log('welcome:ngOnInit:Redirect to home page');
                this.routerService.navigate(['/referral/list']);
              }
            }
          });
        }
      }
    ));
  }

  copyToken() {
    navigator.clipboard.writeText(this.userData.access_token);
  }

  gotoReferralList() {
    console.log('welcome:gotoReferralList:Redirect to home page');
    this.routerService.navigate(['/referral/list']);
  }
}
