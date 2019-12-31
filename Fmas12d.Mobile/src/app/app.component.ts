import { AuthService } from './services/auth/auth.service';
import { BroadcastService } from '@azure/msal-angular';
import { Component, OnInit } from '@angular/core';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';
import { OfflineManagerService } from 'src/app/services/offline-manager/offline-manager.service';
import { Platform } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { StorageService } from './services/storage/storage.service';
import * as jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss']
})
export class AppComponent implements OnInit {

  userName: string;

  constructor(
    private authService: AuthService,
    private broadcastService: BroadcastService,
    private networkService: NetworkService,
    private offlineManager: OfflineManagerService,
    private platform: Platform,
    private splashScreen: SplashScreen,
    private statusBar: StatusBar,
    private storageService: StorageService,
  ) {
  }

  ngOnInit() {
    this.platform.ready().then(() => {
      this.statusBar.styleDefault();
      this.splashScreen.hide();

      this.networkService.onNetworkChange().subscribe((status: ConnectionStatus) => {
        if (status === ConnectionStatus.Online) {
          this.offlineManager.checkForEvents().subscribe();
        }
      });

      this.broadcastService.subscribe('msal:loginFailure', (payload) => {
        console.log(payload);
      });

      this.broadcastService.subscribe('msal:loginSuccess', (payload) => {
        console.log(payload);
        this.storageService.storeAccessToken(payload.token);
      });

      this.broadcastService.subscribe('msal:acquireTokenSuccess', (payload) => {
        console.log(payload);
      });

      this.broadcastService.subscribe('msal:acquireTokenFailure', (payload) => {
        console.log(payload);
      });

      this.storageService.getAccessToken()
      .subscribe(result => {
        const details = jwt_decode(result);
        this.userName = details.name;
        console.log(details);
      });

    });
  }

  public logOff(): void {
    this.authService.signOut();
  }
}
