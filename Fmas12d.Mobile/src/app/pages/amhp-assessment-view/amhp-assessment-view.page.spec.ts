import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from 'src/app/pages/amhp-assessment-outcome/node_modules/src/app/pages/amhp-assessment-list/node_modules/@angular/core/testing';

import { AmhpAssessmentViewPage } from './amhp-assessment-view.page';

describe('AmhpAssessmentViewPage', () => {
  let component: AmhpAssessmentViewPage;
  let fixture: ComponentFixture<AmhpAssessmentViewPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AmhpAssessmentViewPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AmhpAssessmentViewPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
