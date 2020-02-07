import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { DoctorOnCallListPage } from './doctor-on-call-list.page';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

describe('DoctorOnCallListPage', () => {
  let component: DoctorOnCallListPage;
  let fixture: ComponentFixture<DoctorOnCallListPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DoctorOnCallListPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorOnCallListPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
