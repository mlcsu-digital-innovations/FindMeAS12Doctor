import { Component, OnInit } from '@angular/core';
import { SignalRService } from './services/signalR/signalr.service';
import { ToastService } from './services/toast/toast.service';
import { SignalRMessage } from './interfaces/signalRMessage';
import { ToastOptions } from './interfaces/toast-options';
import { UserDetailsService } from './services/user/user-details.service';
import { User } from './interfaces/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Fmas12d';

  private userProfileTypeId: number;

  constructor(
    private signalRService: SignalRService,
    private toastService: ToastService,
    private userDetails: UserDetailsService
  ) { }

  ngOnInit() {
    this.signalRService.startConnection();

    this.signalRService.notification
    .subscribe((signalR: SignalRMessage) => {

      this.userProfileTypeId =  this.userDetails.getCurrentUSerProfileType();

      if (this.userProfileTypeId === signalR.profileType) {
        this.toastService.displayInfo({message: signalR.message});
      }
    });
  }
}
