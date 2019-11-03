import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-delay-spinner',
  templateUrl: './delay-spinner.component.html',
  styleUrls: ['./delay-spinner.component.css']
})
export class DelaySpinnerComponent {

  @Input() delayMessage: string;

}
