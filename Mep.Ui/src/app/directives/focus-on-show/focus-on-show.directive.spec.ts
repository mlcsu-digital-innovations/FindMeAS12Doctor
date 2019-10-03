import { FocusOnShowDirective } from './focus-on-show.directive';
import { DebugElement } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TestFocusOnShowComponent } from './TestFocusOnShowComponent';
import { NgControl, FormBuilder, ReactiveFormsModule } from '@angular/forms';

fdescribe('FocusOnShowDirective', () => {
  let component: TestFocusOnShowComponent;
  let fixture: ComponentFixture<TestFocusOnShowComponent>;
  let InputElement: DebugElement;

  const formBuilder: FormBuilder = new FormBuilder();

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TestFocusOnShowComponent, FocusOnShowDirective],
      imports: [ReactiveFormsModule],
      providers: [NgControl, { provide: FormBuilder, useValue: formBuilder }]
    }).compileComponents();
  });

  function createComponent() {
    fixture = TestBed.createComponent(TestFocusOnShowComponent);
    component = fixture.componentInstance;
    InputElement = fixture.debugElement.nativeElement.querySelector('input');

    component.testForm = formBuilder.group({
      otherField: '',
      focusField: ''
    });
    fixture.detectChanges();
  }

  it('should create an instance and focus on focusField', () => {
    createComponent();
    expect(fixture.debugElement.nativeElement.ownerDocument.activeElement.name).toEqual('focusField');
  });
});
