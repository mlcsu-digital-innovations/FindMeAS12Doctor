import { Component, OnInit, ChangeDetectorRef, OnDestroy  } from '@angular/core';
import { NetworkService, ConnectionStatus } from 'src/app/services/network/network.service';
import { AuthService } from 'src/app/services/auth/auth.service';
import { Platform } from '@ionic/angular';
import { take } from 'rxjs/operators';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit, OnDestroy {
  public connection: boolean;
  public loggedIn: boolean;

  private subscription: Subscription;

  constructor(
    private networkService: NetworkService,
    private authService: AuthService,
    private changeRef: ChangeDetectorRef,
    private platform: Platform
    ) { }

  ngOnInit() {
    this.connection = this.networkService.getCurrentNetworkStatus() === ConnectionStatus.Online;

    this.networkService.onNetworkChange().subscribe((status: ConnectionStatus) => {
      this.connection = status === ConnectionStatus.Online;
      this.changeRef.detectChanges();
    });

    this.subscription = this.authService.authStatus.subscribe(status => {
      console.log('status is now ', status);
      this.loggedIn = status;
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  public logOn(): void {
    if (this.platform.is('cordova')) {
      // unsubscribe as soon as 1 value is returned
      this.authService.loginMsAdal().pipe(take(1)).subscribe();
    } else {
      this.authService.loginMsal();
    }
  }

}
