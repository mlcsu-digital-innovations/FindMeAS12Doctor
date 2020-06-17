import { Injectable, OnDestroy } from '@angular/core';
import { BroadcastService } from '@azure/msal-angular';
import { OAuthSettings } from 'src/oauth';
import { StorageService } from '../storage/storage.service';
import { Subscription, Observable, from } from 'rxjs';
import { Msal, MsalLogLevel } from 'ionic-msal-native';
import { Platform } from '@ionic/angular';
import { ObserveOnOperator } from 'rxjs/internal/operators/observeOn';

@Injectable({
  providedIn: 'root'
})
export class MsalService implements OnDestroy {
  private subscription: Subscription;
  private pluginReady: boolean;
  
  constructor(
    private broadcastService: BroadcastService,
    private msal: Msal,
    private storageService: StorageService,
    private platform: Platform
    ) {
      this.subscription = this.broadcastService.subscribe('msal:acquireTokenFailure', (payload) => {
      // TODO: Process acquire token failure
      });
  }

  private logSuccess(entry: any) {
    console.log('Success', entry);
  }

  private logFail(entry: any) {
    console.log('Success', entry);
  }

  public msalInit() {

    console.log('Init MSAL');

    const options: any = {
      authorities: OAuthSettings.authorities,
      authorizationUserAgent: OAuthSettings.authorizationUserAgent,
      scopes: OAuthSettings.scopes
    };

    this.msal.msalInit(options).then((initResult) => {
      console.log('MSAL Configuration Success:', initResult); 
      this.pluginReady = true;
      return initResult;
    },
      (err) => {
        this.pluginReady = false;
        console.log('MSAL Configuration Error:', err);
    });

    // Cordova MSAL Plugin Log settings
    this.msal.startLogger(MsalLogLevel.Verbose).subscribe((x) => {console.log('Success', x); }, (err) => {
      console.log('Error', err);
    });

  }

  public loginMsal(): Observable<any> {

    const msalSignin = new Observable((observer) => {
      // try to sign in silently
      console.log('try to sign in silently');
      this.msal.signInSilent().then((jwt) => {
        console.log('silent signin ok');
        observer.next(jwt);
        observer.complete();
     },
     () =>  {
      console.log('failed to sign in silently');
       // try an interactive login
      this.msal.signInInteractive().then((jwt) => {
            console.log('interactive sign in ok', jwt);
            observer.next(jwt);
            observer.complete();
         },
         (err) =>  {
           console.log('sign in error', err);
           observer.error(err);
         });
     });
    });
    return msalSignin;
  }

  public logoutMsal(): Observable<any> {

    const msalSignout = new Observable((observer) => {
      this.msal.signOut().then(() => {
        this.storageService.clearAccessToken();
        observer.complete();
     },
     (error) =>  {
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
  }

}
