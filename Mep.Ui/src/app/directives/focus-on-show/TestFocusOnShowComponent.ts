import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-test',
  template: `
    <form [formGroup]='testForm'>
      <input
        type='text'
        formControlName='otherField'
        name='otherField'
        />
      <input
        type='text'
        formControlName='focusField'
        name='focusField'
        appFocusOnShow
      />
    </form>
  `
})
export class TestFocusOnShowComponent {
  @Input() testForm: FormGroup;
}
