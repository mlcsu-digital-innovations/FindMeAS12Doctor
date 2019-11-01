import { Component, OnInit } from '@angular/core';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  public connection: string;

  constructor(private networkService: NetworkService) { }

  ngOnInit() {  
    this.setConnection(this.networkService.getCurrentNetworkStatus());

    this.networkService.onNetworkChange().subscribe((status: ConnectionStatus) => {    
      this.setConnection(status);    
    });
  }

  private setConnection(connectionStatus: ConnectionStatus): void {
    this.connection = connectionStatus === ConnectionStatus.Online ? "light" : "dark";    
  }
}
