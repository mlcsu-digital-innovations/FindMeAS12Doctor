import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DoctorSelectComponent } from './doctor-select.component';
import { SharedComponentsModule } from '../../shared-components.module';

describe('DoctorSelectComponent', () => {
  let component: DoctorSelectComponent;
  let fixture: ComponentFixture<DoctorSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        DoctorSelectComponent
      ],
      imports: [
        SharedComponentsModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
