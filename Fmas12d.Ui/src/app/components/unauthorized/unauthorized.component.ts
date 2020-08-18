import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-unauthorized',
  templateUrl: './unauthorized.component.html',
  styleUrls: ['./unauthorized.component.css']
})
export class UnauthorizedComponent implements OnInit {

  public userUpn = '';

  constructor(private oidcSecurityService: OidcSecurityService) {}

  ngOnInit() {

    this.oidcSecurityService.userData$
    .subscribe(data => {
      this.userUpn = data.upn;
    }, err => {
      this.userUpn = 'Unknown';
    });
  }

  login() {
    // this.oidcSecurityService.authorize();
  }
}
