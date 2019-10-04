
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ExaminationListComponent } from './examination-list.component';
import { SharedComponentsModule } from '../../shared-components.module';

describe('ExaminationListComponent', () => {
  let component: ExaminationListComponent;
  let fixture: ComponentFixture<ExaminationListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExaminationListComponent ],
      imports: [ SharedComponentsModule ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExaminationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
