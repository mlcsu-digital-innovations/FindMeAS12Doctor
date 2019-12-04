import { AmhpAssessmentRequest } from 'src/app/models/amhp-assessment-request.model';
import { AmhpAssessmentService } from 'src/app/services/amhp-assessment/amhp-assessment.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { LoadingController } from '@ionic/angular';

@Component({
  selector: 'app-amhp-assessment-requests',
  templateUrl: './amhp-assessment-requests.page.html',
  styleUrls: ['./amhp-assessment-requests.page.scss'],
})
export class AmhpAssessmentRequestsPage implements OnInit {

  public assessmentRequestsLastUpdated: Date;
  public assessmentRequests$: Observable<AmhpAssessmentRequest[]>;
  public assessmentRequests: AmhpAssessmentRequest[] = [];

  private loading: HTMLIonLoadingElement;

  constructor(
    private assessmentService: AmhpAssessmentService,
    private loadingController: LoadingController) { }

  ngOnInit() {
  }

  ionViewDidEnter() {
    // load data each time the page is shown
    // this may be changed if offline data is to be used
    const request = this.assessmentService.getRequests();
    this.showLoading();

    request
      .subscribe(
        result => {
          this.assessmentRequests = result;
          this.assessmentRequestsLastUpdated = new Date();
          this.closeLoading();
        }, error => {
          this.closeLoading();
        }
      );
  }

  closeLoading() {
    if (this.loading) {
      this.loading.dismiss();
    }
  }

  async showLoading() {
    this.loading = await this.loadingController.create({
      message: 'Please wait',
      spinner: 'lines'
    });
    await this.loading.present();
  }
}
