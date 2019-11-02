import { AmhpAssessmentOutcomePage } from './amhp-assessment-outcome.page';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from 'src/app/pages/amhp-assessment-list/node_modules/@angular/core/testing';

describe('AmhpAssessmentOutcomePage', () => {
  let component: AmhpAssessmentOutcomePage;
  let fixture: ComponentFixture<AmhpAssessmentOutcomePage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AmhpAssessmentOutcomePage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AmhpAssessmentOutcomePage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
