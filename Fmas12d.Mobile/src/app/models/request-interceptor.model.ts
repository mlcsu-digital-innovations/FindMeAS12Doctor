import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { MsalService, BroadcastService } from '@azure/msal-angular';
import { Observable } from 'rxjs';
import { StorageService } from '../services/storage/storage.service';
import { switchMap } from 'rxjs/operators';
import { MSALError } from 'src/app/models/msal-error.model';

export class RequestInterceptor implements HttpInterceptor {
  
  constructor(
    private auth: MsalService, 
    private broadcastService: BroadcastService,
    private storageService: StorageService
  ) {}
  
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let scopes = this.auth.getScopesForEndpoint(req.url);
    this.auth.verbose('Url: ' + req.url + ' maps to scopes: ' + scopes);

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
              if (err instanceof HttpErrorResponse && err.status == 401) {
                let scopes = this.auth.getScopesForEndpoint(req.url);
                let tokenCached = this.auth.getCachedTokenInternal(scopes);
                           
                if (tokenStored && tokenCached && tokenCached.token && 
                  tokenStored === tokenCached.token) {
                  this.auth.clearCacheForScope(tokenStored);
                  this.storageService.clearAccessToken();
                }
                
                let msalError = new MSALError(JSON.stringify(err), "", JSON.stringify(scopes));
                this.broadcastService.broadcast('msal:notAuthorized', msalError);
              }
            });
          }
          else {
            this.auth.loginRedirect();
          }
        })
      );
  }
}