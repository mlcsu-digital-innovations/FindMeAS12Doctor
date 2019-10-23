import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-amhp-examination-list',
  templateUrl: './amhp-examination-list.page.html',
  styleUrls: ['./amhp-examination-list.page.scss'],
})
export class AmhpExaminationListPage implements OnInit {
  public examinationListLastUpdated: string;
  constructor() { }

  ngOnInit() {
  }

}
