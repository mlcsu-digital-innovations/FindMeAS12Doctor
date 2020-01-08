import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorAssessmentsPage } from './doctor-assessments.page';

describe('DoctorAssessmentsPage', () => {
  let component: DoctorAssessmentsPage;
  let fixture: ComponentFixture<DoctorAssessmentsPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DoctorAssessmentsPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorAssessmentsPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
