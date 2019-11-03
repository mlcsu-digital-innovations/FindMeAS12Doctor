import { AlertController, NavController } from '@ionic/angular'
import { AmhpAssessmentOutcome } from 'src/app/models/amhp-assessment-outcome.model';
import { AmhpAssessmentService } from '../../services/amhp-assessment/amhp-assessment.service';
import { AmhpAssessmentView } from '../../models/amhp-assessment-view.model';
import { AmhpAssessmentViewDoctor } from 'src/app/models/amhp-assessment-view-doctor.model';
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
  public assessmentSuccessful: boolean = true;
  public assessmentView: AmhpAssessmentView;
  public unsuccessfulAssessmentTypeId?: number;
  public unsuccessfulAssessmentTypeList: UnsuccessfulAssessmentType[] = [];
  public unsuccessfulAssessmentTypeName: string

  constructor(
    private alertCtrl: AlertController,
    private assessmentService: AmhpAssessmentService,
    private unsuccessfulAssessmentTypeService: UnsuccessfulAssessmentTypeService,
    private toastService: ToastService,
    private navCtrl: NavController
  ) {

  }

  ngOnInit() {
    this.assessmentLastUpdated = new Date();
    this.assessmentView = this.assessmentService.retrieveView();

    if (this.assessmentView.doctorsAllocated) {
      this.assessmentView.doctorsAllocated.forEach(doctor => doctor.attended = true);
    }

    this.unsuccessfulAssessmentTypeService.getList()
      .subscribe(data => this.unsuccessfulAssessmentTypeList = data);
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
    return `<strong>Assessment ${this.assessmentSuccessful ? 'Successful' : 'Unsuccessful'}</strong><br />
            ${this.assessmentSuccessful ? '' : '<strong>Unsuccessful Assessment Type:</strong> ' +
        this.getUnsuccessfulAssessmentTypeName() + '<br />'}
            <strong>Referral Id:</strong> ${this.assessmentView.referralId}<br />
            <strong>Patient Id:</strong> ${this.assessmentView.patientIdentifier}<br />
            <strong>Attending Doctors:</strong> ${this.getDoctorsAllocatedNames()}`;
  }

  private getUnsuccessfulAssessmentTypeName(): string {
    if (this.unsuccessfulAssessmentTypeId) {
      return this.unsuccessfulAssessmentTypeList.filter((unsuccessfulAssessmentType: UnsuccessfulAssessmentType) =>
        this.unsuccessfulAssessmentTypeId == unsuccessfulAssessmentType.id)[0].name;
    }
    return '';
  }

  private getDoctorsAllocatedNames(): string {
    let attendingDoctors: AmhpAssessmentViewDoctor[] = this.attendingDoctors();

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
        this.assessmentSuccessful ? null : this.unsuccessfulAssessmentTypeId);
            
    this.assessmentService.putOutcome(assessmentOutcome, this.assessmentView.id, this.assessmentSuccessful)
      .subscribe(
        result => {
          this.toastService.displaySuccess({            
            message: 'Assessment Outcome Updated'
          });
          this.navCtrl.navigateRoot("amhp-assessment-list");
        },
        error => {
          this.toastService.displayError({
            header: 'Error',
            message: error.error.title
          });
        });
  }

  private attendingDoctors(): AmhpAssessmentViewDoctor[] {
    if (this.assessmentView.doctorsAllocated) {
      return this.assessmentView.doctorsAllocated.filter(doctor => doctor.attended);
    }
    return [];
  }
}
