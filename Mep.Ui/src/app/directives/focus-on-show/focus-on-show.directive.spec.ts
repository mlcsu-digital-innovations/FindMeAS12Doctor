import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FocusOnShowDirective } from './focus-on-show.directive';
import { NgControl, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { TestFocusOnShowComponent } from './TestFocusOnShowComponent';

describe('FocusOnShowDirective', () => {
  let component: TestFocusOnShowComponent;
  let fixture: ComponentFixture<TestFocusOnShowComponent>;

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
