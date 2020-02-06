import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClaimsListPage } from './claims-list.page';

describe('ClaimsListPage', () => {
  let component: ClaimsListPage;
  let fixture: ComponentFixture<ClaimsListPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClaimsListPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClaimsListPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
