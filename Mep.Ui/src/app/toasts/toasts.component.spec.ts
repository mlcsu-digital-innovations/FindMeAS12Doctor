import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NgbToast } from '@ng-bootstrap/ng-bootstrap';
import { ToastsComponent } from './toasts.component';

describe('ToastsComponent', () => {
  let component: ToastsComponent;
  let fixture: ComponentFixture<ToastsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        ToastsComponent,
        NgbToast
     ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ToastsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
