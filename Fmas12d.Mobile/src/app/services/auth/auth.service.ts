import { Injectable, OnDestroy } from '@angular/core';
import { Subscription, ReplaySubject } from 'rxjs';
import { MsalService } from '../msal/msal.service';
import { StorageService } from '../storage/storage.service';
import { take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {

  login: Subscription;
  logout: Subscription;

  public readonly authState: ReplaySubject<boolean> = new ReplaySubject<boolean>(1);

  constructor(
    private msal: MsalService,
    private storageService: StorageService
    ) {
      this.storageService.getAccessToken().map(token => {
        if (!token) {
          // this.loginMsal();
          console.log('No Token');
        } else {
          console.log('Have a token', token);
        }
      }, error => {
        // this.loginMsal();
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

  public loginMsal(): void {

    console.log('authService:login');

    this.login = this.msal.loginMsal().subscribe(success => {
      this.authState.next(true);
    }, error => {
      this.authState.next(false);
    });
  }

  public logoutMsal(): void {

    console.log('authService:logout');

    this.logout = this.msal.logoutMsal().subscribe(success => {
      this.authState.next(false);
      console.log('logged out');
    }, error => {
      console.log(error);
    });
  }
}
