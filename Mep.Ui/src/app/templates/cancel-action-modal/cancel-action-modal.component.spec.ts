import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CancelActionModalComponent } from './cancel-action-modal.component';

describe('CancelActionModalComponent', () => {
  let component: CancelActionModalComponent;
  let fixture: ComponentFixture<CancelActionModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CancelActionModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CancelActionModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
