import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { PatientResultsModalComponent } from './patient-results-modal.component';
import { PatientSearchResult } from 'src/app/interfaces/patient-search-result';

describe('PatientResultsModalComponent', () => {
  let component: PatientResultsModalComponent;
  let fixture: ComponentFixture<PatientResultsModalComponent>;

  let patientResult: PatientSearchResult;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientResultsModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientResultsModalComponent);
    component = fixture.componentInstance;

    patientResult = {} as PatientSearchResult;
    patientResult.alternativeIdentifier = 'Test';
    patientResult.currentReferralId = 1;

    component.patientResult = patientResult;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
