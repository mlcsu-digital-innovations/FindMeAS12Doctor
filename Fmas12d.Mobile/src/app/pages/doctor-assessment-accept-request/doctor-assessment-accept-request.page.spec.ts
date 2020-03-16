import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorAssessmentAcceptRequestPage } from './doctor-assessment-accept-request.page';

describe('DoctorAssessmentAcceptRequestPage', () => {
  let component: DoctorAssessmentAcceptRequestPage;
  let fixture: ComponentFixture<DoctorAssessmentAcceptRequestPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DoctorAssessmentAcceptRequestPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorAssessmentAcceptRequestPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
