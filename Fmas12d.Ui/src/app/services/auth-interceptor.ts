import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    private oidcSecurityService: OidcSecurityService;

    constructor(private injector: Injector) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let requestToForward = req;

        console.log("*** intercept");

        if (this.oidcSecurityService === undefined) {
            console.log("*** getting service");
            this.oidcSecurityService = this.injector.get(OidcSecurityService);
        }
        if (this.oidcSecurityService !== undefined) {
            console.log("*** getting token");
            let token = this.oidcSecurityService.getToken();
            if (token !== '') {
                console.log("*** token acquired");
                let tokenValue = 'Bearer ' + token;
                requestToForward = req.clone({ setHeaders: { Authorization: tokenValue } });
            } else {
              console.log("*** token is empty");
            }
        } else {
            console.debug('OidcSecurityService undefined: NO auth header!');
        }

        return next.handle(requestToForward);
    }
}
