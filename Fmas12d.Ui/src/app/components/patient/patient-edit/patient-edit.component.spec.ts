import { async, ComponentFixture, TestBed } from 'src/app/components/assessment/assessment-view/node_modules/src/app/components/assessment/assessment-list/node_modules/src/app/components/assessment/assessment-create/node_modules/@angular/core/testing';

import { PatientEditComponent } from './patient-edit.component';
import { HttpClientModule } from 'src/app/components/assessment/assessment-view/node_modules/src/app/components/assessment/assessment-create/node_modules/@angular/common/http';
import { SharedComponentsModule } from '../../shared-components.module';

describe('PatientEditComponent', () => {
  let component: PatientEditComponent;
  let fixture: ComponentFixture<PatientEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        PatientEditComponent
      ],
      imports: [
        HttpClientModule,
        SharedComponentsModule
       ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
