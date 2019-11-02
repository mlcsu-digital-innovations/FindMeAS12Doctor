import { AmhpAssessmentList } from '../../models/amhp-assessment-list.model';
import { AmhpAssessmentService } from '../../services/amhp-assessment/amhp-assessment.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-amhp-assessment-list',
  templateUrl: './amhp-assessment-list.page.html',
  styleUrls: ['./amhp-assessment-list.page.scss'],
})
export class AmhpAssessmentListPage implements OnInit {
  public assessmentListLastUpdated: Date;
  public assessmentList$: Observable<AmhpAssessmentList[]>;

  constructor(private assessmentService: AmhpAssessmentService) { }

  ngOnInit() {
    this.assessmentListLastUpdated = new Date();    

    // currently set to get assessments for user id 9
    this.assessmentList$ = this.assessmentService.getList(9);
  }

}
