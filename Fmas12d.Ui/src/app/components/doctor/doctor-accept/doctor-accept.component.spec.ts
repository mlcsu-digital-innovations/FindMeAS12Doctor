import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorAcceptComponent } from './doctor-accept.component';
import { SharedComponentsModule } from '../../shared-components.module';

describe('DoctorAcceptComponent', () => {
  let component: DoctorAcceptComponent;
  let fixture: ComponentFixture<DoctorAcceptComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations:
      [
        DoctorAcceptComponent
      ],
      imports: [
        SharedComponentsModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorAcceptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
