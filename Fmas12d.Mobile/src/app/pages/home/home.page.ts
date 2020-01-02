import { Component, OnInit, ChangeDetectorRef  } from '@angular/core';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {
  public connection: boolean;

  constructor(private networkService: NetworkService, private changeRef: ChangeDetectorRef) { }

  ngOnInit() {  
    this.connection = this.networkService.getCurrentNetworkStatus() === ConnectionStatus.Online;

    this.networkService.onNetworkChange().subscribe((status: ConnectionStatus) => {    
      this.connection = status === ConnectionStatus.Online; 
      this.changeRef.detectChanges();      
    });
  }

}
