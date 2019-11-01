import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AmhpExaminationViewPage } from './amhp-examination-view.page';

describe('AmhpExaminationViewPage', () => {
  let component: AmhpExaminationViewPage;
  let fixture: ComponentFixture<AmhpExaminationViewPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AmhpExaminationViewPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AmhpExaminationViewPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
