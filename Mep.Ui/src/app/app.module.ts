import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { DigitOnlyModule } from '@uiowa/digit-only';
import { DisableControlDirective } from './directives/disable-control/disable-control.directive';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from './navbar/navbar.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { ReferralCreateComponent } from './referral-create/referral-create.component';
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
