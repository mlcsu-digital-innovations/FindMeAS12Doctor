import { Component, OnInit, ChangeDetectorRef, Input } from '@angular/core';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  public connection: boolean;

  @Input()
    title: string;

  @Input()
    lastUpdated: Date;

  constructor(
    private networkService: NetworkService,
    private changeRef: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.connection = this.networkService.getCurrentNetworkStatus() === ConnectionStatus.Online;

    this.networkService.onNetworkChange().subscribe((status: ConnectionStatus) => {
      this.connection = status === ConnectionStatus.Online;
      this.changeRef.detectChanges();
    });
  }
}
