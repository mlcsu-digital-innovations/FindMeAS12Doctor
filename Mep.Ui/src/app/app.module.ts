import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReferralCreateComponent } from './referral-create/referral-create.component';
import { NavbarComponent } from './navbar/navbar.component';

import { SwitchComponent } from './switch/switch.component';
import { NhsNumberFieldComponent } from './nhs-number-field/nhs-number-field.component';

@NgModule({
  declarations: [
    AppComponent,
    ReferralCreateComponent,
    NavbarComponent,
    SwitchComponent,
    NhsNumberFieldComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
