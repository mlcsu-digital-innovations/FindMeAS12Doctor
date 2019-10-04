import { AppComponent } from './app.component';
import { AppRoutes } from './app.routes';
import { BrowserModule } from '@angular/platform-browser';
import { DigitOnlyModule } from '@uiowa/digit-only';
import { ExaminationModule } from './components/examination/examination.module';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { PatientModule } from './components/patient/patient.module';
import { ReferralModule } from './components/referral/referral.module';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent,
    PageNotFoundComponent
  ],
  imports: [
    BrowserModule,
    DigitOnlyModule,
    ExaminationModule,
    HttpClientModule,
    PatientModule,
    ReferralModule,
    RouterModule.forRoot(AppRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
