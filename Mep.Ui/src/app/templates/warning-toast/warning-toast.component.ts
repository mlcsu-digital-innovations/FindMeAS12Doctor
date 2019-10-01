import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-warning-toast',
  templateUrl: './warning-toast.component.html',
  styleUrls: ['./warning-toast.component.css']
})
export class WarningToastComponent {

  @Input()
  toastMessage: string;
}
