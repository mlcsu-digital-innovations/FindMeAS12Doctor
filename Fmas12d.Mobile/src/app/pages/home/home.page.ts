import { AuthService } from 'src/app/services/auth/auth.service';
import { Component, OnInit, ChangeDetectorRef  } from '@angular/core';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';
import { PinDialog } from '@ionic-native/pin-dialog/ngx';
import { Platform } from '@ionic/angular';
import { StorageService } from 'src/app/services/storage/storage.service';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {
  public connection: boolean;
  public isAuthenticated: boolean;

  public askForPin: boolean;

  constructor(
    private authService: AuthService,
    private changeRef: ChangeDetectorRef,
    private networkService: NetworkService,
    private pinDialog: PinDialog,
    private platform: Platform,
    private storageService: StorageService
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

    this.platform.ready().then(() => {

      if (this.platform.is('cordova') && !this.isAuthenticated) {

        this.askForPin = false;

        // Check to see if we have a stored pin for the logged in user.
        this.storageService.getPin()
          .subscribe(pin => {
            console.log('PIN - ', pin);
          });
      }

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
