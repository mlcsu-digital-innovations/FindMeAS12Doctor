import { Component, OnChanges, SimpleChanges, Input } from '@angular/core';

@Component({
  selector: 'app-nhs-number-field',
  templateUrl: './nhs-number-field.component.html',
  styleUrls: ['./nhs-number-field.component.css']
})
export class NhsNumberFieldComponent implements OnChanges {

  @Input()
  nhsNumber: string;

  // constructor() { }

  // ngOnInit() {
  // }

  ngOnChanges(changes: SimpleChanges) {
    console.log(changes);
  }

}
