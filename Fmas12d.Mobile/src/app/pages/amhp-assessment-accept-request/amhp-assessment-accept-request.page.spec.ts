import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AmhpAssessmentAcceptRequestPage } from './amhp-assessment-accept-request.page';

describe('AmhpAssessmentAcceptRequestPage', () => {
  let component: AmhpAssessmentAcceptRequestPage;
  let fixture: ComponentFixture<AmhpAssessmentAcceptRequestPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AmhpAssessmentAcceptRequestPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AmhpAssessmentAcceptRequestPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
