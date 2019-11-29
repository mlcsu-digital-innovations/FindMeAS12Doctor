import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-amhp-assessment-accept-request',
  templateUrl: './amhp-assessment-accept-request.page.html',
  styleUrls: ['./amhp-assessment-accept-request.page.scss'],
})
export class AmhpAssessmentAcceptRequestPage implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) { 
    this.route.queryParams.subscribe(
      params => {
        if (this.router.getCurrentNavigation().extras.state) {
          console.log(this.router.getCurrentNavigation().extras.state.assessment);
        }
      }
    )
  }

  ngOnInit() {
  }

}
