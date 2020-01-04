import * as jwt_decode from 'jwt-decode';
import { AuthService } from './services/auth/auth.service';
import { BroadcastService } from '@azure/msal-angular';
import { Component, OnInit } from '@angular/core';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';
import { OfflineManagerService } from 'src/app/services/offline-manager/offline-manager.service';
import { PROFILETYPEAMHP, PROFILETYPEDOCTOR } from './constants/app.constants';
import { Platform, NavController } from '@ionic/angular';
import { Router } from '@angular/router';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { StorageService } from './services/storage/storage.service';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { ToastService } from './services/toast/toast.service';
import { UserDetailsService } from './services/user-details/user-details.service';

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
    private router: Router,
    private splashScreen: SplashScreen,
    private statusBar: StatusBar,
    private storageService: StorageService,
    private toastService: ToastService,
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
        this.storageService.storeAccessToken(payload.token);
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
        this.storageService.storeAccessToken(payload.accessToken);
        this.setUserDetails(payload.accessToken);
      });
    });

    this.storageService.getAccessToken().subscribe(token => {
      if (token) {
        this.setUserDetails(token);
      }      
    }, error => {
      this.toastService.displayError({message: error});
    });
  }

  public logOff(): void {    
    if (this.platform.is("cordova")) {
      this.authService.logoutMsAdal();

      if (this.router.url === "/home") {
        this.authService.loginMsAdal();
      }
      else {
        this.navController.navigateRoot("home");
      }      
    }
    else {
      this.authService.logoutMsal();
    }
    
  }

  private setUserDetails(token: string): void {    
    const details = jwt_decode(token);

    if (details.name) {
      this.userName = details.name;
    }
    
    if (details.oid) {
      this.userDetailsService.getUserDetails(details.oid)
      .subscribe(user => {
        this.isAmhp = user.profileTypeId === PROFILETYPEAMHP;
        this.isDoctor = user.profileTypeId === PROFILETYPEDOCTOR;
      });
    }    
  }
}
