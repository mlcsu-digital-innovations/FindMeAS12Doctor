import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { RouterService } from 'src/app/services/router/router.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent implements OnInit {

  userDataSubscription: Subscription;
  userData: any;

  constructor(
    private oidcSecurityService: OidcSecurityService,
    private routerService: RouterService) 
  { }

  ngOnInit() {
    this.userDataSubscription = this.oidcSecurityService.getUserData().subscribe(userData => {
      this.userData = userData;
      this.userData.access_token = this.oidcSecurityService.getToken();
    });    
  }

  ngOnDestroy() {
    this.userDataSubscription.unsubscribe();
  }  

  login() {
    this.oidcSecurityService.authorize();
  }

  gotoReferralList() {
    this.routerService.navigate(['/referral/list']);
  }

}
