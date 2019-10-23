import { Component, OnInit } from '@angular/core';
import { AmhpExaminationList } from '../../models/amhp-examination-list.model';
import { AmhpExaminationService } from '../../services/amhp-examination.service/amhp-examination.service';

@Component({
  selector: 'app-amhp-examination-list',
  templateUrl: './amhp-examination-list.page.html',
  styleUrls: ['./amhp-examination-list.page.scss'],
})
export class AmhpExaminationListPage implements OnInit {
  public examinationListLastUpdated: Date;
  public examinationList: AmhpExaminationList[];

  constructor(private examinationService: AmhpExaminationService) { }

  ngOnInit() {
    this.examinationListLastUpdated = new Date();
    this.examinationService.getList(10)
      .subscribe((result: AmhpExaminationList[]) =>
        this.examinationList = result
      );
  }

}
