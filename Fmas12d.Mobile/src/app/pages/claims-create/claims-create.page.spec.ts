import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClaimsCreatePage } from './claims-create.page';

describe('ClaimsCreatePage', () => {
  let component: ClaimsCreatePage;
  let fixture: ComponentFixture<ClaimsCreatePage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClaimsCreatePage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClaimsCreatePage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
