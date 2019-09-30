import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-danger-toast',
  templateUrl: './danger-toast.component.html',
  styleUrls: ['./danger-toast.component.css']
})
export class DangerToastComponent {

  @Input()
  toastMessage: string;

  @Input()
  errMessage: string;
}
