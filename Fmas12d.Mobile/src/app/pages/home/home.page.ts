import { AuthenticationContext, MSAdal, TokenCacheItem } from '@ionic-native/ms-adal/ngx';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Component, OnInit, ChangeDetectorRef, OnDestroy  } from '@angular/core';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';
import { OAuthSettings } from 'src/oauth';
import { PinDialog } from '@ionic-native/pin-dialog/ngx';
import { Platform } from '@ionic/angular';
import { Subscription } from 'rxjs';
import { take } from 'rxjs/operators';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit, OnDestroy {

  private pin: number;
  private subscription: Subscription;
  public askForPin: boolean;
  public connection: boolean;
  public loggedIn: boolean;
  public userName: string;

  constructor(
    private authService: AuthService,
    private changeRef: ChangeDetectorRef,
    private msAdal: MSAdal,
    private networkService: NetworkService,
    private pinDialog: PinDialog,
    private platform: Platform,
    private toastService: ToastService
    ) { }

  ngOnInit() {
    this.connection = this.networkService.getCurrentNetworkStatus() === ConnectionStatus.Online;

    this.networkService.onNetworkChange().subscribe((status: ConnectionStatus) => {
      this.connection = status === ConnectionStatus.Online;
      this.changeRef.detectChanges();
    });

    this.subscription = this.authService.authStatus.subscribe(status => {
      this.loggedIn = status;
    });

    this.platform.ready().then(() => {

      if (this.platform.is('cordova')) {
        const authContext: AuthenticationContext = this.msAdal
        .createAuthenticationContext(OAuthSettings.authority);
  
        console.log('platform is cordova ...');
        this.askForPin = false;
  
        if (authContext.tokenCache !== null ) {
          authContext.tokenCache.readItems()
          .then((items: TokenCacheItem[]) => {
  
            console.log(`items in cache = ${items.length}`);
  
            if (items.length > 0) {
              const userInfo = items[items.length - 1].userInfo;
  
              this.userName = userInfo.displayableId;
              this.pin = this.getPin(userInfo.userId);
              console.log(userInfo);
              console.log(`pin ${this.pin}`);
  
              console.log('askForPin = true');
              this.askForPin = true;
            }
          });
        }
      }
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  public logOn(): void {
    if (this.platform.is('cordova')) {

      console.log('cordova ....');
      console.log('askForPin', this.askForPin);

      if (this.askForPin) {

        this.pinDialog.prompt('Enter your PIN', 'Unlock', ['OK', 'Cancel'])
        .then((result: {buttonIndex: number, input1: string}) => {
          if (result.buttonIndex === 1) {
            console.log(result);
            if ( parseInt(result.input1, 10) === this.pin) {
            // unsubscribe as soon as 1 value is returned
              this.authService.loginMsAdal().pipe(take(1)).subscribe();
            } else {
              this.toastService.displayError(
                {
                  message: 'Incorrect PIN'
                }
              );
            }
          } else {
            console.log('cancelled', result.buttonIndex);
          }
        });
      } else {
        // unsubscribe as soon as 1 value is returned
        this.authService.loginMsAdal().pipe(take(1)).subscribe();
      }
    } else {
      this.authService.loginMsal();
    }
  }

  public logOff(): void {
    if (this.platform.is('cordova')) {
      this.authService.logoutMsAdal();
    } else {
      this.authService.logoutMsal();
    }
    this.toastService.displayMessage(
      {
        message: 'You have been signed out'
      }
    );
  }

  public verify(): void {
    this.pinDialog.prompt('Enter your PIN', 'Verify PIN', ['OK', 'Cancel'])
    .then((result: {buttonIndex: number, input1: string}) => {
      if (result.buttonIndex === 1) {
        console.log(result);
      } else {
        console.log('cancelled', result.buttonIndex);
      }
    });
  }

  private getPin(oid: string): number {
    // get first 4 characters
    const first = oid.substr(0, 4);
    return parseInt(first, 16);
  }

}
