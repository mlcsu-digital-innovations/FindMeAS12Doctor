import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { RouterService } from 'src/app/services/router/router.service';
import { Subscription } from 'rxjs';
import { SecurityService } from 'src/app/services/security/security.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {

  isAuthorized: boolean;
  isAuthorizedSubscription: Subscription;
  isDevelopment: boolean;
  userData: any;
  userDataSubscription: Subscription;

  services: string[] = [
    'Research and Development',
    'Advisory Services',
    'Business Analysis',
    'Evidence Review',
    'Digital Strategy Development',
    'Consensus Building  Evaluation',
    'Leadership',
    'Transformational Change',
    'Proof of Concept',
    'Agile Software Development',
    'Implementation'
  ];

constructor(
    private routerService: RouterService,
    private securityService: SecurityService) {}

ngOnInit() {

    this.isDevelopment = !environment.production;

  }


login() {
  this.securityService.login();
}

}
