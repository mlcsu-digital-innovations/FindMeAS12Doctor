
import { async, ComponentFixture, TestBed } from 'src/app/components/assessment/assessment-create/node_modules/@angular/core/testing';
import { AssessmentListComponent } from './assessment-list.component';
import { SharedComponentsModule } from '../../shared-components.module';

describe('AssessmentListComponent', () => {
  let component: AssessmentListComponent;
  let fixture: ComponentFixture<AssessmentListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AssessmentListComponent ],
      imports: [ SharedComponentsModule ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AssessmentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
