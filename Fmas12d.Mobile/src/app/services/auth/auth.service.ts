import { Injectable } from '@angular/core';
import { MsalService, BroadcastService } from '@azure/msal-angular';
import { Subscription, Observable, from } from 'rxjs';
import { StorageService } from '../storage/storage.service';
import { ApiService } from '../api/api.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private subscription: Subscription;

  constructor(
    private apiService: ApiService,
    private msalService: MsalService,    
    private broadcastService: BroadcastService,
    private storageService: StorageService
    ) 
  {
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
    if(this.subscription) {
      this.subscription.unsubscribe();      
    }
  }

  public login(email: string, password: string): Observable<any> {
    return this.apiService.login(email, password);           
  }

  public logout() {

  }
}
