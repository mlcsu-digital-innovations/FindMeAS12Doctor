import { ActivatedRoute } from '@angular/router';
import { AmhpAssessmentService } from 'src/app/services/amhp-assessment/amhp-assessment.service';
import { AmhpAssessmentView } from 'src/app/models/amhp-assessment-view.model';
import { Component, OnInit } from '@angular/core';
import { LoadingController } from '@ionic/angular';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-doctor-assessment-details',
  templateUrl: './doctor-assessment-details.page.html',
  styleUrls: ['./doctor-assessment-details.page.scss'],
})
export class DoctorAssessmentDetailsPage implements OnInit {

  private loading: HTMLIonLoadingElement;
  private assessment: AmhpAssessmentView;

  constructor(
    private route: ActivatedRoute,
    private assessmentService: AmhpAssessmentService,
    private loadingController: LoadingController,
    private toastService: ToastService
  ) { }

  ngOnInit() {
    this.assessment = new AmhpAssessmentView();
    this.assessment.detailTypes = [];
    const assessmentId = +this.route.snapshot.paramMap.get('id');

    if (assessmentId) {
      const request = this.assessmentService.getView(assessmentId);
      this.showLoading();

      request.subscribe((result: AmhpAssessmentView) => {
        console.log(result);
        this.assessment = result;

        this.closeLoading();
      }, error => {
        this.closeLoading();
        this.toastService.displayError({
          message: "Unable to retrieve assessment details"
        });
      });
    }
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
