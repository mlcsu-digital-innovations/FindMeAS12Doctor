import { async, ComponentFixture, TestBed } from '@angular/core/testing';
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
