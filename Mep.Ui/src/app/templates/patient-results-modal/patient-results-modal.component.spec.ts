import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientResultsModalComponent } from './patient-results-modal.component';

describe('PatientResultsModalComponent', () => {
  let component: PatientResultsModalComponent;
  let fixture: ComponentFixture<PatientResultsModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientResultsModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientResultsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
