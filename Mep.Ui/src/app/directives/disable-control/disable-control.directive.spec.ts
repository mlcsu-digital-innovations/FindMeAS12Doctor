import { DisableControlDirective } from '../disable-control/disable-control.directive';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TestDisableControlComponent } from './TestDisableControlComponent';
import { DebugElement } from '@angular/core';
import { ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { NgControl } from '@angular/forms';

describe('Directive: DisableControlDirective', () => {

  let component: TestDisableControlComponent;
  let fixture: ComponentFixture<TestDisableControlComponent>;
  let InputElement: DebugElement;

  const formBuilder: FormBuilder = new FormBuilder();

  beforeEach(() => {

    TestBed.configureTestingModule({
      declarations: [TestDisableControlComponent, DisableControlDirective],
      imports: [ReactiveFormsModule],
      providers: [
        NgControl,
        {provide: FormBuilder, useValue: formBuilder}
      ]
    }).compileComponents();
  });

  function createComponent() {
    fixture = TestBed.createComponent(TestDisableControlComponent);
    component = fixture.componentInstance;
    InputElement = fixture.debugElement.nativeElement.querySelector('input');

    component.testForm = formBuilder.group({
      testField: ''
    });
    fixture.detectChanges();
  }

  it('should create an instance', () => {
    createComponent();
    expect(fixture.debugElement.componentInstance).toBeTruthy();
  });

  it('should create an instance with a disabled field', () => {

    TestBed.overrideComponent(TestDisableControlComponent, {
      set: {
        template: `<form [formGroup]="testForm">
        <input type="text" formControlName="testField" [appDisableControl]="true"/>
      </form>`
      }
    });
    TestBed.compileComponents();

    createComponent();
    expect('disabled' in InputElement.attributes).toBeTruthy();
  });

  it('should create an instance with an enabled field', () => {

    TestBed.overrideComponent(TestDisableControlComponent, {
      set: {
        template: `<form [formGroup]="testForm">
        <input type="text" formControlName="testField" [appDisableControl]="false"/>
      </form>`
      }
    });
    TestBed.compileComponents();

    createComponent();
    expect('disabled' in InputElement.attributes).toBeFalsy();
  });

});
