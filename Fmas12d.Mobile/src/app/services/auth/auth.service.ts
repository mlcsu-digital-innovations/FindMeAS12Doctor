import { Injectable, OnDestroy } from '@angular/core';
import { Subscription, BehaviorSubject, Observable, ReplaySubject } from 'rxjs';
import { MsalService } from '../msal/msal.service';
import { ToastService } from '../toast/toast.service';
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
    private toastService: ToastService,
    private storageService: StorageService
    ) {
      this.storageService.getAccessToken().map(token => {
        if (!token) {
          this.loginMsal();
        }
      }, error => {
        this.loginMsal();
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

    this.login = this.msal.loginMsal().subscribe(success => {
      this.authState.next(true);
      this.toastService.displaySuccess({
        header: 'Success',
        message: 'Signed In'
      });
    }, error => {
      this.authState.next(false);
      this.toastService.displayError({
        header: 'Error',
        message: 'Unable to Sign In'
      });
    });
  }

  public logoutMsal(): void {
    this.logout = this.msal.logoutMsal().subscribe(success => {
      this.authState.next(false);
      this.toastService.displaySuccess({
        header: 'Success',
        message: 'Signed Out'
      });
    }, error => {
      this.toastService.displayError({
        header: 'Error',
        message: 'Unable to Sign Out'
      });
    });
  }
}
