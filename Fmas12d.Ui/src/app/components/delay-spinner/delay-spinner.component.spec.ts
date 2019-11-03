import { async, ComponentFixture, TestBed } from 'src/app/components/assessment/assessment-view/node_modules/src/app/components/assessment/assessment-list/node_modules/src/app/components/assessment/assessment-create/node_modules/@angular/core/testing';

import { DelaySpinnerComponent } from './delay-spinner.component';

describe('DelaySpinnerComponent', () => {
  let component: DelaySpinnerComponent;
  let fixture: ComponentFixture<DelaySpinnerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DelaySpinnerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DelaySpinnerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
