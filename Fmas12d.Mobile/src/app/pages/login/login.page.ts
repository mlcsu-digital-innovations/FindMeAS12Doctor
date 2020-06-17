import { AuthService } from 'src/app/services/auth/auth.service';
import { Component, OnInit } from '@angular/core';
import { Platform, NavController } from '@ionic/angular';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  constructor(
    private authService: AuthService,
    private navCtrl: NavController,
    private platform: Platform
    ) { }

  ngOnInit() {

  }

  public login(): void {
    // if (this.platform.is("cordova")) {
    //   this.authService.loginMsAdal().subscribe(result => {
    //     this.navCtrl.navigateRoot("home");
    //   }, error => {
    //     this.navCtrl.navigateRoot("home");
    //   });
    // }
    // else {
      this.authService.loginMsal();
      this.navCtrl.navigateRoot('home');
    // }    
  }
}
