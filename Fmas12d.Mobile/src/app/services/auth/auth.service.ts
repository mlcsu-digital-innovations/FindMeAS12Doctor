import { Injectable, OnDestroy } from '@angular/core';
import { MSAdal, AuthenticationContext, AuthenticationResult } from '@ionic-native/ms-adal/ngx';
import { MsalService, BroadcastService } from '@azure/msal-angular';
import { OAuthSettings } from 'src/oauth';
import { StorageService } from '../storage/storage.service';
import { Subscription, Observable, from } from 'rxjs';
import { ToastService } from '../toast/toast.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {
  private subscription: Subscription;

  constructor(    
    private broadcastService: BroadcastService,
    private msAdal: MSAdal,
    private msalService: MsalService,    
    private storageService: StorageService,
    private toastService: ToastService
    ) 
  {
    this.subscription = this.broadcastService.subscribe("msal:acquireTokenFailure", (payload) => {

      console.log(payload);
    });
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
    let authContext: AuthenticationContext = this.msAdal
      .createAuthenticationContext(OAuthSettings.authority);        

    return from(authContext.acquireTokenAsync(
        OAuthSettings.appId, 
        OAuthSettings.appId, 
        OAuthSettings.redirectUrl,
        null, 
        null, 
        null
      ).then((authResponse: AuthenticationResult) => {  
        this.storageService.storeAccessToken(authResponse.accessToken);      
        return authResponse.accessToken;
      }, error => {        
        this.toastService.displayError({ message: error });
        throw Promise.reject("Failed to authenticate: " + error);
      }));
  }

  public logoutMsAdal() {
    this.storageService.clearAccessToken();    
    this.toastService.displayMessage({ message: "You have logged out of Find Me An S12 Doctor" });
  }
}
