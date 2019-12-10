import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AmhpAssessmentRequestResponsePage } from './amhp-assessment-request-response.page';

describe('AmhpAssessmentRequestResponsePage', () => {
  let component: AmhpAssessmentRequestResponsePage;
  let fixture: ComponentFixture<AmhpAssessmentRequestResponsePage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AmhpAssessmentRequestResponsePage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AmhpAssessmentRequestResponsePage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
