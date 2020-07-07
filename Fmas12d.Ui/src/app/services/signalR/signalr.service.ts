import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  private hubConnection: signalR.HubConnection;

  constructor() {
  }

  public startConnection = () => {
    console.log('start connection ' + `${environment.apiEndpoint}/signalRHub`);
    this.hubConnection =
      new signalR.HubConnectionBuilder()
      .withUrl(`${environment.apiEndpoint}/signalRHub`, {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('connection started'))
      .catch(err => console.log('error while starting connection: ', err));
  }

  public showNotification = () => {
    this.hubConnection.on('ReceiveNotification', (msg) => {
      console.log(msg);
    });
  }
}
