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
  ) { }

  async canActivate(): Promise<boolean> {
    console.log('check canActivate');
    return this.authService.isAuthenticated();
  }

}
