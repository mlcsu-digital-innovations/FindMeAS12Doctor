import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorAssessmentDetailsPage } from './doctor-assessment-details.page';

describe('DoctorAssessmentDetailsPage', () => {
  let component: DoctorAssessmentDetailsPage;
  let fixture: ComponentFixture<DoctorAssessmentDetailsPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DoctorAssessmentDetailsPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorAssessmentDetailsPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
