import { Injectable, OnDestroy } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { MsalService as CordovaMsalService } from '../msal/msal.service';
import { Platform } from '@ionic/angular';
import { StorageService } from '../storage/storage.service';
import { Subscription, ReplaySubject, Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { ToastService } from '../toast/toast.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {

  login: Subscription;
  logout: Subscription;

  public readonly authState: ReplaySubject<boolean> = new ReplaySubject<boolean>(1);

  constructor(
    private cordovaMsal: CordovaMsalService,
    private msalService: MsalService,
    private storageService: StorageService,
    private platform: Platform,
    private toastService: ToastService
    ) {
      this.storageService.getAccessToken().map(token => {
        if (!token) {
          this.loginUsingMsal();
          console.log('No Token');
        } else {
          console.log('Have a token', token);
        }
      }, error => {
        this.loginUsingMsal();
        console.log('Error getting token', error);
      });
    }

  ngOnDestroy(): void {
    this.login.unsubscribe();
    this.logout.unsubscribe();
  }

  public async isAuthenticated(): Promise<boolean> {
    return this.authState.asObservable().pipe(take(1)).toPromise();
  }

  private loginUsingMsal() {
    if (this.platform.is('cordova')) {
      this.loginCordovaMsal();
    } else {
      this.loginAzureMsal();
    }
  }

  public logoutUsingMsal() {
    if (this.platform.is('cordova')) {
      this.logoutCordovaMsal();
    } else {
      this.logoutAzureMsal();
    }
  }

  public loginAzureMsal(): void {
    this.msalService.loginRedirect();
  }

  public logoutAzureMsal(): void {
    this.msalService.logout();
    this.toastService.displaySuccess({message: 'Signed out'});
    this.storageService.clearAccessToken();
  }

  public loginCordovaMsal(): void {

    this.login = this.cordovaMsal.loginMsal().subscribe(success => {
      this.authState.next(true);
    }, error => {
      this.authState.next(false);
    });
  }

  public logoutCordovaMsal(): void {

    this.logout = this.cordovaMsal.logoutMsal().subscribe(success => {
      this.authState.next(false);
      this.toastService.displaySuccess({message: 'Signed out'});
    }, error => {
      console.log(error);
    });
  }
}
