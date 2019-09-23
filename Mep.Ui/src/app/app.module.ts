import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReferralCreateComponent } from './referral-create/referral-create.component';
import { NavbarComponent } from './navbar/navbar.component';

import { DigitOnlyModule } from '@uiowa/digit-only';
import { DisableControlDirective } from './directives/disable-control/disable-control.directive';

import { HttpClientModule } from '@angular/common/http';
import { ToastsComponent } from './toasts/toasts.component';

@NgModule({
  declarations: [
    AppComponent,
    DisableControlDirective,
    NavbarComponent,
    ReferralCreateComponent,
    ToastsComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    DigitOnlyModule,
    HttpClientModule,
    NgbModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
