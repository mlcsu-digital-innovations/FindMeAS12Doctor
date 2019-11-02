import { async, ComponentFixture, TestBed } from 'src/app/components/assessment/assessment-view/node_modules/src/app/components/assessment/assessment-list/node_modules/src/app/components/assessment/assessment-create/node_modules/@angular/core/testing';
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
