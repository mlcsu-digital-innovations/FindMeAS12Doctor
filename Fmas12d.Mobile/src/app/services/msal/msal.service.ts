import { BroadcastService } from '@azure/msal-angular';
import { Injectable, OnDestroy } from '@angular/core';
import { Msal } from 'ionic-msal-native';
import { OAuthSettingsMSAL } from 'src/oauth';
import { StorageService } from '../storage/storage.service';
import { Subscription, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MsalService implements OnDestroy {
  private subscription: Subscription;
  private refreshTimer: ReturnType<typeof setTimeout>;

  constructor(
    private broadcastService: BroadcastService,
    private msal: Msal,
    private storageService: StorageService
    ) {
  }

  private logSuccess(entry: any) {
    console.log('Success', entry);
  }

  private logFail(entry: any) {
    console.log('Success', entry);
  }

  public msalInit(): Observable<any> {

    const options: any = {
      authorities: OAuthSettingsMSAL.authorities,
      authorizationUserAgent: OAuthSettingsMSAL.authorizationUserAgent,
      scopes: OAuthSettingsMSAL.scopes
    };

    const init = new Observable((observer) => {
      this.msal.msalInit(options).then((initResult) => {
        observer.next(initResult);
        observer.complete();
      },
        (err) => {
          console.log('MSAL Configuration Error:', err);
          observer.error(err);
      });
    });

    return init;

    // Cordova MSAL Plugin Log settings
    // this.msal.startLogger(MsalLogLevel.Verbose)
    //   .subscribe(
    //     (ok) => {
    //       this.logSuccess(ok);
    //     },
    //     (err) => {
    //       this.logFail(err);
    //   }
    // );
  }

  public loginMsal(): Observable<any> {

    const msalSignin = new Observable((observer) => {
      // try to sign in silently
      this.msal.signInSilent().then((jwt) => {
        this.broadcastService.broadcast('msal:loginSuccess', jwt);
        this.startTokenRefresh();
        observer.next(jwt);
        observer.complete();
     },
     () =>  {
       // try an interactive login
      this.msal.signInInteractive({prompt: 'LOGIN'}).then((jwt) => {
            this.broadcastService.broadcast('msal:loginSuccess', jwt);
            this.startTokenRefresh();
            observer.next(jwt);
            observer.complete();
         },
         (err) =>  {
           this.broadcastService.broadcast('msal:loginFailure', err);
           observer.error(err);
         });
     });
    });
    return msalSignin;
  }


  public loginMsalInteractive(): Observable<any> {

    const msalSignin = new Observable((observer) => {
      // try to sign in silently
      this.msal.signInInteractive({prompt: 'LOGIN'}).then((jwt) => {
        this.broadcastService.broadcast('msal:loginSuccess', jwt);
        this.startTokenRefresh();
        observer.next(jwt);
        observer.complete();
     }, (err) =>  {
           this.broadcastService.broadcast('msal:loginFailure', err);
           observer.error(err);
         });
     });
    return msalSignin;
  }

  public canSilentlyLogin(): Observable<boolean> {
    const msalSigninTest = new Observable<boolean>((observer) => {
      // try to sign in silently
      this.msal.signInSilent().then((jwt) => {
        observer.next(true);
        observer.complete();
     },
     () =>  {
      observer.next(false);
      observer.complete();
     });
    });
    return msalSigninTest;
  }

  private startTokenRefresh() {
    // Refresh the token in 10 minutes.
    this.refreshTimer =
      setTimeout(() => {
        this.broadcastService.broadcast('msal:refreshToken', null);
      }, 10 * 60 * 1000);
  }

  public refreshTokenSilently() {

    this.msal.signInSilent().then((jwt) => {
      this.broadcastService.broadcast('msal:tokenRefresh', jwt);
      this.startTokenRefresh();
    },
    () =>  {
      console.log('failed to refresh token');
      clearTimeout(this.refreshTimer);
    });
  }

  public signinSilently(): Observable<any> {

    const msalSigninSilently = new Observable((observer) => {
      // try to sign in silently
      this.msal.signInSilent().then((jwt) => {
        this.broadcastService.broadcast('msal:silentLoginSuccess', jwt);
        this.startTokenRefresh();
        observer.next(jwt);
        observer.complete();
     },
     (err) =>  {
        observer.error(err);
     });
    });
    return msalSigninSilently;
  }

  public logoutMsal(): Observable<any> {

    clearTimeout(this.refreshTimer);

    const msalSignout = new Observable((observer) => {
      this.msal.signOut().then(() => {
        this.storageService.clearAccessToken();
        this.broadcastService.broadcast('msal:logoutSuccess', '');
        observer.next();
        observer.complete();
     },
     (error) =>  {
        this.broadcastService.broadcast('msal:logoutFailure', '');
        observer.error(error);
     });
    });

    return msalSignout;
  }

  ngOnDestroy() {
    this.broadcastService.getMSALSubject().next(1);
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
    clearTimeout(this.refreshTimer);
  }

}
