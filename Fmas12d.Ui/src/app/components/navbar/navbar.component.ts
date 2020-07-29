import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { SecurityService } from 'src/app/services/security/security.service';
import { User } from 'src/app/interfaces/user.js';
import { UserDetailsService } from 'src/app/services/user/user-details.service';
import { version } from '../../../../package.json';

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
    public securityService: SecurityService,
    private userDetailsService: UserDetailsService
  ) { }

  ngOnInit() {
    this.isAuthorized$ = this.securityService.getIsAuthorized();
    this.user$ = this.userDetailsService.getCurrentUserDetails();
    this.user$.subscribe((user: User) => {
      if (user.isDoctor) {
        this.homePageLink = '/doctor/claims/list';
      } else if (user.isFinance) {
        this.homePageLink = '/finance/claims/list';
      } else {
        this.homePageLink = '/referral/list';
      }
    });
  }

  login() {
    this.securityService.login();
  }

  refreshSession() {
    this.securityService.refreshSession();
  }

  logout() {
    this.securityService.logout();
  }
}
