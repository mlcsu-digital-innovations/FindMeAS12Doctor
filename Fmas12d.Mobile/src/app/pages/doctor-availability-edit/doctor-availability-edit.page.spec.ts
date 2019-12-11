import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorAvailabilityEditPage } from './doctor-availability-edit.page';

describe('DoctorAvailabilityEditPage', () => {
  let component: DoctorAvailabilityEditPage;
  let fixture: ComponentFixture<DoctorAvailabilityEditPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DoctorAvailabilityEditPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorAvailabilityEditPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
