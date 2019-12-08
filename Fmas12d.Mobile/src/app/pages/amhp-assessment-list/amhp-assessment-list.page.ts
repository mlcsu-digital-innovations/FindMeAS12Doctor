import { AmhpAssessmentList } from '../../models/amhp-assessment-list.model';
import { AmhpAssessmentService } from '../../services/amhp-assessment/amhp-assessment.service';
import { Component, OnInit } from '@angular/core';
import { LoadingController } from '@ionic/angular';
import { REFERRALSTATUSASSESSMENTSCHEDULED } from 'src/app/constants/app.constants';

@Component({
  selector: 'app-amhp-assessment-list',
  templateUrl: './amhp-assessment-list.page.html',
  styleUrls: ['./amhp-assessment-list.page.scss'],
})
export class AmhpAssessmentListPage implements OnInit {
  public assessmentListLastUpdated: Date;  
  public assessmentListScheduled: AmhpAssessmentList[] = [];
  public assessmentListUnscheduled: AmhpAssessmentList[] = [];
  private loading: HTMLIonLoadingElement;

  constructor(
    private assessmentService: AmhpAssessmentService,
    private loadingController: LoadingController
    ) { }

  ngOnInit() {        
    const request = this.assessmentService.getList();
    this.showLoading();

    request.subscribe((result: AmhpAssessmentList[]) => {
      this.assessmentListLastUpdated = new Date();
      this.assessmentListScheduled = result
        .filter(assessment => assessment.referralStatusId >= REFERRALSTATUSASSESSMENTSCHEDULED);
      this.assessmentListUnscheduled = result
        .filter(assessment => assessment.referralStatusId < REFERRALSTATUSASSESSMENTSCHEDULED);
      this.closeLoading();
    }, error => {
      this.closeLoading();
    });
  }

  closeLoading() {
    if (this.loading) {
      this.loading.dismiss();
    }
  }

  async showLoading() {
    this.loading = await this.loadingController.create({
      message: 'Please wait',
      spinner: 'lines',
      duration: 5000
    });
    await this.loading.present();
  }
}
