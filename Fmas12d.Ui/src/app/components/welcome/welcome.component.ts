import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { RouterService } from 'src/app/services/router/router.service';
import { Subscription } from 'rxjs';
import { SecurityService } from 'src/app/services/security/security.service';
import { User } from 'src/app/interfaces/user';
import { UserDetailsService } from 'src/app/services/user/user-details.service';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent implements OnInit {

  userData: any;
  userDataSubscription: Subscription;

constructor(
    private oidcSecurityService: OidcSecurityService,
    private securityService: SecurityService,
    private userDetailsService: UserDetailsService,
    private routerService: RouterService) {}

ngOnInit() {

    this.oidcSecurityService.checkAuth()
    .pipe(take(1))
    .subscribe((auth) => {

      if (auth) {
        this.oidcSecurityService.userData$
        .pipe(take(1))
        .subscribe((userData) => {
          this.userData = userData;

          if (this.userData !== null) {
            this.userData.access_token = this.oidcSecurityService.getToken();

            let url = '/referral/list';

            this.userDetailsService.getCurrentUserDetails()
            .pipe(take(1))
            .subscribe((user: User) => {

              if (user.isDoctor) { url = '/doctor/claims/list'; }

              if (user.isFinance) { url = '/finance/claims/list'; }

              this.routerService.navigate([url]);
            });
          }
        });
      }

    });
  }

login() {
  this.securityService.login();
}

copyToken() {
  navigator.clipboard.writeText(this.userData.access_token);
}

scroll(el: HTMLElement) {
  el.scrollIntoView({behavior: 'smooth'});
}

gotoReferralList() {
    this.routerService.navigate(['/referral/list']);
  }
}
