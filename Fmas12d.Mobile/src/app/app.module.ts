import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { ComponentsModule } from './components/components.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { IonicStorageModule } from '@ionic/storage';
import { MsalModule, MsalInterceptor } from '@azure/msal-angular';
import { Network } from '@ionic-native/network/ngx'
import { NgModule } from '@angular/core';
import { OAuthSettings } from '../oauth';
import { RequestInterceptor } from './models/request-interceptor.model';
import { RouteReuseStrategy } from '@angular/router';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { environment, ProtectedResourceMap } from 'src/environments/environment';

@NgModule({
  declarations: [AppComponent],
  entryComponents: [],
  imports: [
    AppRoutingModule, 
    BrowserModule,
    ComponentsModule,
    HttpClientModule,
    IonicModule.forRoot(),
    IonicStorageModule.forRoot(),
    MsalModule.forRoot({
      clientID: OAuthSettings.appId,
      authority: OAuthSettings.authority,
      cacheLocation: "localStorage",
      consentScopes: OAuthSettings.consentScopes,
      protectedResourceMap: ProtectedResourceMap,
      redirectUri: environment.redirectUri     
    })
  ],
  providers: [
    Network,
    StatusBar,
    SplashScreen,
    { 
      provide: RouteReuseStrategy, 
      useClass: IonicRouteStrategy 
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: RequestInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }