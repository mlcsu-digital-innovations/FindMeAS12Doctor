import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorAssessmentRequestResponsePage } from './doctor-assessment-request-response.page';

describe('AmhpAssessmentRequestResponsePage', () => {
  let component: DoctorAssessmentRequestResponsePage;
  let fixture: ComponentFixture<DoctorAssessmentRequestResponsePage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DoctorAssessmentRequestResponsePage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorAssessmentRequestResponsePage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
