import { Injectable, OnDestroy } from '@angular/core';
import { MSAdal, AuthenticationContext, AuthenticationResult, TokenCacheItem } from '@ionic-native/ms-adal/ngx';
import { MsalService, BroadcastService } from '@azure/msal-angular';
import { OAuthSettings } from 'src/oauth';
import { StorageService } from '../storage/storage.service';
import { Subscription, Observable, from, BehaviorSubject } from 'rxjs';
import { ToastService } from '../toast/toast.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {
  private subscription: Subscription;

  public signInStatus: BehaviorSubject<boolean>;

  constructor(
    private broadcastService: BroadcastService,
    private msAdal: MSAdal,
    private msalService: MsalService,
    private storageService: StorageService,
    private toastService: ToastService
    ) {
    this.subscription = this.broadcastService.subscribe('msal:acquireTokenFailure', (payload) => {
      // TODO: Process acquire token failure
    });
    this.signInStatus = new BehaviorSubject<boolean>(false);
  }

  public loginMsal(): void {
    this.msalService.loginRedirect();
  }

  public logoutMsal(): void {
    this.msalService.logout();
    this.storageService.clearAccessToken();
  }

  ngOnDestroy() {
    this.broadcastService.getMSALSubject().next(1);
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  public loginMsAdal(): Observable<string> {

    console.log('loginMsAdal');

    let authContext: AuthenticationContext = this.msAdal
      .createAuthenticationContext(OAuthSettings.authority);

    const loginObservable: Observable<string> = new Observable((observer) => {

      authContext.tokenCache.readItems()
      .then((items: TokenCacheItem[]) => {

        if (items.length > 0) {
          console.log(items);
          const authority = items[0].authority;
          authContext = this.msAdal.createAuthenticationContext(authority);
        }

        // attempt to authorise user silently
        authContext.acquireTokenSilentAsync(OAuthSettings.appId, OAuthSettings.appId, null)
        .then((authResponse: AuthenticationResult) => {
          console.log('success (silent)', authResponse);
          this.broadcastService.broadcast('msadal:loginSuccess', authResponse);
          this.updateSignInStatus(true);
          observer.next(authResponse.accessToken);
          observer.complete();
        }, (error) => {
          console.log('failure (silent)', error);
          authContext.acquireTokenAsync(OAuthSettings.appId, OAuthSettings.appId, OAuthSettings.redirectUrl, null, null, null)
          .then((authResponse: AuthenticationResult) => {
            console.log('success (active)', authResponse);
            this.broadcastService.broadcast('msadal:loginSuccess', authResponse);
            this.updateSignInStatus(true);
            observer.next(authResponse.accessToken);
            observer.complete();
          }, (err) => {
              console.log('failure', err);
              this.toastService.displayError({ message: err });
              this.updateSignInStatus(false);
              observer.error(`Failed to authenticate: ${err}`);
              observer.complete();
          });
        });
      });
    });

    return loginObservable;
  }

  public logoutMsAdal() {
    const authContext: AuthenticationContext = this.msAdal
      .createAuthenticationContext(OAuthSettings.authority);
    authContext.tokenCache.clear();
    this.updateSignInStatus(false);
    this.storageService.clearAccessToken();
  }

  public get authStatus(): Observable<boolean> {
    return this.signInStatus.asObservable();
  }

  private updateSignInStatus(status: boolean): void {
    this.signInStatus.next(status);
  }
}
