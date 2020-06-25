import { AuthService } from '../services/auth/auth.service';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { MSALError } from 'src/app/models/msal-error.model';
import { MsalService as CordovaMsalService} from '../services/msal/msal.service';
import { MsalService, BroadcastService } from '@azure/msal-angular';
import { Observable } from 'rxjs';
import { Platform } from '@ionic/angular';
import { StorageService } from '../services/storage/storage.service';
import { switchMap } from 'rxjs/operators';

export class RequestInterceptor implements HttpInterceptor {

  constructor(
    private authService: AuthService,
    private broadcastService: BroadcastService,
    private msalService: MsalService,
    private platform: Platform,
    private storageService: StorageService,
    private cordovaMsalService: CordovaMsalService
  ) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    if (this.platform.is('cordova')) {

      return this.storageService.getAccessToken()
      .pipe(
        switchMap((tokenStored: string) => {
          if (tokenStored) {
            req = req.clone({
              setHeaders: {
                Authorization: `Bearer ${tokenStored}`,
                'Content-Type': 'application/json'
              }
            });
            return next.handle(req);
          } else {
            return this.cordovaMsalService.loginMsal()
            .pipe(
              switchMap((token: string) => {
                req = req.clone({
                  setHeaders: {
                    Authorization: `Bearer ${token}`,
                    'Content-Type': 'application/json'
                  }
                });
                return next.handle(req);
              })
            );
          }
        })
      );
    } else {

      let scopes = this.msalService.getScopesForEndpoint(req.url);
      this.msalService.verbose('Url: ' + req.url + ' maps to scopes: ' + scopes);

      if (scopes === null) {
        return next.handle(req);
      }

      return this.storageService.getAccessToken()
      .pipe(
        switchMap((tokenStored: string) => {
          if (tokenStored) {
            req = req.clone({
              setHeaders: {
                Authorization: `Bearer ${tokenStored}`,
                'Content-Type': 'application/json'
              }
            });

            return next.handle(req).do(event => {}, err => {

              if (err instanceof HttpErrorResponse && err.status === 401) {
                scopes = this.msalService.getScopesForEndpoint(req.url);
                const tokenCached = this.msalService.getCachedTokenInternal(scopes);

                if (tokenStored && tokenCached && tokenCached.token &&
                  tokenStored === tokenCached.token) {
                  this.msalService.clearCacheForScope(tokenStored);
                  this.storageService.clearAccessToken();
                }

                const msalError = new MSALError(JSON.stringify(err), '', JSON.stringify(scopes));
                this.broadcastService.broadcast('msal:notAuthorized', msalError);
              }
            });
          } else {
            this.msalService.loginRedirect();
          }
        })
      );

    }
  }
}
