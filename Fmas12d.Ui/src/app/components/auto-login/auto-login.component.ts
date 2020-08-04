import { Component, OnInit, OnDestroy } from '@angular/core';
import { OidcSecurityService, AuthorizationResult } from 'angular-auth-oidc-client';

@Component({
    selector: 'app-auto-component',
    templateUrl: './auto-login.component.html'
})

export class AutoLoginComponent implements OnInit, OnDestroy {
    lang: any;

    constructor(public oidcSecurityService: OidcSecurityService
    ) {
        console.log('auto-login component constructor');
        this.oidcSecurityService.onModuleSetup.subscribe(() => { this.onModuleSetup(); });
    }

    ngOnInit() {
        if (this.oidcSecurityService.moduleSetup) {
            this.onModuleSetup();
        }
    }

    ngOnDestroy(): void {
    }

    private onModuleSetup() {
        this.oidcSecurityService.authorize();
    }
}
