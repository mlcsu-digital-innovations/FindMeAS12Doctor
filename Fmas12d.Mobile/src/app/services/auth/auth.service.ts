import { Injectable, OnDestroy } from '@angular/core';
import { MsalService, BroadcastService } from '@azure/msal-angular';
import { OAuthSettings } from 'src/oauth';
import { Subscription, Observable, from } from 'rxjs';
import { StorageService } from '../storage/storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {
  private subscription: Subscription;

  constructor(
    private msalService: MsalService,
    private broadcastService: BroadcastService,
    private storageService: StorageService
    ) {
    this.subscription = this.broadcastService.subscribe("msal:acquireTokenFailure", (payload) => {
      console.log(payload);
    });
  }

  public signIn(): void {
    this.msalService.loginRedirect();
  }

  public signOut(): void {
    this.msalService.logout();
    this.storageService.clearAccessToken();
  }

  ngOnDestroy() {
    this.broadcastService.getMSALSubject().next(1);
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
