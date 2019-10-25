import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { Observable } from 'rxjs';
import { AmhpExaminationView } from '../../models/amhp-examination-view.model';
import { AmhpExaminationService } from 
  '../../services/amhp-examination.service/amhp-examination.service'

@Component({
  selector: 'app-amhp-examination-view',
  templateUrl: './amhp-examination-view.page.html',
  styleUrls: ['./amhp-examination-view.page.scss'],
})
export class AmhpExaminationViewPage implements OnInit {
  public examinationLastUpdated: Date;
  public examinationView$: Observable<AmhpExaminationView>;

  constructor(
    private route: ActivatedRoute,
    private examinationService: AmhpExaminationService
  ) { }

  ngOnInit() {
    this.examinationLastUpdated = new Date();
    let examinationId = this.route.snapshot.paramMap.get('id');

    if (examinationId) {
      this.examinationView$ = this.examinationService.getView(examinationId);
    }

  }

}
