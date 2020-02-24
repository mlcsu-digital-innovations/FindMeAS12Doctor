import { Injectable } from '@angular/core';
import { Router, NavigationEnd, NavigationExtras, UrlTree } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RouterService {

  private previousUrl: string = undefined;
  private currentUrl: string = undefined;

  constructor(private router: Router) {
    this.currentUrl = this.router.url;
    router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.previousUrl = this.currentUrl;
        this.currentUrl = event.url;
      }
    });
  }

  public getPreviousUrl() {
    return this.previousUrl;
  }

  public navigate(commands: any[], extras?: NavigationExtras): Promise<boolean> {
    return this.router.navigate(commands, extras);
  }

  public navigateByUrl(url: string | UrlTree, extras?: NavigationExtras): Promise<boolean> {
    return this.router.navigateByUrl(url, extras);
  }

  public navigatePrevious() {
    if (this.previousUrl) {
      this.navigateByUrl(this.previousUrl);
    }
  }
}
