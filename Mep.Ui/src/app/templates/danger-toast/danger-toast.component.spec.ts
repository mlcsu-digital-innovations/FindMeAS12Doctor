import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DangerToastComponent } from './danger-toast.component';

describe('DangerToastComponent', () => {
  let component: DangerToastComponent;
  let fixture: ComponentFixture<DangerToastComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DangerToastComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DangerToastComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
