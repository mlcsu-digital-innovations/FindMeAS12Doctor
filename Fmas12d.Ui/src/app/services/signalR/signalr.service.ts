import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { SignalRMessage } from 'src/app/interfaces/signalRMessage';
import * as signalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private hubConnection: signalR.HubConnection;
  public readonly notification: ReplaySubject<SignalRMessage> = new ReplaySubject<SignalRMessage>(1);

  constructor() {
  }

  public startConnection = () => {
    this.hubConnection =
      new signalR.HubConnectionBuilder()
      .withUrl(`${environment.apiEndpoint}/signalRHub`, {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .configureLogging(signalR.LogLevel.Debug)
      .build();

    this.hubConnection
      .start()
      .then(() => {
        this.waitForNotifications();
      })
      .catch(err => console.log('error while starting connection: ', err));
  }

  public waitForNotifications = () => {
    this.hubConnection.on('ReceiveNotification', ((profileType: number, message: string) => {
      this.notification.next({profileType, message});
    }));
  }
}
