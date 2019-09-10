import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NhsNumberFieldComponent } from './nhs-number-field.component';

describe('NhsNumberFieldComponent', () => {
  let component: NhsNumberFieldComponent;
  let fixture: ComponentFixture<NhsNumberFieldComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NhsNumberFieldComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NhsNumberFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
