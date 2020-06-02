import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';
import { UserDetailsService } from 'src/app/services/user/user-details.service.js';
import { User } from 'src/app/interfaces/user.js';
import { version } from '../../../../package.json';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  public homePageLink: string;
  public version: string = version;
  isAuthorized$: Observable<boolean>;
  user$: Observable<User>;  

  constructor(
    public oidcSecurityService: OidcSecurityService,
    private userDetailsService: UserDetailsService
  ) { }

  ngOnInit() {
    this.isAuthorized$ = this.oidcSecurityService.getIsAuthorized();    
    this.user$ = this.userDetailsService.getCurrentUserDetails();
    this.user$.subscribe((user: User) => {
      if (user.isDoctor) {
        this.homePageLink = '/doctor/claims/list';
      }
      else if (user.isFinance) {
        this.homePageLink = '/finance/claims/list';
      } else {
        this.homePageLink = '/referral/list';
      }
    });
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
}
