import { AuthService } from './services/auth/auth.service';
import { BroadcastService } from '@azure/msal-angular';
import { Component } from '@angular/core';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';
import { OfflineManagerService } from 'src/app/services/offline-manager/offline-manager.service';
import { Platform, NavController } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { StorageService } from './services/storage/storage.service';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss']
})
export class AppComponent {
  constructor(
    private authService: AuthService,
    private broadcastService: BroadcastService,
    private navController: NavController,
    private networkService: NetworkService,
    private offlineManager: OfflineManagerService,
    private platform: Platform,
    private splashScreen: SplashScreen,
    private statusBar: StatusBar,
    private storageService: StorageService    
  ) {
    this.initializeApp();
  }

  initializeApp() {
    this.platform.ready().then(() => {
      this.statusBar.styleDefault();
      this.splashScreen.hide();

      this.networkService.onNetworkChange().subscribe((status: ConnectionStatus) => {
        if (status == ConnectionStatus.Online) {
          this.offlineManager.checkForEvents().subscribe();
        }
      });   
      
      this.broadcastService.subscribe("msal:loginFailure", (payload) => {
        console.log(payload);
      });
        
      this.broadcastService.subscribe("msal:loginSuccess", (payload) => {   
        console.log(payload);
        this.storageService.storeAccessToken(payload.token);
      });
  
      this.broadcastService.subscribe("msal:acquireTokenSuccess", (payload) => {
        console.log(payload);        
      });
      
      this.broadcastService.subscribe("msal:acquireTokenFailure", (payload) => {
        console.log(payload);
      });  
    });
  }

  public logOff(): void {    
    if (this.platform.is("cordova")) {
      this.authService.logoutMsAdal();
    }
    else {
      this.authService.logoutMsal();
    }
    
    this.navController.navigateRoot("login");
  }
}
