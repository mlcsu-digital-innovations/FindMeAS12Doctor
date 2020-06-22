import { AuthService } from '../services/auth/auth.service';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { MSALError } from 'src/app/models/msal-error.model';
import { MsalService, BroadcastService } from '@azure/msal-angular';
import { Observable } from 'rxjs';
import { Platform } from '@ionic/angular';
import { StorageService } from '../services/storage/storage.service';
import { switchMap } from 'rxjs/operators';

export class RequestInterceptor implements HttpInterceptor {

  constructor(
    private authService: AuthService,
    private broadcastService: BroadcastService,
    // private msalService: MsalService,
    private platform: Platform,
    private storageService: StorageService
  ) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // if (this.platform.is("cordova")) {
    //   return this.storageService.getAccessToken()
    //   .pipe(
    //     switchMap((tokenStored: string) => {
    //       if (tokenStored) {
    //         req = req.clone({
    //           setHeaders: {
    //             Authorization: `Bearer ${tokenStored}`,
    //             'Content-Type': 'application/json'
    //           }
    //         });

    //         return next.handle(req);
    //       }
    //       else {
    //         return this.authService.loginMsAdal()
    //           .pipe(
    //             switchMap((token: string) => {
    //                 req = req.clone({
    //                 setHeaders: {
    //                   Authorization: `Bearer ${token}`,
    //                   'Content-Type': 'application/json'
    //                 }
    //               });

    //               return next.handle(req);
    //             })
    //           );
    //       }          
    //     }
    //   ));
    // }
    // else {
      // const scopes = this.msalService.getScopesForEndpoint(req.url);
      // this.msalService.verbose('Url: ' + req.url + ' maps to scopes: ' + scopes);

      // console.log('scopes', scopes);

      // if (scopes === null) {
      //   return next.handle(req);
      // }

      console.log('do something with access token');

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

              console.log('Updated request', req);

              return next.handle(req).do(event => {}, err => {

                console.log('Request Error', err);

                // if (err instanceof HttpErrorResponse && err.status === 401) {
                //   // const scopes = this.msalService.getScopesForEndpoint(req.url);
                //   const tokenCached = this.msalService.getCachedTokenInternal(scopes);

                //   if (tokenStored && tokenCached && tokenCached.token &&
                //     tokenStored === tokenCached.token) {
                //     this.msalService.clearCacheForScope(tokenStored);
                //     this.storageService.clearAccessToken();
                //   }

                //   const msalError = new MSALError(JSON.stringify(err), '', JSON.stringify(scopes));
                //   this.broadcastService.broadcast('msal:notAuthorized', msalError);
                // }
              });
            } else {
              // this.msalService.loginRedirect();
            }
          })
        );
    // }
  }
}
