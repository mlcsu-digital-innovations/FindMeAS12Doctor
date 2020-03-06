import { AlertController, NavController, LoadingController } from '@ionic/angular'
import { AmhpAssessmentOutcome } from 'src/app/models/amhp-assessment-outcome.model';
import { AmhpAssessmentService } from '../../services/amhp-assessment/amhp-assessment.service';
import { AmhpAssessmentView } from '../../models/amhp-assessment-view.model';
import { AssessmentViewDoctor } from 'src/app/models/assessment-view-doctor.model';
import { Component, OnInit } from '@angular/core';
import { ToastService } from 'src/app/services/toast/toast.service';
import { UnsuccessfulAssessmentType } from 'src/app/models/unsuccessful-assessment-type.model';
import { UnsuccessfulAssessmentTypeService } from
  'src/app/services/unsuccessful-assessment-type/unsuccessful-assessment-type.service';

@Component({
  selector: 'app-amhp-assessment-outcome',
  templateUrl: './amhp-assessment-outcome.page.html',
  styleUrls: ['./amhp-assessment-outcome.page.scss'],
})
export class AmhpAssessmentOutcomePage implements OnInit {
  public assessmentLastUpdated: Date;  
  public assessmentView: AmhpAssessmentView;
  private loading: HTMLIonLoadingElement; 
  public assessmentStatusId?: number;
  public assessmentStatusList: UnsuccessfulAssessmentType[] = [];
  public assessmentStatusName: string;

  constructor(
    private alertCtrl: AlertController,
    private assessmentService: AmhpAssessmentService,
    private loadingController: LoadingController,
    private unsuccessfulAssessmentTypeService: UnsuccessfulAssessmentTypeService,
    private toastService: ToastService,
    private navCtrl: NavController
  ) {

  }

  ngOnInit() {    
    this.showLoading();
    this.assessmentView = this.assessmentService.retrieveView();

    if (this.assessmentView.doctorsAllocated) {
      this.assessmentView.doctorsAllocated.forEach((doctor: AssessmentViewDoctor) => 
        doctor.attended = true);
    }

    const request = this.unsuccessfulAssessmentTypeService.getList();

    request.subscribe((data: UnsuccessfulAssessmentType[]) => 
      {
        this.assessmentLastUpdated = new Date();
        this.assessmentStatusList = data;

        let success: UnsuccessfulAssessmentType = new UnsuccessfulAssessmentType();
        success.id = 0;
        success.name = 'Successful';
        success.description = 'Successful';

        this.assessmentStatusList.unshift(success);
        this.closeLoading();
      }, error => {
        this.toastService.displayError({
          message: 'Unable to retrieve unsuccessful assessment types'
        });
        this.closeLoading();
      });
  }

  closeLoading() {
    if (this.loading) {
      this.loading.dismiss();
    }
  }  

  public async confirmSave() {
    let alert = await this.alertCtrl.create({
      header: 'Confirm Outcome',
      message: this.confirmMessage(),
      cssClass: 'amhp-assessment-outcome-alert',
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel'
        },
        {
          text: 'Ok',
          handler: () => {
            this.save();
          }
        }
      ]
    });

    await alert.present();
  }

  private confirmMessage(): string {
    return `<strong>Assessment 
      ${this.assessmentStatusId === 0 ? 'Successful' : 'Unsuccessful'}</strong><br />
            ${this.assessmentStatusId === 0 ? '' : 'Reason: <strong>' +
        this.getAssessmentStatusName() + '</strong><br />'}
            Referral Id: <strong>${this.assessmentView.referralId}</strong><br />
            Patient Id: <strong>${this.assessmentView.patientIdentifier}</strong><br />
            Attending Doctors: <strong>${this.getDoctorsAllocatedNames()}</strong>`;
  }

  private getAssessmentStatusName(): string {
    if (this.assessmentStatusId) {
      return this.assessmentStatusList.filter((assessmentStatus: UnsuccessfulAssessmentType) =>
        this.assessmentStatusId == assessmentStatus.id)[0].name;
    }
    return '';
  }

  private getDoctorsAllocatedNames(): string {
    let attendingDoctors: AssessmentViewDoctor[] = this.attendingDoctors();

    if (attendingDoctors && attendingDoctors.length > 0) {
      return attendingDoctors.map(doctor => doctor.displayName).join(", ");
    }

    return 'none';
  }

  private save(): void {
    let assessmentOutcome: AmhpAssessmentOutcome =
      new AmhpAssessmentOutcome(
        new Date(),
        this.assessmentView.doctorsAllocated,
        this.assessmentView.id,
        this.assessmentStatusId === 0 ? null : this.assessmentStatusId);
            
    this.assessmentService.putOutcome(assessmentOutcome, this.assessmentView.id, this.assessmentStatusId === 0)
      .subscribe(
        result => {
          this.toastService.displaySuccess({
            message: 'Assessment Outcome Updated'
          });
          this.navCtrl.navigateRoot('amhp-assessment-list');
        },
        error => {
          this.toastService.displayError({
            header: 'Error',
            message: error.error.title
          });
        });
  }

  async showLoading() {
    this.loading = await this.loadingController.create({
      message: 'Please wait',
      spinner: 'lines',
      duration: 5000
    });
    await this.loading.present();
  }

  private attendingDoctors(): AssessmentViewDoctor[] {
    if (this.assessmentView.doctorsAllocated) {
      return this.assessmentView.doctorsAllocated.filter((doctor: AssessmentViewDoctor) =>
        doctor.attended);
    }
    return [];
  }
}
