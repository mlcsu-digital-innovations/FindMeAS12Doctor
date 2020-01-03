import { AuthService } from './services/auth/auth.service';
import { BroadcastService } from '@azure/msal-angular';
import { Component, OnInit } from '@angular/core';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';
import { OfflineManagerService } from 'src/app/services/offline-manager/offline-manager.service';
import { Platform, NavController } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { StorageService } from './services/storage/storage.service';
import * as jwt_decode from 'jwt-decode';
import { UserDetailsService } from './services/user-details/user-details.service';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { PROFILETYPEAMHP, PROFILETYPEDOCTOR } from './constants/app.constants';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss']
})
export class AppComponent implements OnInit {

  userName: string;
  isAmhp: boolean;
  isDoctor: boolean;

  constructor(
    private authService: AuthService,
    private broadcastService: BroadcastService,
    private navController: NavController,
    private networkService: NetworkService,
    private offlineManager: OfflineManagerService,
    private platform: Platform,
    private splashScreen: SplashScreen,
    private statusBar: StatusBar,
    private storageService: StorageService,
    private userDetailsService: UserDetailsService
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
        console.log('msal:loginFailure');
        console.log(payload);
      });

      this.broadcastService.subscribe('msal:loginSuccess', (payload) => {
        console.log('msal:loginSuccess');
        console.log(payload);
        this.setUserDetails(payload.token);
      });

      this.broadcastService.subscribe('msal:acquireTokenSuccess', (payload) => {
        console.log('msal:acquireTokenSuccess');
        console.log(payload);
      });

      this.broadcastService.subscribe('msal:acquireTokenFailure', (payload) => {
        console.log(payload);
      });
   
      this.broadcastService.subscribe('msadal:loginSuccess', (payload) => {
        console.log('msadal:loginSuccess');
        console.log(payload);
        this.setUserDetails(payload.accessToken);
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

  private setUserDetails(token: string): void {
    this.storageService.storeAccessToken(token);
    const details = jwt_decode(token);
    this.userName = details.name;

    this.userDetailsService.getUserDetails(details.oid)
    .subscribe(user => {
      this.isAmhp = user.profileTypeId === PROFILETYPEAMHP;
      this.isDoctor = user.profileTypeId === PROFILETYPEDOCTOR;
    });
  }
}
