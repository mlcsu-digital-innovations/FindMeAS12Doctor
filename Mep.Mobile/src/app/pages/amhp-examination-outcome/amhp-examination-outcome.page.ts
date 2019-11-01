import { AlertController, NavController } from '@ionic/angular'
import { AmhpExaminationOutcome } from 'src/app/models/amhp-examination-outcome.model';
import { AmhpExaminationService } from '../../services/amhp-examination/amhp-examination.service';
import { AmhpExaminationView } from '../../models/amhp-examination-view.model';
import { AmhpExaminationViewDoctor } from 'src/app/models/amhp-examination-view-doctor.model';
import { Component, OnInit } from '@angular/core';
import { ToastService } from 'src/app/services/toast/toast.service';
import { UnsuccessfulExaminationType } from 'src/app/models/unsuccessful-examination-type.model';
import { UnsuccessfulExaminationTypeService } from
  'src/app/services/unsuccessful-examination-type/unsuccessful-examination-type.service';

@Component({
  selector: 'app-amhp-examination-outcome',
  templateUrl: './amhp-examination-outcome.page.html',
  styleUrls: ['./amhp-examination-outcome.page.scss'],
})
export class AmhpExaminationOutcomePage implements OnInit {
  public examinationLastUpdated: Date;
  public examinationSuccessful: boolean = true;
  public examinationView: AmhpExaminationView;
  public unsuccessfulExaminationTypeId?: number;
  public unsuccessfulExaminationTypeList: UnsuccessfulExaminationType[] = [];
  public unsuccessfulExaminationTypeName: string

  constructor(
    private alertCtrl: AlertController,
    private examinationService: AmhpExaminationService,
    private unsuccessfulExaminationTypeService: UnsuccessfulExaminationTypeService,
    private toastService: ToastService,
    private navCtrl: NavController
  ) {

  }

  ngOnInit() {
    this.examinationLastUpdated = new Date();
    this.examinationView = this.examinationService.retrieveView();

    if (this.examinationView.doctorsAllocated) {
      this.examinationView.doctorsAllocated.forEach(doctor => doctor.attended = true);
    }

    this.unsuccessfulExaminationTypeService.getList()
      .subscribe(data => this.unsuccessfulExaminationTypeList = data);
  }

  public async confirmSave() {
    let alert = await this.alertCtrl.create({
      header: 'Confirm Outcome',
      message: this.confirmMessage(),
      cssClass: 'amhp-examination-outcome-alert',
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
    return `<strong>Examination ${this.examinationSuccessful ? 'Successful' : 'Unsuccessful'}</strong><br />
            ${this.examinationSuccessful ? '' : '<strong>Unsuccessful Exam Type:</strong> ' +
        this.getUnsuccessfulExaminationTypeName() + '<br />'}
            <strong>Referral Id:</strong> ${this.examinationView.referralId}<br />
            <strong>Patient Id:</strong> ${this.examinationView.patientIdentifier}<br />
            <strong>Attending Doctors:</strong> ${this.getDoctorsAllocatedNames()}`;
  }

  private getUnsuccessfulExaminationTypeName(): string {
    if (this.unsuccessfulExaminationTypeId) {
      return this.unsuccessfulExaminationTypeList.filter((unsuccessfulExaminationType: UnsuccessfulExaminationType) =>
        this.unsuccessfulExaminationTypeId == unsuccessfulExaminationType.id)[0].name;
    }
    return '';
  }

  private getDoctorsAllocatedNames(): string {
    let attendingDoctors: AmhpExaminationViewDoctor[] = this.attendingDoctors();

    if (attendingDoctors && attendingDoctors.length > 0) {
      return attendingDoctors.map(doctor => doctor.displayName).join(", ");
    }

    return 'none';
  }

  private save(): void {
    let examinationOutcome: AmhpExaminationOutcome =
      new AmhpExaminationOutcome(
        new Date(),
        this.examinationView.doctorsAllocated,
        this.examinationView.id,
        this.examinationSuccessful ? null : this.unsuccessfulExaminationTypeId);
            
    this.examinationService.putOutcome(examinationOutcome, this.examinationView.id, this.examinationSuccessful)
      .subscribe(
        result => {
          this.toastService.displaySuccess({
            header: 'Examination Outcome Updated',
            message: ''
          });
          this.navCtrl.navigateRoot("amhp-examination-list");
        },
        error => {
          console.log(error);
          this.toastService.displayError({
            header: 'Error',
            message: error.error.title
          });
        });
  }

  private attendingDoctors(): AmhpExaminationViewDoctor[] {
    if (this.examinationView.doctorsAllocated) {
      return this.examinationView.doctorsAllocated.filter(doctor => doctor.attended);
    }
    return [];
  }
}
