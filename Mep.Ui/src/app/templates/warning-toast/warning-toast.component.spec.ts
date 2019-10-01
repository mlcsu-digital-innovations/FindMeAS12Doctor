import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WarningToastComponent } from './warning-toast.component';

describe('WarningToastComponent', () => {
  let component: WarningToastComponent;
  let fixture: ComponentFixture<WarningToastComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WarningToastComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WarningToastComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
