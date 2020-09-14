import { Component, OnInit } from '@angular/core';
import { SignalRMessage } from './interfaces/signalRMessage';
import { SignalRService } from './services/signalR/signalr.service';
import { ToastService } from './services/toast/toast.service';
import { UserDetailsService } from './services/user/user-details.service';

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
    private userDetails: UserDetailsService,
  ) { }

  ngOnInit() {

    // clear any existing redirects
    localStorage.setItem('redirect', JSON.stringify('/'));

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
