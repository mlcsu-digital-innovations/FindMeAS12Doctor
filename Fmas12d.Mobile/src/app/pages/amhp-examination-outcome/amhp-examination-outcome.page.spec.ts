import { AmhpExaminationOutcomePage } from './amhp-examination-outcome.page';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

describe('AmhpExaminationOutcomePage', () => {
  let component: AmhpExaminationOutcomePage;
  let fixture: ComponentFixture<AmhpExaminationOutcomePage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AmhpExaminationOutcomePage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AmhpExaminationOutcomePage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
