import { AmhpAssessmentList } from '../../models/amhp-assessment-list.model';
import { AmhpAssessmentService } from '../../services/amhp-assessment/amhp-assessment.service';
import { Component } from '@angular/core';
import { LoadingController } from '@ionic/angular';
import {
  REFERRALSTATUSASSESSMENTSCHEDULED, REFERRALSTATUS_SCHEDULED, REFERRALSTATUS_RESCHEDULING,
  REFERRALSTATUS_AWAITING_REVIEW, REFERRALSTATUS_NEW, REFERRALSTATUS_SELECTING,
  REFERRALSTATUS_AWAITING_RESPONSES, REFERRALSTATUS_RESPONSES_PARTIAL,
  REFERRALSTATUS_RESPONSES_COMPLETE
} from 'src/app/constants/app.constants';

import * as moment from 'moment';

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

  private allResults: AmhpAssessmentList[] = [];
  public showAll: boolean;

  private readonly scheduledStatuses: number[] =
  [REFERRALSTATUS_SCHEDULED, REFERRALSTATUS_RESCHEDULING];

  private readonly completedStatuses: number[] = [REFERRALSTATUS_AWAITING_REVIEW];

  private readonly unscheduledStatuses: number[] =
  [REFERRALSTATUS_NEW, REFERRALSTATUS_SELECTING, REFERRALSTATUS_AWAITING_RESPONSES,
    REFERRALSTATUS_RESPONSES_PARTIAL, REFERRALSTATUS_RESPONSES_COMPLETE];

  constructor(
    private assessmentService: AmhpAssessmentService,
    private loadingController: LoadingController
  ) { }

  ionViewDidEnter() {
    this.refreshPage();
  }

  toggleChanged(showAll: boolean) {
    this.filterData();
  }

  filterData() {

    this.assessmentListScheduled = this.allResults
    .filter(assessment => this.scheduledStatuses.includes(assessment.referralStatusId));

    this.assessmentListUnscheduled = this.allResults
    .filter(assessment => this.unscheduledStatuses.includes(assessment.referralStatusId));

    this.assessmentListComplete = this.allResults
    .filter(assessment => this.completedStatuses.includes(assessment.referralStatusId));

    if (!this.showAll) {
      const currentDate = moment().startOf('day');

      this.assessmentListScheduled =
        this.assessmentListScheduled
        .filter(assessment => moment(assessment.dateTime).isAfter(currentDate));

      this.assessmentListUnscheduled =
        this.assessmentListUnscheduled
        .filter(assessment => moment(assessment.dateTime).isAfter(currentDate));

      this.assessmentListComplete =
        this.assessmentListComplete
        .filter(assessment => moment(assessment.dateTime).isAfter(currentDate));
    }
  }

  refreshPage($event?: any) {
    const request = this.assessmentService.getList();
    this.showLoading();

    this.assessmentListScheduled = [];
    this.assessmentListUnscheduled = [];
    this.assessmentListComplete = [];


    request.subscribe((result: AmhpAssessmentList[]) => {
      this.assessmentListLastUpdated = new Date();
      this.allResults = result;
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
