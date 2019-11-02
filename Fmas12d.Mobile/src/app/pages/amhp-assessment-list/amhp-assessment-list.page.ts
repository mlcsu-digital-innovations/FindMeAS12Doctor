import { AmhpExaminationList } from '../../models/amhp-assessment-list.model';
import { AmhpExaminationService } from '../../services/amhp-assessment/amhp-assessment.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-amhp-examination-list',
  templateUrl: './amhp-examination-list.page.html',
  styleUrls: ['./amhp-examination-list.page.scss'],
})
export class AmhpExaminationListPage implements OnInit {
  public examinationListLastUpdated: Date;
  public examinationList$: Observable<AmhpExaminationList[]>;

  constructor(private examinationService: AmhpExaminationService) { }

  ngOnInit() {
    this.examinationListLastUpdated = new Date();    

    // currently set to get examinations for user id 9
    this.examinationList$ = this.examinationService.getList(9);
  }

}
