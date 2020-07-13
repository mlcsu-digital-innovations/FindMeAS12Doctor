import { Component, OnInit } from '@angular/core';
import { SignalRService } from './services/signalR/signalr.service';
import { ToastService } from './services/toast/toast.service';
import { SignalRMessage } from './interfaces/signalRMessage';
import { ToastOptions } from './interfaces/toast-options';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Fmas12d';

  constructor(
    private signalRService: SignalRService,
    private toastService: ToastService
  ) { }

  ngOnInit() {
    this.signalRService.startConnection();

    this.signalRService.notification
    .subscribe((signalR: SignalRMessage) => {

      const options: ToastOptions = {audience: signalR.profileType, message: signalR.message};
      this.toastService.displayNotification(options);
    });

  }
}
