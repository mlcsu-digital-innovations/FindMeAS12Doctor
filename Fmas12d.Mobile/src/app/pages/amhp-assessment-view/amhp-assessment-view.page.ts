import { ActivatedRoute } from '@angular/router'
import { AmhpAssessmentService } from '../../services/amhp-assessment/amhp-assessment.service'
import { AmhpAssessmentView } from '../../models/amhp-assessment-view.model';
import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-amhp-assessment-view',
  templateUrl: './amhp-assessment-view.page.html',
  styleUrls: ['./amhp-assessment-view.page.scss'],
})
export class AmhpAssessmentViewPage implements OnInit {
  public assessmentLastUpdated: Date;
  public assessmentView: AmhpAssessmentView;

  constructor(
    private route: ActivatedRoute,
    private assessmentService: AmhpAssessmentService,
    private navCtrl: NavController
  ) { }

  ngOnInit() {
    this.assessmentLastUpdated = new Date();
    let assessmentId = this.route.snapshot.paramMap.get('id');

    if (assessmentId) {
      this.assessmentService.getView(assessmentId)
        .subscribe(data => this.assessmentView = data);
    }

  }

  public updateAssessment(): void {
    this.assessmentService.storeView(this.assessmentView);
    this.navCtrl.navigateForward("amhp-assessment-outcome");
  }
}
