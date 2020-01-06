import { AuthService } from '../auth/auth.service';
import { CanActivate } from "@angular/router";
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { Platform, NavController } from '@ionic/angular';
import { StorageService } from '../storage/storage.service';

@Injectable({
  providedIn: 'root'
})
export class RouteGuardService implements CanActivate {
  

  constructor(
    private authService: AuthService,
    private navCtrl: NavController,
    private platform: Platform,
    private storageService: StorageService
  ) { }

  canActivate(): Observable<boolean> {
    if (this.platform.is("cordova")) {
      return this.storageService.getAccessToken().map(token => {
        if (!token) {
          this.authService.loginMsAdal().subscribe(result => {
            this.navCtrl.navigateRoot("home");
          }, error => {
            this.navCtrl.navigateRoot("home");
          });
          return false;          
        }        
        return true;
      }, error => {
        this.authService.loginMsAdal().subscribe(result => {
          this.navCtrl.navigateRoot("home");
        }, error => {
          this.navCtrl.navigateRoot("home");
        });
        return false;
      });
    }
    else {
      return this.storageService.getAccessToken().map(token => {
        if (!token) {          
          this.authService.loginMsal();
          return false;
        }
        return true;        
      }, error => {
        this.authService.loginMsal();
        return false;
      });
    }
  }

}
