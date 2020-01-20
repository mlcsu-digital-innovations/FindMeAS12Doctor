import { OnCallDoctorListComponent } from './on-call-doctor-list.component';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

describe('OnCallDoctorListComponent', () => {
  let component: OnCallDoctorListComponent;
  let fixture: ComponentFixture<OnCallDoctorListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OnCallDoctorListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OnCallDoctorListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
