import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminationViewComponent } from './examination-view.component';

describe('ExaminationViewComponent', () => {
  let component: ExaminationViewComponent;
  let fixture: ComponentFixture<ExaminationViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExaminationViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExaminationViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
