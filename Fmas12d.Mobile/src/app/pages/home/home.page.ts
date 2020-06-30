import { AuthService } from 'src/app/services/auth/auth.service';
import { Component, OnInit, ChangeDetectorRef  } from '@angular/core';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';
import { Platform } from '@ionic/angular';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {
  public connection: boolean;
  public isAuthenticated: boolean;

  constructor(
    private authService: AuthService,
    private changeRef: ChangeDetectorRef,
    private networkService: NetworkService,
    private platform: Platform
    ) {

      this.authService.authState.subscribe(authState => {
        this.isAuthenticated = authState;
      });

     }

  ngOnInit() {
    this.connection = this.networkService.getCurrentNetworkStatus() === ConnectionStatus.Online;

    this.networkService.onNetworkChange().subscribe((status: ConnectionStatus) => {
      this.connection = status === ConnectionStatus.Online;
      this.changeRef.detectChanges();
    });
  }

  public logIn(): void {

    if (this.platform.is('cordova')) {
      this.authService.loginCordovaMsal();
    } else {
      this.authService.loginAzureMsal();
    }
  }
}
