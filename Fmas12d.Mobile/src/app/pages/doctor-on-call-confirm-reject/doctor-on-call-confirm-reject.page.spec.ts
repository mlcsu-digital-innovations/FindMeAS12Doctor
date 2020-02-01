import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { DoctorOnCallConfirmRejectPage } from './doctor-on-call-confirm-reject.page';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

describe('DoctorOnCallConfirmRejectPage', () => {
  let component: DoctorOnCallConfirmRejectPage;
  let fixture: ComponentFixture<DoctorOnCallConfirmRejectPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DoctorOnCallConfirmRejectPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorOnCallConfirmRejectPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
