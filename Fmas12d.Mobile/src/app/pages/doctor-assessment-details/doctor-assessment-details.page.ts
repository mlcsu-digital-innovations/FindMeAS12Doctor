import { ActivatedRoute } from '@angular/router';
import { AmhpAssessmentService } from 'src/app/services/amhp-assessment/amhp-assessment.service';
import { AmhpAssessmentView } from 'src/app/models/amhp-assessment-view.model';
import { CallNumber } from '@ionic-native/call-number/ngx';
import { Component, OnInit } from '@angular/core';
import { LoadingController, AlertController } from '@ionic/angular';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-doctor-assessment-details',
  templateUrl: './doctor-assessment-details.page.html',
  styleUrls: ['./doctor-assessment-details.page.scss'],
})
export class DoctorAssessmentDetailsPage implements OnInit {

  private loading: HTMLIonLoadingElement;
  public assessment: AmhpAssessmentView;

  constructor(
    private alertController: AlertController,
    private callNumber: CallNumber,
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
        this.assessment = result;
        console.log(this.assessment);
        this.closeLoading();
      }, error => {
        this.closeLoading();
        this.toastService.displayError({
          message: 'Unable to retrieve assessment details'
        });
      });
    }
  }

  callContact(contactNumber: string) {

    if (!isNaN(+contactNumber)) {
      this.callNumber.callNumber(contactNumber, true)
      .then(res => console.log('launched dialler', res))
      .catch(err => this.toastService.displayError({message: 'Unable to launch dialler'}));
    }
  }

  closeLoading() {
    if (this.loading) {
      setTimeout(() => { this.loading.dismiss(); }, 500);
    }
  }

  public async confirmCallContact(contactNumber: string) {
    const alert = await this.alertController.create({
      header: 'Confirm Call',
      message: `Call ${this.assessment.amhpUserName} ?`,
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel',
          cssClass: 'secondary',
          handler: (blah) => {
            console.log('Confirm Cancel: blah');
          }
        }, {
          text: 'Call',
          handler: () => {
            this.callContact(contactNumber);
          }
        }
      ]
    });

    await alert.present();
  }

  async showLoading() {
    this.loading = await this.loadingController.create({
      message: 'Please wait',
      spinner: 'lines'
    });
    await this.loading.present();
  }
}
