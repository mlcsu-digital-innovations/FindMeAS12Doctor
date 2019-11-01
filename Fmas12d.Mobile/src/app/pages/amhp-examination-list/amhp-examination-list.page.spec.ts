import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AmhpExaminationListPage } from './amhp-examination-list.page';

describe('AmhpExaminationListPage', () => {
  let component: AmhpExaminationListPage;
  let fixture: ComponentFixture<AmhpExaminationListPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AmhpExaminationListPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AmhpExaminationListPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
