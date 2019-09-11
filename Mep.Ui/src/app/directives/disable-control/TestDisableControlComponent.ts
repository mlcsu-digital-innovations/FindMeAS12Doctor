import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-test',
  template: `<form [formGroup]="testForm">
              <input type="text" formControlName="testField" [appDisableControl]="true"/>
            </form>
  `
})
export class TestDisableControlComponent {

  @Input() testForm: FormGroup;
}
