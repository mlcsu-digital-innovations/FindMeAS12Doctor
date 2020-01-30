import { OnCallDoctorModalComponent } from './on-call-doctor-modal.component';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

describe('OnCallDoctorModalComponent', () => {
  let component: OnCallDoctorModalComponent;
  let fixture: ComponentFixture<OnCallDoctorModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OnCallDoctorModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OnCallDoctorModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
