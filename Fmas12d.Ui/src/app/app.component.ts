import { Component, OnInit } from '@angular/core';
import { SignalRService } from './services/signalR/signalr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Fmas12d';

  constructor(
    public signalRService: SignalRService
  ) { }

  ngOnInit() {
    console.log('try to start signalR connection');
    this.signalRService.startConnection();
  }

}
