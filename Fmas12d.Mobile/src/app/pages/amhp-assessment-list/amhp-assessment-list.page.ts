import { AmhpAssessmentList } from '../../models/amhp-assessment-list.model';
import { AmhpAssessmentService } from '../../services/amhp-assessment/amhp-assessment.service';
import { BroadcastService } from '@azure/msal-angular';
import { Component } from '@angular/core';
import { LoadingController } from '@ionic/angular';
import {
  REFERRALSTATUSASSESSMENTSCHEDULED, REFERRALSTATUS_SCHEDULED, REFERRALSTATUS_RESCHEDULING,
  REFERRALSTATUS_AWAITING_REVIEW, REFERRALSTATUS_NEW, REFERRALSTATUS_SELECTING,
  REFERRALSTATUS_AWAITING_RESPONSES, REFERRALSTATUS_RESPONSES_PARTIAL,
  REFERRALSTATUS_RESPONSES_COMPLETE
} from 'src/app/constants/app.constants';

@Component({
  selector: 'app-amhp-assessment-list',
  templateUrl: './amhp-assessment-list.page.html',
  styleUrls: ['./amhp-assessment-list.page.scss'],
})
export class AmhpAssessmentListPage {
  public assessmentListLastUpdated: Date;
  public assessmentListComplete: AmhpAssessmentList[] = [];
  public assessmentListScheduled: AmhpAssessmentList[] = [];
  public assessmentListUnscheduled: AmhpAssessmentList[] = [];
  private loading: HTMLIonLoadingElement;

  constructor(
    private assessmentService: AmhpAssessmentService,
    private broadcastService: BroadcastService,
    private loadingController: LoadingController
  ) { }

  ionViewDidEnter() {
    this.refreshPage();
  }

  refreshPage($event?: any) {
    const request = this.assessmentService.getList();
    this.showLoading();

    const scheduledStatuses: number[] =
      [REFERRALSTATUS_SCHEDULED, REFERRALSTATUS_RESCHEDULING];

    const completedStatuses: number[] =
      [REFERRALSTATUS_AWAITING_REVIEW];

    const unscheduledStatuses: number[] =
      [REFERRALSTATUS_NEW, REFERRALSTATUS_SELECTING, REFERRALSTATUS_AWAITING_RESPONSES,
        REFERRALSTATUS_RESPONSES_PARTIAL, REFERRALSTATUS_RESPONSES_COMPLETE];

    request.subscribe((result: AmhpAssessmentList[]) => {
      this.assessmentListLastUpdated = new Date();

      this.assessmentListScheduled = result
        .filter(assessment => scheduledStatuses.includes(assessment.referralStatusId));

      this.assessmentListUnscheduled = result
        .filter(assessment => unscheduledStatuses.includes(assessment.referralStatusId));

      this.assessmentListComplete = result
        .filter(assessment => completedStatuses.includes(assessment.referralStatusId));

      this.broadcastService.broadcast('assessments:requiringaction',
        this.assessmentListUnscheduled.length);

      this.closeLoading();
      this.closeRefreshing($event);
    }, error => {
      this.closeLoading();
      this.closeRefreshing($event);
    });
  }

  closeLoading() {
    if (this.loading) {
      setTimeout(() => { this.loading.dismiss(); }, 500);
    }
  }

  closeRefreshing($event?: any) {
    if ($event) {
      $event.target.complete();
    }
  }

  async showLoading() {
    this.loading = await this.loadingController.create({
      message: 'Please wait',
      spinner: 'lines',
      duration: 3000
    });
    await this.loading.present();
  }
}
