import {Component} from '@angular/core';
import {ToastService} from '../../services/toast/toast.service';

@Component({
  selector: 'app-toasts',
  templateUrl: './toasts.component.html',
  styleUrls: ['./toasts.component.css'],
  host: {'[class.ngb-toasts]' : 'true'}
})
export class ToastsComponent {

  constructor(public toastService: ToastService) {}
}
