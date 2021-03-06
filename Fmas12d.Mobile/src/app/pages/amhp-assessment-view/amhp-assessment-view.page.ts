import { ActivatedRoute } from '@angular/router'
import { AmhpAssessmentService } from '../../services/amhp-assessment/amhp-assessment.service'
import { AmhpAssessmentView } from '../../models/amhp-assessment-view.model';
import { Component, OnInit } from '@angular/core';
import { LoadingController, NavController, ModalController } from '@ionic/angular';
import { ToastService } from 'src/app/services/toast/toast.service';
import { AssessmentSelectedDoctor } from 'src/app/models/assessment-selected-doctor.model';
import { UserContactModalPage } from '../user-contact-modal/user-contact-modal.page';

@Component({
  selector: 'app-amhp-assessment-view',
  templateUrl: './amhp-assessment-view.page.html',
  styleUrls: ['./amhp-assessment-view.page.scss'],
})
export class AmhpAssessmentViewPage implements OnInit {
  public assessmentLastUpdated: Date;
  public assessmentView: AmhpAssessmentView;
  public displayDoctors: boolean;
  private loading: HTMLIonLoadingElement;
  private modal: HTMLIonModalElement;

  constructor(
    private route: ActivatedRoute,
    private assessmentService: AmhpAssessmentService,
    private modalController: ModalController,
    private navCtrl: NavController,
    private loadingController: LoadingController,
    private toastService: ToastService
  ) { }

  ngOnInit() {
    this.displayDoctors = false;
    const assessmentId = +this.route.snapshot.paramMap.get('id');
    this.assessmentView = {} as AmhpAssessmentView;
    this.assessmentView.detailTypes = [];

    if (assessmentId) {
      const request = this.assessmentService.getView(assessmentId);
      this.showLoading();

      request.subscribe((result: AmhpAssessmentView) => {
        this.assessmentLastUpdated = new Date();
        this.assessmentView = result;

        if (this.assessmentView.doctorsAllocated && this.assessmentView.doctorsAllocated.length > 0 ||
          this.assessmentView.doctorsSelected && this.assessmentView.doctorsSelected.length > 0)
        {
          this.displayDoctors = true;
        }
        this.closeLoading();
      }, error => {
        this.closeLoading();
        this.toastService.displayError({
          message: 'Unable to retrieve assessment details'
        });
      });
    }

  }

  closeLoading() {
    if (this.loading) {
      setTimeout(() => { this.loading.dismiss(); }, 500);
    }
  }

  onModalAction(action: boolean) {
    this.modal.dismiss();
  }

  async showContacts(doctor: AssessmentSelectedDoctor) {
    console.log(doctor);

    this.modal = await this.modalController.create({
      component: UserContactModalPage,
      componentProps: {
        'userName': doctor.displayName,
        'contactDetails': doctor.contactDetails,
        'actioned': this.onModalAction
      }
    });

    return await this.modal.present();
  }

  async showLoading() {
    this.loading = await this.loadingController.create({
      message: 'Please wait',
      spinner: 'lines'
    });
    await this.loading.present();
  }

  public updateAssessment(): void {
    this.assessmentService.storeView(this.assessmentView);
    this.navCtrl.navigateForward('amhp-assessment-outcome');
  }
}
