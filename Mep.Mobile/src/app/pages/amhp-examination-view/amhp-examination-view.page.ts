import { ActivatedRoute } from '@angular/router'
import { AmhpExaminationService } from '../../services/amhp-examination/amhp-examination.service'
import { AmhpExaminationView } from '../../models/amhp-examination-view.model';
import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-amhp-examination-view',
  templateUrl: './amhp-examination-view.page.html',
  styleUrls: ['./amhp-examination-view.page.scss'],
})
export class AmhpExaminationViewPage implements OnInit {
  public examinationLastUpdated: Date;
  public examinationView: AmhpExaminationView;

  constructor(
    private route: ActivatedRoute,
    private examinationService: AmhpExaminationService,
    private navCtrl: NavController
  ) { }

  ngOnInit() {
    this.examinationLastUpdated = new Date();
    let examinationId = this.route.snapshot.paramMap.get('id');

    if (examinationId) {
      this.examinationService.getView(examinationId)
        .subscribe(data => this.examinationView = data);
    }

  }

  public updateExamination(): void {
    this.examinationService.storeView(this.examinationView);
    this.navCtrl.navigateForward("amhp-examination-outcome");
  }
}
