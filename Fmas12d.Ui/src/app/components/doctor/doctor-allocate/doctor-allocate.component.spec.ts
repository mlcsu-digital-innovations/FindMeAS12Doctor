import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DoctorAllocateComponent } from './doctor-allocate.component';
import { SharedComponentsModule } from '../../shared-components.module';

describe('DoctorAcceptComponent', () => {
  let component: DoctorAllocateComponent;
  let fixture: ComponentFixture<DoctorAllocateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations:
      [
        DoctorAllocateComponent
      ],
      imports: [
        SharedComponentsModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorAllocateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
