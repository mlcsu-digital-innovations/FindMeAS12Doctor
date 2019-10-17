import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ExaminationEditComponent } from './examination-edit.component';
import { SharedComponentsModule } from '../../shared-components.module';

describe('ExaminationEditComponent', () => {
  let component: ExaminationEditComponent;
  let fixture: ComponentFixture<ExaminationEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        ExaminationEditComponent
      ],
      imports: [
        SharedComponentsModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExaminationEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
