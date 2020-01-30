import { OnCallDoctorListComponent } from './on-call-doctor-list.component';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { OnCallDoctorListService } from 'src/app/services/on-call-doctor-list/on-call-doctor-list.service';
import { of } from 'rxjs';
import { ToastService } from 'src/app/services/toast/toast.service';

describe('OnCallDoctorListComponent', () => {
  let component: OnCallDoctorListComponent;
  let fixture: ComponentFixture<OnCallDoctorListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OnCallDoctorListComponent ],
      providers: [ ToastService ] 
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
